using DouyuBarrage.Utils;
using System;
using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Icon { set; get; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public string RegisterTime { set; get; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 用户经验值
        /// </summary>
        public int Experience { set; get; }

        /// <summary>
        /// 关注状态 0-未关注 1-关注
        /// </summary>
        public bool Focus { set; get; }
    }

    /// <summary>
    /// 用户赠送酬勤通知消息
    /// </summary>
    public class BcBuyDeserveMessage
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
        /// 用户等级
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 赠送数量
        /// </summary>
        public int Count { set; get; }

        /// <summary>
        /// 连击数量
        /// </summary>
        public int Hits { set; get; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            set
            { UserInfo = value; }

            get
            {
                UserInfo userInfo = new UserInfo();
                string sui = Original.GetValueOrDefault("sui");
                Dictionary<string, string> pairs = STTDeserializer.Deserialize(sui);
                userInfo.Id = Convert.ToInt32(pairs.GetValueOrDefault("id"));
                userInfo.Name = pairs.GetValueOrDefault("name");
                userInfo.NickName = pairs.GetValueOrDefault("nick");
                userInfo.Icon = pairs.GetValueOrDefault("ic");
                userInfo.RegisterTime = pairs.GetValueOrDefault("rt");
                userInfo.Experience = Convert.ToInt32(pairs.GetValueOrDefault("experience"));
                userInfo.Level = Convert.ToInt32(pairs.GetValueOrDefault("level"));
                userInfo.Focus = pairs.GetValueOrDefault("fs") == "1";

                return userInfo;
            }
        }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
