using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 房间内用户抢红包消息
    /// </summary>
    public class GgbbMessage
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
        /// 抢到的鱼丸的类型0-普通运气 1-手气王 2-暴击X4 3-暴击X3 4-暴击X2 5-暴击X1
        /// </summary>
        public int GiftType { set; get; }

        /// <summary>
        /// 鱼丸数量
        /// </summary>
        public int Count { set; get; }

        /// <summary>
        /// 礼包产生者ID
        /// </summary>
        public int SenderId { set; get; }

        /// <summary>
        /// 礼包产生者昵称
        /// </summary>
        public string SenderNickName { set; get; }

        /// <summary>
        /// 抢包者ID
        /// </summary>
        public int DeservedId { set; get; }

        /// <summary>
        /// 抢包者昵称
        /// </summary>
        public string DeservedNickName { set; get; }

        /// <summary>
        /// 礼包类型
        /// </summary>
        public int Type { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
