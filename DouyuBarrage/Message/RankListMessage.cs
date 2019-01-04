using DouyuBarrage.Utils;
using System;
using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 榜单项
    /// </summary>
    public class RankListItem
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { set; get; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Icon { set; get; }

        /// <summary>
        /// 当前排名
        /// </summary>
        public int CurrentRank { set; get; }

        /// <summary>
        /// 上次排名
        /// </summary>
        public int LastRank { set; get; }

        /// <summary>
        /// 排名变化 -1：下降 0：持平 1-上升
        /// </summary>
        public int RankStatus { set; get; }

        /// <summary>
        /// 当前贡献值
        /// </summary>
        public int Gold { set; get; }
    }

    public class RankListMessage
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
        /// 排行榜更新时间戳
        /// </summary>
        public long Timestamp { set; get; }

        /// <summary>
        /// 排行榜更新序号
        /// </summary>
        public int Sequence { set; get; }

        /// <summary>
        /// 总榜
        /// </summary>
        public IList<RankListItem> AllRankList
        {
            get
            {
                // 构造结果集合
                List<RankListItem> list = new List<RankListItem>();

                // 1 先获取总榜的内容
                string list_all = Original.GetValueOrDefault("list_all");

                // 2 解析总榜的内容（内容为数组，因此解析出来的map只有key，而没有value）
                Dictionary<string, string> keyValues = STTDeserializer.Deserialize(list_all);
                foreach (var kv in keyValues)
                {
                    Dictionary<string, string> pairs = STTDeserializer.Deserialize(kv.Key);
                    RankListItem item = new RankListItem
                    {
                        UserId = Convert.ToInt32(pairs.GetValueOrDefault("uid")),
                        NickName = pairs.GetValueOrDefault("nickname"),
                        Level = Convert.ToInt32(pairs.GetValueOrDefault("level")),
                        Icon = pairs.GetValueOrDefault("icon"),
                        CurrentRank = Convert.ToInt32(pairs.GetValueOrDefault("crk")),
                        LastRank = Convert.ToInt32(pairs.GetValueOrDefault("lrk")),
                        RankStatus = Convert.ToInt32(pairs.GetValueOrDefault("rs")),
                        Gold = Convert.ToInt32(pairs.GetValueOrDefault("gold"))
                    };

                    list.Add(item);
                }

                return list;
            }
        }

        /// <summary>
        /// 周榜
        /// </summary>
        public IList<RankListItem> WeekRankList
        {
            get
            {
                // 构造结果集合
                List<RankListItem> list = new List<RankListItem>();

                // 1 先获取总榜的内容
                string list_all = Original.GetValueOrDefault("list");

                // 2 解析总榜的内容（内容为数组，因此解析出来的map只有key，而没有value）
                Dictionary<string, string> keyValues = STTDeserializer.Deserialize(list_all);
                foreach (var kv in keyValues)
                {
                    Dictionary<string, string> pairs = STTDeserializer.Deserialize(kv.Key);
                    RankListItem item = new RankListItem
                    {
                        UserId = Convert.ToInt32(pairs.GetValueOrDefault("uid")),
                        NickName = pairs.GetValueOrDefault("nickname"),
                        Level = Convert.ToInt32(pairs.GetValueOrDefault("level")),
                        Icon = pairs.GetValueOrDefault("icon"),
                        CurrentRank = Convert.ToInt32(pairs.GetValueOrDefault("crk")),
                        LastRank = Convert.ToInt32(pairs.GetValueOrDefault("lrk")),
                        RankStatus = Convert.ToInt32(pairs.GetValueOrDefault("rs")),
                        Gold = Convert.ToInt32(pairs.GetValueOrDefault("gold"))
                    };

                    list.Add(item);
                }

                return list;
            }
        }

        /// <summary>
        /// 日榜
        /// </summary>
        public IList<RankListItem> DayRankList
        {
            get
            {
                // 构造结果集合
                List<RankListItem> list = new List<RankListItem>();

                // 1 先获取总榜的内容
                string list_all = Original.GetValueOrDefault("list_day");

                // 2 解析总榜的内容（内容为数组，因此解析出来的map只有key，而没有value）
                Dictionary<string, string> keyValues = STTDeserializer.Deserialize(list_all);
                foreach (var kv in keyValues)
                {
                    Dictionary<string, string> pairs = STTDeserializer.Deserialize(kv.Key);
                    RankListItem item = new RankListItem
                    {
                        UserId = Convert.ToInt32(pairs.GetValueOrDefault("uid")),
                        NickName = pairs.GetValueOrDefault("nickname"),
                        Level = Convert.ToInt32(pairs.GetValueOrDefault("level")),
                        Icon = pairs.GetValueOrDefault("icon"),
                        CurrentRank = Convert.ToInt32(pairs.GetValueOrDefault("crk")),
                        LastRank = Convert.ToInt32(pairs.GetValueOrDefault("lrk")),
                        RankStatus = Convert.ToInt32(pairs.GetValueOrDefault("rs")),
                        Gold = Convert.ToInt32(pairs.GetValueOrDefault("gold"))
                    };

                    list.Add(item);
                }

                return list;
            }
        }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
