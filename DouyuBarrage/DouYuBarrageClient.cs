using System;
using System.Net.Sockets;

namespace DouyuBarrage
{
    /// <summary>
    /// DouYu barrage client.
    /// </summary>
    public class DouYuBarrageClient
    {
        public DouYuBarrageClient()
        {
            // 默认的斗鱼弹幕api接口地址
            DouyuBarrageApiAddress = "openbarrage.douyutv.com";
            DouyuBarrageApiPort = 8601;
        }

        public void Connect(int roomId, int groupId = -9999)
        {
            // 连接之斗鱼弹幕服务器
            tcpClient = new TcpClient();
            tcpClient.Connect(DouyuBarrageApiAddress, DouyuBarrageApiPort);
            Console.WriteLine("Connect OK!");

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
                throw new Exception("Read Error");
            }

            // 解析出包长度
            int messageLength = BitConverter.ToInt32(recvBuffer);

            // 根据读取到的包长，接收剩下的包体
            recvBuffer = new byte[messageLength];
            readCnt = ns.Read(recvBuffer, 0, messageLength);
            if (readCnt != messageLength)
            {
                throw new Exception("Read Error");
            }

            BarragePacket recvPacket = new BarragePacket(recvBuffer);
            Console.WriteLine(recvPacket.ToString());

            tcpClient.Close();
        }

        /// <summary>
        /// Login the specified roomId.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="roomId">Room identifier.</param>
        private bool Login(int roomId)
        {
            if (tcpClient != null && tcpClient.Connected)
            {
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
                    throw new Exception("Read Error");
                }

                // 解析出包长度
                int messageLength = BitConverter.ToInt32(recvBuffer);

                // 根据读取到的包长，接收剩下的包体
                recvBuffer = new byte[messageLength];
                readCnt = ns.Read(recvBuffer, 0, messageLength);
                if (readCnt != messageLength)
                {
                    throw new Exception("Read Error");
                }

                // 解析响应
                BarragePacket recvPacket = new BarragePacket(recvBuffer);

                // 记录日志
            }

            return false;
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
