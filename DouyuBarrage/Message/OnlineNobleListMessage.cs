using DouyuBarrage.Utils;
using System;
using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 房间内贵族列表
    /// </summary>
    public class OnlineNobleListMessage
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
        /// 贵族数量
        /// </summary>
        public int Count { set; get; }

        /// <summary>
        /// 贵族列表
        /// </summary>
        public IList<UserInfo> NobleList
        {
            set
            { NobleList = value; }

            get
            {
                IList<UserInfo> userInfos = new List<UserInfo>();

                string sui = Original.GetValueOrDefault("nl");
                Dictionary<string, string> pairs = STTDeserializer.Deserialize(sui);
                foreach (var kv in pairs)
                {
                    Dictionary<string, string> pairs2 = STTDeserializer.Deserialize(kv.Key);
                    UserInfo userInfo = new UserInfo();
                    userInfo.Id = Convert.ToInt32(pairs2.GetValueOrDefault("uid"));
                    userInfo.NickName = pairs2.GetValueOrDefault("nn");
                    userInfo.Icon = pairs2.GetValueOrDefault("icon");
                    userInfo.Level = Convert.ToInt32(pairs2.GetValueOrDefault("lv"));

                    userInfos.Add(userInfo);
                }

                return userInfos;
            }
        }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
