﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DouyuBarrage.Message
{
    public class HeartBeatMessage
    {
        /// <summary>
        /// 原始数据
        /// </summary>
        public Dictionary<string, string> Original { set; get; }
    }
}
