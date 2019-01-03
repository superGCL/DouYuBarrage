using System;
using System.Collections.Generic;
using System.Text;

namespace DouyuBarrage.Message
{
    public class RankListItem
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// 当前排名
        /// </summary>
        public int CurrentRank { set; get; }

        /// <summary>
        /// 排名变化 -1：下降 0：持平 1-上升
        /// </summary>
        public int RankStatus { set; get; }

        /// <summary>
        /// 当前贡献值
        /// </summary>
        public int GoldCost { set; get; }
    }

    public class RankListMessage
    {
        public int RoomId { set; get; }
        public int GroupId { set; get; }
        public long Timestamp { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
