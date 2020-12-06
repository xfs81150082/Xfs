using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    [XfsObjectSystem]
    class XfsHeartComponentAwakeSystem : XfsStartSystem<XfsHeartComponent>
    {
        public override void Start(XfsHeartComponent self)
        {
            self.CdCount = 0; ;
            self.MaxCdCount = 4; ;
            self.Counting = true; ;
        }
    }
    [XfsObjectSystem]
    class XfsHeartComponentUpdateSystem : XfsUpdateSystem<XfsHeartComponent>
    {
        public override void Update(XfsHeartComponent self)
        {
            //this.CheckSession(self);
        }

        private void CheckSession(XfsHeartComponent self)
        {
            if ((self.Parent as XfsSession) == null) return;
            self.Time += 1;
            if (self.Time < self.RecTimer) return;

            self.CdCount += 1;
            self.Time = 0;

            if (self.CdCount > self.MaxCdCount)
            {
                self.Counting = false;
            }
            if (!self.Counting)
            {
                if (self.Parent != null)
                {
                    self.Dispose();
                    self.Parent.Dispose();
                    return;
                }
            }
            else
            {
                if ((self.Parent as XfsSession) != null && self != null)
                {
                    //发送心跳检测（并等待签到，签到入口在TmTcpSession里，双向发向即：客户端向服务端发送，服务端向客户端发送）
                    (self.Parent as XfsSession).Send(new C4G_Ping());
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " IsServer: " + (self.Parent as XfsSession).IsServer + " CdCount:{0}-{1} ", self.CdCount, self.MaxCdCount);
                }
            }
        }

    }
}