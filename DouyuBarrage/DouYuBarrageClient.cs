using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

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
            byte[] loginRequestPackage = Utils.BuildDouyuBarragePackage(loginRequest.ToString());
            NetworkStream ns = tcpClient.GetStream();
            ns.Write(loginRequestPackage, 0, loginRequestPackage.Length);
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

            // 解析包内容
            byte[] buffer = new byte[messageLength];
            MemoryStream ms = new MemoryStream(recvBuffer, 0, recvBuffer.Length);
            ms.Read(buffer, 0, 4); // 读取消息长度

            messageLength = BitConverter.ToInt32(buffer);
            Console.WriteLine("Parse message length 1 " + messageLength);

            ms.Read(buffer, 0, 2); // 读取消息类型
            int messageType = BitConverter.ToInt16(buffer);
            Console.WriteLine("Parse message type " + messageType);

            ms.Read(buffer, 0, 2); // 读取加密字段和保留字段，但是不解析

            ms.Read(buffer, 0, messageLength - 4 - 4); // 读取数据部
            string data = Encoding.UTF8.GetString(buffer, 0, messageLength - 4 - 4);
            Console.WriteLine("Parse message data " + data);

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
                LoginRequest loginRequest = new LoginRequest(roomId);
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
