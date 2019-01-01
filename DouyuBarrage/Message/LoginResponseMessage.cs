using DouyuBarrage.Utils;
using System;
using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    /// <summary>
    /// 登录响应消息.
    /// </summary>
    public class LoginResponseMessage
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 直播状态
        /// </summary>
        public string LiveStat { set; get; }

        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
