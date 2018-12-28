using DouyuBarrage.Utils;
using System;
using System.Collections.Generic;

namespace DouyuBarrage.Response
{
    public class LoginResponse
    {
        /// <summary>
        /// 表示为登陆消息，固定为loginres
        /// </summary>
        public string Type { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { set; get; }

        /// <summary>
        /// 房间权限组
        /// </summary>
        public int RoomGroup { set; get; }

        /// <summary>
        /// 平台权限组
        /// </summary>
        public int PlatformGroup { set; get; }

        /// <summary>
        /// 会话ID
        /// </summary>
        public int SessionId { set; get; }

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
        /// 是否违规
        /// </summary>
        public bool IsIllegal { set; get; }

        /// <summary>
        /// 违规提醒内容
        /// </summary>
        public string IllegalContentTips { set; get; }

        /// <summary>
        /// 违规提醒开始时间戳
        /// </summary>
        public long IllegalTimestamp { set; get; }

        /// <summary>
        /// 系统当前时间
        /// </summary>
        public string Now { set; get; }

        /// <summary>
        /// 手机绑定提示
        /// </summary>
        public string PhoneTips { set; get; }

        /// <summary>
        /// 邮箱绑定提示
        /// </summary>
        public string EmailTips { set; get; }

        /// <summary>
        /// 认证类型
        /// </summary>
        public string IdentificationType { set; get; }

        /// <summary>
        /// 认证状态
        /// </summary>
        public string IdentificationTypeStat { set; get; }

        /// <summary>
        /// 是否需要手机验证
        /// </summary>
        public bool NeedPhoneVerify { set; get; }

        /// <summary>
        /// 最高酬勤等级
        /// </summary>
        public int BestDiligenceLevel { set; get; }

        /// <summary>
        /// 当前酬勤等级
        /// </summary>
        public int CurLevel { set; get; }

        /// <summary>
        /// 观看房间需要的条件
        /// </summary>
        public string NeedRoomCondition { set; get; }

        /// <summary>
        /// 是否进房隐身
        /// </summary>
        public bool IsHiden { set; get; }

        /// <summary>
        /// 服务ID
        /// </summary>
        public string ServerId { set; get; }

        /// <summary>
        /// 拓展字段，一般不使用，可忽略
        /// </summary>
        public string Sahf { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="response">经过STT序列化算法序列化后的返回结果</param>
        public LoginResponse(string response)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(response);
            if (keyValues != null)
            {
                Type = keyValues.GetValueOrDefault("type", "loginres");
                UserId = keyValues.GetValueOrDefault("userid");
                RoomGroup = Convert.ToInt32(keyValues.GetValueOrDefault("roomgroup"));
                PlatformGroup = Convert.ToInt32(keyValues.GetValueOrDefault("pg"));
                SessionId = Convert.ToInt32(keyValues.GetValueOrDefault("sessionid"));
                UserName = keyValues.GetValueOrDefault("username");
                NickName = keyValues.GetValueOrDefault("nickname");
                LiveStat = keyValues.GetValueOrDefault("live_stat");
                IsIllegal = keyValues.GetValueOrDefault("is_illegal") != "0";
                IllegalContentTips = keyValues.GetValueOrDefault("ill_ct");
                IllegalTimestamp = Convert.ToInt64(keyValues.GetValueOrDefault("now"));
                PhoneTips = keyValues.GetValueOrDefault("ps");
                EmailTips = keyValues.GetValueOrDefault("es");
                IdentificationType = keyValues.GetValueOrDefault("it");
                IdentificationTypeStat = keyValues.GetValueOrDefault("its");
                NeedPhoneVerify = keyValues.GetValueOrDefault("npv") != "0";
                BestDiligenceLevel = Convert.ToInt32(keyValues.GetValueOrDefault("best_dlev"));
                CurLevel = Convert.ToInt32(keyValues.GetValueOrDefault("cur_lev"));
                NeedRoomCondition = keyValues.GetValueOrDefault("nrc");
                IsHiden = keyValues.GetValueOrDefault("ih") != "0";
                ServerId = keyValues.GetValueOrDefault("sid");
                Sahf = keyValues.GetValueOrDefault("sahf");
            }
        }
    }
}
