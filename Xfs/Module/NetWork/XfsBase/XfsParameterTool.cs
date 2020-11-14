using Newtonsoft.Json;
using System.Collections.Generic;
namespace Xfs
{
    public static class XfsParameterTool
    {
        public static XfsParameter ToJsonParameter<T>(TenCode ten, ElevenCode eleven, string key, T value)
        {
            XfsParameter parameter = new XfsParameter();
            string json = XfsJsonHelper.ToString<T>(value);
            parameter.TenCode = ten;
            parameter.ElevenCode = eleven;
            parameter.Parameters.Add(key, json);
            return parameter;
        }
        public static void AddJsonParameter<T>(XfsParameter parameter, string key, T value)
        {
            object obj;
            bool yes = parameter.Parameters.TryGetValue(key, out obj);
            if (yes) { parameter.Parameters.Remove(key); }
            string json = XfsJsonHelper.ToString<T>(value);
            parameter.Parameters.Add(key, json);
        }
        public static T GetJsonValue<T>(XfsParameter parameter, string key)
        {
            object obj = null;
            parameter.Parameters.TryGetValue(key, out obj);
            string json = (string)obj;
            //Json.NET反序列化
            T t = JsonConvert.DeserializeObject<T>(json);
            return t;
        }

        public static XfsParameter ToParameter<T>(TenCode ten, ElevenCode eleven, string key, T value)
        {
            XfsParameter parameter = new XfsParameter();
            parameter.TenCode = ten;
            parameter.ElevenCode = eleven;
            parameter.Parameters.Add(key, value);
            return parameter;
        }
        public static XfsParameter ToParameter(TenCode ten, ElevenCode eleven)
        {
            XfsParameter parameter = new XfsParameter();
            parameter.TenCode = ten;
            parameter.ElevenCode = eleven;
            return parameter;
        } 
        public static void AddParameter<T>(XfsParameter parameter, string key, T value)
        {
            object obj;
            bool yes = parameter.Parameters.TryGetValue(key, out obj);
            if (yes) { parameter.Parameters.Remove(key); }
            parameter.Parameters.Add(key, value);
        }
        public static T GetValue<T>(XfsParameter parameter, string key)
        {
            object obj = null;
            parameter.Parameters.TryGetValue(key, out obj);
            T tp = (T)obj;
            return tp;
        }
        public static T OutOfDictionary<T>(string key, Dictionary<string, T> dictionary)
        {
            T val;
            bool yes = dictionary.TryGetValue(key, out val);
            if (yes)
            {
                return val;
            }
            return default(T);
        }
    }
}