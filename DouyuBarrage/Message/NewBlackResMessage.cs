using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 禁言操作结果消息
    /// </summary>
    public class NewBlackResMessage
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
        /// 错误码
        /// </summary>
        public int ErrorCode { set; get; }

        /// <summary>
        /// 操作者类型 0：普通用户 1：房管 2：主播 3：超管
        /// </summary>
        public int OperatorType { set; get; }

        /// <summary>
        /// 操作者昵称
        /// </summary>
        public string Operator { set; get; }

        /// <summary>
        /// 被禁言者昵称
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 禁言失效时间
        /// </summary>
        public string EndTime { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
