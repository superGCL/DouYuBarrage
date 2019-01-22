using DouyuBarrage.Utils;
using System;
using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 用户点赞推送通知消息
    /// </summary>
    public class RuclpMessage
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
        /// 用户信息
        /// </summary>
        public IList<UserInfo> UserInfoList
        {
            set
            { UserInfoList = value; }

            get
            {
                IList<UserInfo> userInfos = new List<UserInfo>();

                string sui = Original.GetValueOrDefault("ui_list");
                Dictionary<string, string> pairs = STTDeserializer.Deserialize(sui);
                foreach (var kv in pairs)
                {
                    Dictionary<string, string> pairs2 = STTDeserializer.Deserialize(kv.Key);
                    UserInfo userInfo = new UserInfo();
                    userInfo.Id = Convert.ToInt32(pairs2.GetValueOrDefault("uid"));
                    userInfo.NickName = pairs2.GetValueOrDefault("nn");
                    userInfo.Level = Convert.ToInt32(pairs2.GetValueOrDefault("level"));

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
