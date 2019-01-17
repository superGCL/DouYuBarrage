using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 主播回来消息
    /// </summary>
    public class AnchorBackMessage
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        public int RoomId { set; get; }

        /// <summary>
        /// 弹幕ID
        /// </summary>
        public int GroupId { set; get; }

        /// <summary>
        /// 主播ID
        /// </summary>
        public int AnchorId { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
