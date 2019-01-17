using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 房间内用户抢到道具消息
    /// </summary>
    public class GpbcMessage
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
        /// 抢到的道具数量
        /// </summary>
        public int Count { set; get; }

        /// <summary>
        /// 派发者ID
        /// </summary>
        public string SenderId { set; get; }

        /// <summary>
        /// 派发者昵称
        /// </summary>
        public string SenderNickName { set; get; }

        /// <summary>
        /// 接收者ID
        /// </summary>
        public string ReceiverId { set; get; }

        /// <summary>
        /// 接收者昵称
        /// </summary>
        public string ReceiverNickName { set; get; }

        /// <summary>
        /// 宝箱类型
        /// </summary>
        public string RedPacketType { set; get; }

        /// <summary>
        /// 道具名称
        /// </summary>
        public string PacketName { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
