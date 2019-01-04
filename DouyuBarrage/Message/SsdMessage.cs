using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 超级弹幕消息
    /// </summary>
    public class SsdMessage
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        public int RoomId { set; get; }

        /// <summary>
        /// 弹幕分组ID
        /// </summary>
        public int GroupId { set; get; }

        /// <summary>
        /// 超级弹幕ID
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 跳转房间ID
        /// </summary>
        public int JumpRoomId { set; get; }

        /// <summary>
        /// 超级弹幕内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 跳转URL
        /// </summary>
        public string Url { set; get; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public int ClientType { set; get; }

        /// <summary>
        /// 跳转类型
        /// </summary>
        public int JumpType { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
