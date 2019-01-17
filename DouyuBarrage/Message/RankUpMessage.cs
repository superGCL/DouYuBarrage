using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 房间内Top10发生变化消息
    /// </summary>
    public class RankUpMessage
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
        /// 发送者UID.
        /// </summary>
        public string UserId { set; get; }

        /// <summary>
        /// 目标房间ID
        /// </summary>
        public int DestRoomId { set; get; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// top10榜的类型 1-周榜 2-总榜 4-日榜
        /// </summary>
        public string RankType { set; get; }

        /// <summary>
        /// 上升后的排名
        /// </summary>
        public int RankNow { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
