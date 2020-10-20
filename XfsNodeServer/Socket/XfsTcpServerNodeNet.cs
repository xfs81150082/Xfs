using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsNodeServer
{
    public class XfsTcpServerNodeNet : XfsTcpServer
    {
        public override NodeType NodeType => NodeType.Node;
        public XfsTcpServerNodeNet()
        {
            Console.WriteLine(XfsTimerTool.CurrentTime() + " NodeType: " + this.NodeType);
        }
        public XfsTcpServerNodeNet(string ipString, int port, int maxListenCount)
        {
            this.IpString = ipString;
            this.Port = port;
            this.MaxListenCount = maxListenCount;

            Console.WriteLine(XfsTimerTool.CurrentTime() + " NodeType: " + this.NodeType);
        }




    }
}
