using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    public class XfsTcpClientDbNet : XfsTcpClient
    {
        public override XfsSenceType SenceType => XfsSenceType.Db;
        public XfsTcpClientDbNet(string ipString, int port, int maxListenCount)
        {
            this.IpString = ipString;
            this.Port = port;
            this.MaxListenCount = maxListenCount;

            Console.WriteLine(XfsTimerTool.CurrentTime() + " NodeType: " + this.SenceType);
        }

    }
}