using System;
using System.Collections.Generic;
using System.Text;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 领取在线鱼丸暴击消息
    /// </summary>
    public class OnlineGiftMessage
    {
        /// <summary>
        /// 房间ID
        /// </summary>
        public int RoomId { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { set; get; }

        /// <summary>
        /// 分组ID
        /// </summary>
        public int GroupId { set; get; }

        /// <summary>
        /// 鱼丸数
        /// </summary>
        public int Sil { set; get; }

        /// <summary>
        /// 领取鱼丸的等级
        /// </summary>
        public int If { set; get; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 鱼丸之刃的倍率
        /// </summary>
        public int Ur { set; get; }

        /// <summary>
        /// 用户等级
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
