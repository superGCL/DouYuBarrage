using log4net;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Timers;

namespace DouyuBarrage
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
            byte[] recvBuffer = new byte[512];
            int readCnt = ns.Read(recvBuffer, 0, 4); // 先读取包长度
            if (readCnt != 4)
            {
                throw new Exception($"Read Error! Expected 4bytes, but {readCnt}");
            }

            // 解析出包长度
            int messageLength = BitConverter.ToInt32(recvBuffer);

            // 根据读取到的包长，接收剩下的包体
            recvBuffer = new byte[messageLength];
            readCnt = ns.Read(recvBuffer, 0, messageLength);
            if (readCnt != messageLength)
            {
                throw new Exception($"Read Error! Expected {messageLength}bytes, but {readCnt}");
            }

            // 解析响应
            BarragePacket recvPacket = new BarragePacket(recvBuffer);

            // 记录日志
            logger.Info(recvPacket.ToString());

            // 将返回结果解析成LoginResponse
            LoginResponse response = new LoginResponse(recvPacket.ToString());

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

            LogoutRequest logoutRequest = new LogoutRequest();
            BarragePacket packet = new BarragePacket(logoutRequest.ToString(), MessageType.CLIENT);
            tcpClient.GetStream().Write(packet.Bytes, 0, packet.Bytes.Length);
            tcpClient.GetStream().Flush();

            // 设置是否正在运行标志为false
            Running = false;

            // 停止心跳线程
            keepAliveTimer.Stop();

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

            logger.Info("Heart Beat......");
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
                    byte[] buffer = new byte[8];
                    int readCnt = ns.Read(buffer, 0, 4); // 先读取包长度
                    if (readCnt != 4)
                    {
                        throw new Exception($"Read Error! Expected 4 bytes, but {readCnt} bytes");
                    }

                    // 解析出包长度
                    int messageLength = BitConverter.ToInt32(buffer);

                    // 根据读取到的包长，接收剩下的包体
                    byte[] recvBuffer = new byte[messageLength];
                    readCnt = ns.Read(recvBuffer, 0, messageLength);
                    if (readCnt != messageLength)
                    {
                        throw new Exception($"Read Error! Expected {messageLength} bytes, but {readCnt} bytes");
                    }

                    // 解析响应
                    BarragePacket recvPacket = new BarragePacket(recvBuffer);

                    // 记录日志
                    logger.Info(recvPacket.ToString());
                }
                catch (Exception e)
                {
                    logger.Error(e.ToString());
                }
            }
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
    }
}
