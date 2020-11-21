using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    public class XfsTcpServerGateNet : XfsNetWorkComponent
    {
        public override XfsSenceType SenceType => XfsSenceType.Gate;
        public override bool IsServer => true;
        public XfsTcpServerGateNet()
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " SenceType: " + this.SenceType + " IsServer: " + this.IsServer);
        }

    }
}