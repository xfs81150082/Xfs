using Newtonsoft.Json;
namespace Xfs
{
    public static class XfsJsonHelper
    {
        public static T ToObject<T>(string str)
        {
            //Json.NET反序列化
            T t = JsonConvert.DeserializeObject<T>(str);
            return t;
        }
        public static object ToObject(string str,object instance )
        {
            //Json.NET反序列化
            object t = JsonConvert.DeserializeAnonymousType(str, instance);
            return t;
        }
        public static string ToJson<T>(T value)
        {
            //Json.NET序列化
            string jsonData = JsonConvert.SerializeObject(value);
            return jsonData;
        }
    }
}
