using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    public class XfsTcpClientDbNetSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            ValTime = 4000;
        }
        public override void XfsUpdate()
        {
            XfsGame.XfsSence.GetComponent<XfsTcpClientDbNet>().Connecting();
        }
    }
}
