using System;
using System.IO;
using System.Text;

namespace DouyuBarrage
{
    public enum MessageType
    {
        CLIENT = 689,
        SERVERT = 690,
        UNKNOWN = -1
    }

    public class BarragePacket
    {
        public BarragePacket(string content, MessageType type)
        {
            Data = content;

            // 将消息内容转化成byte[]
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);

            // 计算消息长度 = 消息长度（4）+ 消息类型(2) + 加密字段(1) + 保留字段(1) + 真实消息长度 + 结尾标识长度（1）
            MessageLength = 4 + 4 + contentBytes.Length + 1;

            // 组装TCP包
            MemoryStream ms = new MemoryStream();

            // 写第一个消息长度
            byte[] byteBuffer = BitConverter.GetBytes(MessageLength);
            ms.Write(byteBuffer, 0, 4);

            // 写第二个消息长度
            ms.Write(byteBuffer, 0, 4);

            // 写消息类型
            byteBuffer = BitConverter.GetBytes((Int16)type);
            ms.Write(byteBuffer, 0, 2);

            // 写加密字段
            ms.WriteByte(0x00);
            Encrypt = 0x00;

            // 写保留字段
            ms.WriteByte(0x00);
            Reserved = 0x00;

            // 写消息内容
            ms.Write(contentBytes, 0, contentBytes.Length);

            // 写数据结尾标识
            ms.WriteByte((byte)'\0');

            Bytes = ms.ToArray();
        }

        /// <summary>
        /// Gets or sets the length of the message.
        /// </summary>
        /// <value>The length of the message.</value>
        public int MessageLength { set; get; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public MessageType MessageType { set; get; }

        /// <summary>
        /// Gets or sets the encrypt.
        /// </summary>
        /// <value>The encrypt.</value>
        public byte Encrypt { set; get; }

        /// <summary>
        /// Gets or sets the reserved.
        /// </summary>
        /// <value>The reserved.</value>
        public byte Reserved { set; get; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public string Data { set; get; }

        /// <summary>
        /// Gets or sets the bytes.
        /// </summary>
        /// <value>The bytes.</value>
        public byte[] Bytes { set; get; }
    }
}
