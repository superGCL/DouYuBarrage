using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 房间内礼物广播
    /// </summary>
    public class SpbcMessage
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
        /// 赠送者昵称
        /// </summary>
        public string SenderNickName { set; get; }

        /// <summary>
        /// 受赠者昵称
        /// </summary>
        public string DeservedNickName { set; get; }

        /// <summary>
        /// 礼物名称
        /// </summary>
        public string GiftName { set; get; }

        /// <summary>
        /// 礼物数量
        /// </summary>
        public int GiftCount { set; get; }

        /// <summary>
        /// 赠送房间ID
        /// </summary>
        public int DeservedRoomId { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
