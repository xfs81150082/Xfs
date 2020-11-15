using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    [XfsObjectSystem]
    class XfsHeartComponentAwakeSystem : XfsAwakeSystem<XfsHeartComponent>
    {
        public override void Awake(XfsHeartComponent self)
        {
            self.IsPeer = (self.Parent as XfsTcpSession).IsPeer;
            self.SenceType = (self.Parent as XfsTcpSession).SenceType;
            self.CdCount = 0; ;
            self.MaxCdCount = 4000; ;
            self.Counting = true; ;
        }
    }
    [XfsObjectSystem]
    class XfsHeartComponentUpdateSystem : XfsUpdateSystem<XfsHeartComponent>
    {
        public override void Update(XfsHeartComponent self)
        {
            CheckSession(self);
        }

        int ti = 0;
        int timer = 4000;
        void CheckSession(XfsHeartComponent self)
        {
            ti += 1;
            if (ti < timer) return;
            ti = 0;

            bool ispeer = self.IsPeer;
            if (!self.Counting)
            {
                self.Parent.Dispose();
            }
            else
            {
                //发送心跳检测（并等待签到，签到入口在TmTcpSession里，双向发向即：客户端向服务端发送，服务端向客户端发送）
                XfsParameter mvc = XfsParameterTool.ToParameter(TenCode.Zero, ElevenCode.Zero);
                mvc.Keys.Add(self.Parent.InstanceId);
                if (self.IsPeer)
                {
                    (self.Parent as XfsPeer).Send(mvc);
                }
                else
                {

                    (self.Parent as XfsClient).Send(mvc);
                }

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " IsPeer: " + self.IsPeer + " CdCount:{0}-{1} ", self.CdCount, self.MaxCdCount);
            }
        }
    }

}
