using System;
using System.Net.Sockets;
using log4net;

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

            LogoutRequest logoutRequest = new LogoutRequest();
            BarragePacket packet = new BarragePacket(logoutRequest.ToString(), MessageType.CLIENT);
            tcpClient.GetStream().Write(packet.Bytes, 0, packet.Bytes.Length);
            tcpClient.GetStream().Flush();

            tcpClient.Close();
            tcpClient = null;
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

        private TcpClient tcpClient;
    }
}
