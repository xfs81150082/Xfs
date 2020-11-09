using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    [XfsObjectSystem]
    public class XfsTcpClientDbNetAwakeSystem : XfsAwakeSystem<XfsTcpClientDbNet>
    {
        public override void Awake(XfsTcpClientDbNet self)
        {
            self.ValTime = 4000;
        }

        //public override void XfsAwake()
        //{
        //    ValTime = 4000;
        //}
    
    }

    [XfsObjectSystem]
    public class XfsTcpClientDbNetUpdateSystem : XfsUpdateSystem<XfsTcpClientDbNet>
    {
     int timer = 0;
        public override void Update(XfsTcpClientDbNet self)
        {
            timer += 1;
            if (timer > self.ValTime)
            {
                XfsGame.XfsSence.GetComponent<XfsTcpClientDbNet>().Connecting();
                timer = 0;
            }
        }

        
    }


}
