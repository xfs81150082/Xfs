using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    public class XfsTcpClientNodeNetSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            ValTime = 4000;
        }
        public override void XfsUpdate()
        {
            XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().Connecting();
        }


    }
}