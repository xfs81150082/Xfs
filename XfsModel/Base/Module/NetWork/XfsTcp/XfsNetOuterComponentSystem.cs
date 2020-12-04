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

namespace Xfs
{
    [XfsObjectSystem]
    public class XfsNetOuterComponentAwakeSystem : XfsAwakeSystem<XfsNetOuterComponent>
    {
        public override void Awake(XfsNetOuterComponent self)
        {
            self.MessageDispatcher = new XfsOuterMessageDispatcher();
        }
    }

    [XfsObjectSystem]
    public class XfsNetOuterComponentUpdateSystem : XfsUpdateSystem<XfsNetOuterComponent>
    {
        int timer = 0;
        int valTime = 4000;
        public override void Update(XfsNetOuterComponent self)
        {
            timer += 1;
            if (timer > valTime)
            {
                timer = 0;
                if (self.IsServer)
                {
                    self.Listening();
                }
                else
                {
                    self.Connecting();
                }
            }
        }
        
    }
}