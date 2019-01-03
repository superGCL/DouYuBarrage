using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 房间开关播提醒
    /// </summary>
    public class RssMessage
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
        /// 直播状态
        /// </summary>
        public bool Living { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Code { set; get; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public string Notify { set; get; }

        /// <summary>
        /// 关播时间
        /// </summary>
        public string Endtime { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
