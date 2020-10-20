using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    public class XfsTcpClientNodeNet : XfsTcpClient
    {
        public override NodeType NodeType => NodeType.Client;
        public XfsTcpClientNodeNet()
        {
            Console.WriteLine(XfsTimerTool.CurrentTime() + " NodeType: " + this.NodeType);
        }
        public XfsTcpClientNodeNet(string ipString, int port, int maxListenCount)
        {
            this.IpString = ipString;
            this.Port = port;
            this.MaxListenCount = maxListenCount;

            Console.WriteLine(XfsTimerTool.CurrentTime() + " NodeType: " + this.NodeType);
        }
    }
}