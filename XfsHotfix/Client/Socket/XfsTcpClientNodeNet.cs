using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace Xfs
{
    public class XfsTcpClientNodeNet : XfsNetWorkComponent
    {
        public override bool IsServer => false;

        public XfsTcpClientNodeNet()
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() +" IsServer: " + this.IsServer);
        }

    }
}