using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public abstract class XfsHandler : XfsEntity
    {
        public abstract NodeType NodeType { get; }
        public XfsHandler() 
        {
            XfsSockets.AddXfsHandler(this);

            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsHandler:" + this.NodeType + "已注册");
        }
        public abstract void Recv(object obj, XfsParameter parameter, NodeType nodeType);
        
    }
}