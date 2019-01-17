namespace DouyuBarrage.Utils
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
}
