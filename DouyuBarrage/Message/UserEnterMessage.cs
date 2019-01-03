using System;
using System.Collections.Generic;
using System.Text;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 用户进房通知消息
    /// </summary>
    public class UserEnterMessage
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        public int RoomId { set; get; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public int GroupId { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { set; get; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 用户头像.
        /// </summary>
        public string Icon { set; get; }

        /// <summary>
        /// 贵族等级
        /// </summary>
        public int NobleLevel { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
