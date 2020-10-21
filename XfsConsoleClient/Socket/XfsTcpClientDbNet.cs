using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    public class XfsTcpClientDbNet : XfsTcpClient
    {
        public override NodeType NodeType => NodeType.Db;
        public XfsTcpClientDbNet(string ipString, int port, int maxListenCount)
        {
            this.IpString = ipString;
            this.Port = port;
            this.MaxListenCount = maxListenCount;

            Console.WriteLine(XfsTimerTool.CurrentTime() + " NodeType: " + this.NodeType);
        }

    }
}