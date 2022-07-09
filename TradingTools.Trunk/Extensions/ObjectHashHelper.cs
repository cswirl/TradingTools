using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingTools.Trunk.Extensions
{
    public class ObjectHashHelper
    {
    }

    public static class MD5HashHelper
    {
        public static string CreateHash(this string str)
        {
            string hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = BitConverter.ToString(
                  md5.ComputeHash(Encoding.UTF8.GetBytes(str))
                ).Replace("-", String.Empty);
            }
            return hash;
        }

        public static string CreateHash(this object obj)
        {
            var str = obj.ToJSON();
            string hash;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                hash = BitConverter.ToString(
                  md5.ComputeHash(Encoding.UTF8.GetBytes(str))
                ).Replace("-", String.Empty);
            }
            return hash;
        }

        public static bool CompareWithHash(this string str, string hash)
        {
            string strHash = CreateHash(str);

            return strHash.Equals(hash, StringComparison.Ordinal);
        }
    }

    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            return json;
        }

        public static T JSONToObject<T>(this string json)
        {
            Object obj = JsonConvert.DeserializeObject<T>(json);
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}
