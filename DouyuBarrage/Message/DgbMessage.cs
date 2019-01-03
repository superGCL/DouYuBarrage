using System;
using System.Collections.Generic;
using System.Text;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 赠送礼物消息
    /// </summary>
    public class DgbMessage
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
        /// 礼物ID
        /// </summary>
        public int GiftId { set; get; }

        /// <summary>
        /// 礼物显示样式
        /// </summary>
        public string GiftStyle { set; get; }

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
        /// 礼物个数
        /// </summary>
        public int GiftCount { set; get; }

        /// <summary>
        /// 连击数量
        /// </summary>
        public int Hits { set; get; }

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
