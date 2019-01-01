using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 弹幕消息.
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// 弹幕组ID.
        /// </summary>
        public int GroupId { set; get; }

        /// <summary>
        /// 房间ID.
        /// </summary>
        public int RoomId { set; get; }

        /// <summary>
        /// 发送者UID.
        /// </summary>
        public string UserId { set; get; }

        /// <summary>
        /// 昵称.
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 弹幕内容.
        /// </summary>
        public string Text { set; get; }

        /// <summary>
        /// 弹幕唯一ID.
        /// </summary>
        public string ChatId { set; get; }

        /// <summary>
        /// 用户等级.
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 用户头像.
        /// </summary>
        public string Icon { set; get; }

        /// <summary>
        /// 主播等级.
        /// </summary>
        public int AnchorLevel { set; get; }

        /// <summary>
        /// 是否粉丝弹幕标记
        /// </summary>
        public bool IsFans { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
