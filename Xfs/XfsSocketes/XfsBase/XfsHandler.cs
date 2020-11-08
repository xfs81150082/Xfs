using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public abstract class XfsHandler : XfsEntity
    {
        public abstract XfsSenceType SenceType { get; }
        public XfsHandler() 
        {
            XfsSockets.XfsHandlers.Add(this.SenceType, this);
        }
        public abstract void Recv(object obj, XfsParameter parameter);
        
    }
}