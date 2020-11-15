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
    [XfsObjectSystem]
    public class XfsTcpServerGateNetAwakeSystem : XfsAwakeSystem<XfsTcpServerGateNet>
    {
        public override void Awake(XfsTcpServerGateNet self)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTcpServerGateNet is Awake.");
            self.Init("127.0.0.1", 2001, 10);
        }
    }   

    [XfsObjectSystem]
    public class XfsTcpServerGateNetUpdateSystem : XfsUpdateSystem<XfsTcpServerGateNet>
    {
        int timer = 0;
        int valTime = 1000;
        public override void Update(XfsTcpServerGateNet self)
        {
            timer += 1;
            if (timer > valTime)
            {
                timer = 0;
                self.Listening();
            }
        }


    }
}