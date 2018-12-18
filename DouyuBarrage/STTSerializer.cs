using System.Collections.Generic;
using System.Text;

namespace DouyuBarrage
{
    /// <summary>
    /// STT Serializer.
    /// </summary>
    static public class STTSerializer
    {
        private static readonly string KEY_VALUE_SPERATOR = "@="; // key和value之间的分割符
        private static readonly string ARRAY_SPERATOR = "/"; // 键值对之间使用/分割

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
