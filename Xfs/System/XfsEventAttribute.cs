using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class XfsEventAttribute : XfsBaseAttribute
    {
        public string Type { get; }

        public XfsEventAttribute(string type)
        {
            this.Type = type;
        }
    }
}
