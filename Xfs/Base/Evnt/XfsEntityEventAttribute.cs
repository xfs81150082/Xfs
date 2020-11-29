using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class XfsEntityEventAttribute : Attribute
    {
        public int ClassType;
        public XfsEntityEventAttribute(int classType)
        {
            this.ClassType = classType;
        }
    }
}
