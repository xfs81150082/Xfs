using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using Xfs;

namespace XfsGateSever
{
    public class XfsTcpServerGateNetSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            ValTime = 4000;
        }
        public override void XfsUpdate()
        {
            XfsGame.XfsSence.GetComponent<XfsTcpServerGateNet>().Listening();
        }


    }
}