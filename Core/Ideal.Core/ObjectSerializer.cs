using System;
using System.Text;
using Newtonsoft.Json;

namespace Ideal.Core
{
    public static class ObjectSerializer
    {
        public static byte[] Serialize(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var json = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(json);
        }

        public static object Deserialize(this byte[] arrBytes, Type type)
        {
            var json = Encoding.UTF8.GetString(arrBytes);
            return JsonConvert.DeserializeObject(json, type);
        }

        public static T Deserialize<T>(this byte[] arrBytes)
        {
            var json = Encoding.UTF8.GetString(arrBytes);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string DeSerializeText(this byte[] arrBytes)
        {
            return Encoding.UTF8.GetString(arrBytes);
        }
    }
}