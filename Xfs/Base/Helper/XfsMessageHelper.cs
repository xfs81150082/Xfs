using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public static class XfsMessageHelper
    {   
        //public static string ToJsonMessage<T>(ushort opcode , T value)
        //{
        //    //XfsMessageInfo messageInfo = new XfsMessageInfo(opcode, value);
        //    string json = XfsJsonHelper.ToString<XfsMessageInfo>(messageInfo);
        //    return json;
        //}
        public static XfsMessageInfo GetMessageInfo(string value)
        {
            XfsMessageInfo messageInfo = JsonConvert.DeserializeObject<XfsMessageInfo>(value);
            return messageInfo;
        }
        //public static void AddJsonParameter<T>(XfsParameter parameter, string key, T value)
        //{
        //    object obj;
        //    bool yes = parameter.Parameters.TryGetValue(key, out obj);
        //    if (yes) { parameter.Parameters.Remove(key); }
        //    string json = XfsJsonHelper.ToString<T>(value);
        //    parameter.Parameters.Add(key, json);
        //}
        //public static T GetJsonValue<T>(XfsParameter parameter, string key)
        //{
        //    object obj = null;
        //    obj = parameter.Message as object;
        //    string json = (string)obj;
        //    //Json.NET反序列化
        //    T t = JsonConvert.DeserializeObject<T>(json);
        //    return t;
        //}

        public static XfsParameter ToParameter<T>(TenCode ten, ElevenCode eleven, T value)
        {
            XfsParameter parameter = new XfsParameter();
            parameter.TenCode = ten;
            parameter.ElevenCode = eleven;
            parameter.Message = value;
            return parameter;        }
        public static XfsParameter ToParameter(TenCode ten, ElevenCode eleven)
        {
            XfsParameter parameter = new XfsParameter();
            parameter.TenCode = ten;
            parameter.ElevenCode = eleven;
            return parameter;
        } 
        //public static void AddParameter<T>(XfsParameter parameter, string key, T value)
        //{
        //    object obj;
        //    bool yes = parameter.Parameters.TryGetValue(key, out obj);
        //    if (yes) { parameter.Parameters.Remove(key); }
        //    parameter.Parameters.Add(key, value);
        //}
        public static T GetValue<T>(XfsParameter parameter)
        {
            object obj = parameter.Message;
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
