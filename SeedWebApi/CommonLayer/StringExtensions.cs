using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace CommonLayer
{
    public static class StringExtensions
    {
        public static byte[] ToBytesFromBase64String(this string data)
        {
            return Convert.FromBase64String(data);
        }

        public static byte[] ToBytesUsingUTF8Encoding(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static byte[] ToBytesArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static string ToBase64String(this byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static byte[] ToUTF8Bytes(this string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }

        public static string ToStringFromUTF8Bytes(this byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static string ToSha1Hash(this string data)
        {
            using (var sha1Csp = new SHA1CryptoServiceProvider())
            {
                var hash = sha1Csp.ComputeHash(data.ToUTF8Bytes());
                return hash.Aggregate("", (c, b) => c + $"{b:x2}");
            }
        }

        public static T ToObjectFromJsonString<T>(this string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static string ToJsonStringFromObject<T>(this T data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }

}
