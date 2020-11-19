using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    public class XfsTcpClientNodeNet : XfsNetWorkComponent
    {
        public override XfsSenceType SenceType => XfsSenceType.Client;
        public override bool IsServer => false;

        public XfsTcpClientNodeNet()
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " SenceType: " + this.SenceType + " IsServer: " + this.IsServer);
        }

    }
}