using System;
using System.IO;
using System.Text;

namespace DouyuBarrage
{
    static public class Utils
    {
        /// <summary>
        /// Test byte order whether is big endin.
        /// </summary>
        /// <returns><c>true</c>, if byte order is  big endin, <c>false</c> otherwise.</returns>
        static public bool IsBigEndin()
        {
            Int32 number = 0x12345678;
            number = number >> 16; // 右移16位，16-32位移动到0-16位，高位补0
            if (number == 0x1234) // 如果剩下的是0x1234，说明高位值保存在高位地址，是小端子节序
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Builds the douyu barrage package.
        /// </summary>
        /// <returns>The douyu barrage package.</returns>
        /// <param name="content">Content.</param>
        static public byte[] BuildDouyuBarragePackage(string content)
        {
            // 将消息内容转化成byte[]
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);

            // 计算消息长度 = 消息长度（4）+ 消息类型（4）+ 真实消息长度 + 结尾标识长度（1）
            Int32 messageLength = 4 + 4 + contentBytes.Length + 1;

            // 组装TCP包
            MemoryStream ms = new MemoryStream();

            // 写第一个消息长度
            byte[] byteBuffer = BitConverter.GetBytes(messageLength);
            ms.Write(byteBuffer, 0, 4);

            // 写第二个消息长度
            ms.Write(byteBuffer, 0, 4);

            // 写消息类型
            Int16 messageType = 689;
            byteBuffer = BitConverter.GetBytes(messageType);
            ms.Write(byteBuffer, 0, 2);

            // 写加密字段
            ms.WriteByte(0x00);

            // 写保留字段
            ms.WriteByte(0x00);

            // 写消息内容
            ms.Write(contentBytes, 0, contentBytes.Length);

            // 写数据结尾标识
            ms.WriteByte((byte)'\0');

            return ms.ToArray();
        }
    }
}
