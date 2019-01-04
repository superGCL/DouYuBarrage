using DouyuBarrage.Message;
using DouyuBarrage.Request;
using DouyuBarrage.Utils;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;

namespace DouyuBarrage.Client
{
    /// <summary>
    /// DouYu barrage client.
    /// </summary>
    public class DouYuBarrageClient
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILog logger = LogManager.GetLogger("Logger", typeof(DouYuBarrageClient));

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DouyuBarrage.DouYuBarrageClient"/> class.
        /// </summary>
        public DouYuBarrageClient()
        {
            // 默认的斗鱼弹幕api接口地址
            DouyuBarrageApiAddress = "openbarrage.douyutv.com";
            DouyuBarrageApiPort = 8601;
            Running = false;

            // 心跳包定时发送
            keepAliveTimer = new Timer();
            keepAliveTimer.Interval = 45 * 1000; // 45s
            keepAliveTimer.Elapsed += (object sender, ElapsedEventArgs e) => { KeepAlive(); };

            // 单独线程接收消息
            messageLookTask = new Task(Run);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~DouYuBarrageClient()
        {
            if (tcpClient == null || !tcpClient.Connected)
            {
                return;
            }

            Logout();
        }

        /// <summary>
        /// Connect the specified roomId and groupId.
        /// </summary>
        /// <returns>The connect.</returns>
        /// <param name="roomId">Room identifier.</param>
        /// <param name="groupId">Group identifier.</param>
        public bool Connect(int roomId, int groupId = -9999)
        {
            // 连接之斗鱼弹幕服务器
            tcpClient = new TcpClient();
            tcpClient.Connect(DouyuBarrageApiAddress, DouyuBarrageApiPort);

            // 发送登录请求
            if (!Login(roomId))
            {
                logger.Error($"Login Room[{roomId}] Failed!");
                return false;
            }

            // 加入分组
            if (!JoinGroup(roomId, groupId))
            {
                logger.Error("Join Group Failed!");
                return false;
            }

            RoomId = roomId;
            GroupId = groupId;
            Running = true;

            // 启动心跳线程
            keepAliveTimer.Start();

            // 启动接收消息循环
            messageLookTask.Start();

            logger.Info($"Connected To {roomId}.");

            return true;
        }

        /// <summary>
        /// Disconnect this instance.
        /// </summary>
        public void Disconnect()
        {
            if (tcpClient == null || !tcpClient.Connected)
            {
                return;
            }

            Logout();

            tcpClient.Close();
            tcpClient = null;

            logger.Info("Disconnect.");
        }

        /// <summary>
        /// Login the specified roomId.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="roomId">Room identifier.</param>
        private bool Login(int roomId)
        {
            if (tcpClient == null || !tcpClient.Connected)
            {
                return false;
            }

            // 发送登录请求
            LoginRequest loginRequest = new LoginRequest(roomId);
            BarragePacket sendPacket = new BarragePacket(loginRequest.ToString(), MessageType.CLIENT);
            NetworkStream ns = tcpClient.GetStream();
            ns.Write(sendPacket.Bytes, 0, sendPacket.Bytes.Length);
            ns.Flush();

            // 接收登录请求回应
            byte[] buffer = BlockRead(ns, 4);

            // 解析出包长度
            int messageLength = BitConverter.ToInt32(buffer);

            // 根据读取到的包长，接收剩下的包体
            buffer = BlockRead(ns, messageLength);

            // 解析响应
            BarragePacket recvPacket = new BarragePacket(buffer);

            // 记录日志
            logger.Debug(recvPacket.ToString());

            // 将返回结果解析成LoginResponse
            LoginResponseMessage response = MessageResolver.ResolveLoginResponseMessage(recvPacket.ToString());
            OnLoginSucceed?.Invoke(response);

            return true;
        }

        /// <summary>
        /// Logout the roomId
        /// </summary>
        private void Logout()
        {
            if (tcpClient == null || !tcpClient.Connected)
            {
                return;
            }

            // 判断消息循环任务是否退出了，若退出了，则说明网络出了问题
            if (!messageLookTask.IsCompleted)
            {
                // 消息循环任务还没有退出，说明是主动断开连接，需要发送Logout包
                LogoutRequest logoutRequest = new LogoutRequest();
                BarragePacket packet = new BarragePacket(logoutRequest.ToString(), MessageType.CLIENT);
                tcpClient.GetStream().Write(packet.Bytes, 0, packet.Bytes.Length);
                tcpClient.GetStream().Flush();
            }

            // 设置是否正在运行标志为false
            Running = false;

            // 停止心跳线程
            keepAliveTimer.Stop();

            // 等待消息循环退出
            messageLookTask.Wait();

            logger.Info("Logout.");
        }

        /// <summary>
        /// Joins the group.
        /// </summary>
        /// <returns><c>true</c>, if group was joined, <c>false</c> otherwise.</returns>
        /// <param name="roomId">Room identifier.</param>
        /// <param name="groupId">Group identifier.</param>
        private bool JoinGroup(int roomId, int groupId)
        {
            if (tcpClient == null || !tcpClient.Connected)
            {
                return false;
            }

            // 发送请求 入组消息没有响应
            JoinGroupRequest joinGroupRequest = new JoinGroupRequest(roomId, groupId);
            BarragePacket packet = new BarragePacket(joinGroupRequest.ToString(), MessageType.CLIENT);
            NetworkStream ns = tcpClient.GetStream();
            ns.Write(packet.Bytes, 0, packet.Bytes.Length);
            ns.Flush();

            return true;
        }

        /// <summary>
        /// Keep sending keepalive packet.
        /// </summary>
        private void KeepAlive()
        {
            if (tcpClient == null || !tcpClient.Connected || !Running)
            {
                return;
            }

            // 发送请求 入组消息没有响应
            KeepAliveRequest request = new KeepAliveRequest();
            BarragePacket packet = new BarragePacket(request.ToString(), MessageType.CLIENT);
            NetworkStream ns = tcpClient.GetStream();
            ns.Write(packet.Bytes, 0, packet.Bytes.Length);
            ns.Flush();

            logger.Debug("Heart Beat......");
        }

        /// <summary>
        /// 循环接收消息
        /// </summary>
        private void Run()
        {
            if (tcpClient == null || !Running || !tcpClient.Connected)
            {
                return;
            }

            NetworkStream ns = tcpClient.GetStream();
            while (Running)
            {
                try
                {
                    // 接收登录请求回应
                    byte[] buffer = BlockRead(ns, 4); // 先读取包长度

                    // 判断是否停止运行
                    if (!Running)
                    {
                        break;
                    }

                    // 解析出包长度
                    int messageLength = BitConverter.ToInt32(buffer);

                    // 根据读取到的包长，接收剩下的包体
                    byte[] recvBuffer = BlockRead(ns, messageLength);

                    // 判断是否停止运行
                    if (!Running)
                    {
                        break;
                    }

                    // 解析响应
                    BarragePacket recvPacket = new BarragePacket(recvBuffer);

                    // 记录日志
                    logger.Debug(recvPacket.ToString());

                    // 解析内容
                    Dictionary<string, string> keyValues = STTDeserializer.Deserialize(recvPacket.Data);
                    if (keyValues.ContainsKey("type"))
                    {
                        switch(keyValues["type"])
                        {
                            case "chatmsg":
                                ChatMessage chatMessage = MessageResolver.ResolveChatMessage(recvPacket.Data);
                                OnChat?.Invoke(chatMessage);
                                break;
                            case "onlinegift":
                                OnlineGiftMessage onlineGiftMessage = MessageResolver.ResolveOnlineGiftMessage(recvPacket.Data);
                                OnOnlineGift?.Invoke(onlineGiftMessage);
                                break;
                            case "dgb":
                                DgbMessage dgbMessage = MessageResolver.ResolveDgbMessage(recvPacket.Data);
                                OnDgb?.Invoke(dgbMessage);
                                break;
                            case "uenter":
                                UserEnterMessage userEnterMessage = MessageResolver.ResolveUserEnterMessage(recvPacket.Data);
                                OnUserEnter?.Invoke(userEnterMessage);
                                break;
                            case "bc_buy_deserve":
                                BcBuyDeserveMessage bcBuyDeserveMessage = MessageResolver.ResolveBcBuyDeserveMessage(recvPacket.Data);
                                OnBcBuyDeserve?.Invoke(bcBuyDeserveMessage);
                                break;
                            case "rss":
                                RssMessage rssMessage = MessageResolver.ResolveRssMessage(recvPacket.Data);
                                OnRss?.Invoke(rssMessage);
                                break;
                            case "ranklist":
                                RankListMessage rankListMessage = MessageResolver.ResolveRankListMessage(recvPacket.Data);
                                OnRankListBroadcast?.Invoke(rankListMessage);
                                break;
                            case "ssd":
                                SsdMessage ssdMessage = MessageResolver.ResolveSsdMessage(recvPacket.Data);
                                OnSsd?.Invoke(ssdMessage);
                                break;
                        }
                    }
                    else
                    {
                        logger.Error("Unsupported Message! " + recvPacket.ToString());
                    }
                }
                catch (IOException e)
                {
                    logger.Error(e);

                    // 连接断开了
                    Running = false;
                    logger.Error("Connection Lose.");
                }
                catch (Exception e)
                {
                    logger.Error(e);
                }
            }
        }

        /// <summary>
        /// 从Stream中读取指定大小的字节
        /// </summary>
        /// <param name="ns">NetworkStream</param>
        /// <param name="size">Read Size</param>
        /// <returns>Required Bytes</returns>
        private byte[] BlockRead(NetworkStream ns, int size)
        {
            byte[] bytes = new byte[size];

            for (int i = 0; i < size; ++i)
            {
                int b = ns.ReadByte();
                if (b == -1)
                {
                    return null;
                }

                bytes[i] = (byte)b;
            }

            return bytes;
        }

        /// <summary>
        /// Gets or sets the douyu barrage API address.
        /// </summary>
        /// <value>The douyu barrage API address.</value>
        public string DouyuBarrageApiAddress { set; get; }

        /// <summary>
        /// Gets or sets the douyu barrage API port.
        /// </summary>
        /// <value>The douyu barrage API port.</value>
        public int DouyuBarrageApiPort { set; get; }

        /// <summary>
        /// Gets or sets the room identifier.
        /// </summary>
        /// <value>The room identifier.</value>
        public int RoomId { set; get; }

        /// <summary>
        /// Gets or sets the group identifier.
        /// </summary>
        /// <value>The group identifier.</value>
        public int GroupId { set; get; }

        /// <summary>
        /// Gets or sets the running flag.
        /// </summary>
        /// <value>Whether running.</value>
        public bool Running { set; get; }

        /// <summary>
        /// 与斗鱼弹幕服务器通信的socket
        /// </summary>
        private TcpClient tcpClient;

        /// <summary>
        /// 定时器，定时发送心跳包
        /// </summary>
        private Timer keepAliveTimer;

        /// <summary>
        /// 持续接收消息
        /// </summary>
        private Task messageLookTask;
        
        /// <summary>
        /// Login Room Succeed Event
        /// </summary>
        public event Action<LoginResponseMessage> OnLoginSucceed;

        /// <summary>
        /// Chat Message Event
        /// </summary>
        public event Action<ChatMessage> OnChat;

        /// <summary>
        /// 在线鱼丸暴击
        /// </summary>
        public event Action<OnlineGiftMessage> OnOnlineGift;

        /// <summary>
        /// 赠送礼物
        /// </summary>
        public event Action<DgbMessage> OnDgb;

        /// <summary>
        /// 用户进入房间
        /// </summary>
        public event Action<UserEnterMessage> OnUserEnter;

        /// <summary>
        /// 用户购买酬勤
        /// </summary>
        public event Action<BcBuyDeserveMessage> OnBcBuyDeserve;

        /// <summary>
        /// 主播开关播提醒
        /// </summary>
        public event Action<RssMessage> OnRss;

        /// <summary>
        /// 广播排行榜信息
        /// </summary>
        public event Action<RankListMessage> OnRankListBroadcast;

        /// <summary>
        /// 超级弹幕
        /// </summary>
        public event Action<SsdMessage> OnSsd;
    }
}
