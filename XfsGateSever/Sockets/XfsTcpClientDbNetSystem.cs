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
            //self.ValTime = 4000;
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
        int valTime = 4000;
        public override void Update(XfsTcpClientDbNet self)
        {
            timer += 1;
            if (timer > valTime)
            {
                timer = 0;

                XfsGame.XfsSence.GetComponent<XfsTcpClientDbNet>().Connecting();
            }
        }

        
    }


}
