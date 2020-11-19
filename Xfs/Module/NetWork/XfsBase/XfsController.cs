using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public abstract class XfsController : XfsEntity
    {
        public abstract XfsSenceType SenceType { get; }
        public XfsController() 
        {
            //XfsSockets.XfsControllers.Add(this.SenceType, this);
        }
        public abstract void Recv(object obj, XfsParameter parameter);  
    }
}