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
        public static XfsMessageInfo GetMessageInfo(string value)
        {
            XfsMessageInfo messageInfo = JsonConvert.DeserializeObject<XfsMessageInfo>(value);
            return messageInfo;
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
