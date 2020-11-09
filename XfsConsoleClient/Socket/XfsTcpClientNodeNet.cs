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
        public override XfsSenceType SenceType => XfsSenceType.Client;
        public XfsTcpClientNodeNet() { }
        public XfsTcpClientNodeNet(string ipString, int port, int maxListenCount)
        {
            this.IpString = ipString;
            this.Port = port;
            this.MaxListenCount = maxListenCount;

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " SenceType: " + this.SenceType);
        }
    }
}