using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    [XfsObjectSystem]
    public class XfsTcpClientNodeNetAwakeSystem : XfsAwakeSystem<XfsTcpClientNodeNet>
    {
        public override void Awake(XfsTcpClientNodeNet self)
        {
            self.Init("127.0.0.1", 2001);
            self.MessageDispatcher = new OuterMessageDispatcher();
        }
    }
    [XfsObjectSystem]
    public class XfsTcpClientNodeNetUpdateSystem : XfsUpdateSystem<XfsTcpClientNodeNet>
    {
        int timer = 0;
        int valTime = 4000;
        public override void Update(XfsTcpClientNodeNet self)
        {
            timer += 1;
            if (timer > valTime)
            {
                timer = 0;
                self.Connecting();
            }
        }


    }



}