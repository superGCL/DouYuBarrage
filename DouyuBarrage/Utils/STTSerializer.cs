﻿using System.Collections.Generic;
using System.Text;

namespace DouyuBarrage.Utils
{
    /// <summary>
    /// STT Serializer.
    /// </summary>
    static public class STTSerializer
    {
        /// <summary>
        /// key和value之间的分割符
        /// </summary>
        private static readonly string KEY_VALUE_SPERATOR = "@=";

        /// <summary>
        /// 键值对之间使用/分割
        /// </summary>
        private static readonly string ARRAY_SPERATOR = "/";

        /// <summary>
        /// 使用STT序列化算法，序列化内容
        /// </summary>
        /// <typeparam name="T1">key的类型</typeparam>
        /// <typeparam name="T2">value的类型</typeparam>
        /// <param name="data">需要序列化的数据</param>
        /// <returns>序列化后的内容</returns>
        static public string Serialize<T1, T2>(Dictionary<T1, T2> data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<T1, T2> pair in data)
            {

                sb.Append(Escape(pair.Key.ToString()) + KEY_VALUE_SPERATOR + Escape(pair.Value.ToString()) + ARRAY_SPERATOR);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Escape the specified str. <br/>
        /// / => @S, @ => @A
        /// </summary>
        /// <returns>The str after escape.</returns>
        /// <param name="str">String.</param>
        static private string Escape(string str)
        {
            str = str.Replace("@", "@A");
            str = str.Replace("/", "@S");
            return str;
        }
    }
}
