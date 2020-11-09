using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    public class XfsTcpServerGateNet : XfsTcpServer
    {
        public override XfsSenceType SenceType => XfsSenceType.Gate;
        public XfsTcpServerGateNet()
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " NodeType: " + this.SenceType);
        }
        public void Init(string ipString, int port, int maxListenCount)
        {
            this.IpString = ipString;
            this.Port = port;
            this.MaxListenCount = maxListenCount;
        }




    }
}
