using System.Collections.Generic;

namespace DouyuBarrage.Utils
{
    /// <summary>
    /// STT Deserializer.
    /// </summary>
    public class STTDeserializer
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
        /// 使用STT序列化算法，反序列化内容
        /// </summary>
        /// <param name="content">使用STT序列化算法序列化的内容</param>
        /// <returns>反序列后的结果Key和Value对</returns>
        public static Dictionary<string, string> Deserialize(string content)
        {
            Dictionary<string, string> keyValues = new Dictionary<string, string>();

            // 第一步 先用/分割
            string[] arr = content.Split(ARRAY_SPERATOR);
            if (arr.Length <= 0)
            {
                return keyValues;
            }

            // 第二步 对每一个keyValuePair再使用@=分割
            foreach (string kv in arr)
            {
                string[] arr2 = kv.Split(KEY_VALUE_SPERATOR);
                if (arr2.Length == 1) // value为空
                {
                    string key = Escape(arr2[0]);
                    keyValues.TryAdd(key, "");
                }
                else
                {
                    string key = Escape(arr2[0]);
                    string value = Escape(arr2[1]);
                    keyValues.TryAdd(key, value);
                }
            }

            return keyValues;
        }

        /// <summary>
        /// Escape the specified str. <br/>
        /// / => @S, @ => @A
        /// </summary>
        /// <returns>The str after escape.</returns>
        /// <param name="str">String.</param>
        private static string Escape(string str)
        {
            str = str.Replace("@A", "@");
            str = str.Replace("@S", "/");
            return str;
        }
    }
}
