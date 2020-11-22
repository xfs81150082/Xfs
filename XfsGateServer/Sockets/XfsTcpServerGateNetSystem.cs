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

namespace XfsGateServer
{
    [XfsObjectSystem]
    public class XfsTcpServerGateNetAwakeSystem : XfsAwakeSystem<XfsTcpServerGateNet>
    {
        public override void Awake(XfsTcpServerGateNet self)
        {
            self.Init("127.0.0.1", 2001, 10);
            self.MessageDispatcher = new OuterMessageDispatcher();
            self.IPEndPoint = new IPEndPoint(self.Address, self.Port);
            Console.WriteLine("{0} 服务器初始化属性，监听{1}成功", XfsTimeHelper.CurrentTime(), self.IPEndPoint);

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