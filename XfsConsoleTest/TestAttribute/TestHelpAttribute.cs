using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XfsConsoleTest.TestAttribute
{
    [AttributeUsage(AttributeTargets.All)]
     public  class TestHelpAttribute : Attribute
    {
        private string topic;
        public readonly string Url;
        public string Topic  // Topic 是一个命名（named）参数
        {
            get
            {
                return topic;
            }
            set
            {
                topic = value;
            }
        }

        public TestHelpAttribute(string url)  // url 是一个定位（positional）参数
        {
            this.Url = url;
        }


    }
}
