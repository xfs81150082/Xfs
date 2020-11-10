using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsDbServer
{ 
    [XfsObjectSystem]
    public class XfsTcpServerDbNetAwakeSystem : XfsAwakeSystem<XfsTcpServerDbNet>
    {
        public override void Awake(XfsTcpServerDbNet self)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " Awake: " + this.GetType());
            self.Init("127.0.0.1", 2001, 10);

        }
    }

    [XfsObjectSystem]
    public class XfsTcpServerDbNetUpdateSystem : XfsUpdateSystem<XfsTcpServerDbNet>
    {
        int timer = 0;
        public override void Update(XfsTcpServerDbNet self)
        {
            timer += 1;
            if (timer > self.ValTime)
            {
                timer = 0;
                self.Listening();
                //XfsGame.XfsSence.GetComponent<XfsTcpServerGateNet>().Listening();
            }
        }


    }

}
