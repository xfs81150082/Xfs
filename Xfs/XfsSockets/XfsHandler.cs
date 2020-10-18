using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public abstract class XfsHandler : XfsComponent
    {
        private static XfsHandler _instance;
        public static XfsHandler Instance { get => _instance; }
        public XfsHandler() { _instance = this; }       
        public abstract void  Recv(XfsParameter parameter);
    }
}