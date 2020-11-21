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
            //CheckSession(self);
        }

        int ti = 0;
        int timer = 4000;
        void CheckSession(XfsHeartComponent self)
        {
            ti += 1;
            if (ti < timer) return;
            ti = 0;
            self.CdCount += 1;
            if (self.CdCount > self.MaxCdCount)
            {
                self.Counting = false;
            }
            if (!self.Counting)
            {
                if ((self.Parent as XfsSession).Parent != null)
                {
                    (self.Parent as XfsSession).IsRunning = false;
                    if ((self.Parent as XfsSession).Socket != null)
                    {
                        (self.Parent as XfsSession).Socket.Close();
                    }
                }

                if ((self.Parent as XfsSession).Parent != null)
                {
                    if ((self.Parent as XfsSession).IsServer)
                    {
                        if ((self.Parent as XfsSession).Network != null)
                        {
                            (self.Parent as XfsSession).Network.IsRunning = false;
                        }
                    }
                    else
                    {
                        if ((self.Parent as XfsSession).Network != null)
                        {
                            (self.Parent as XfsSession).Network.IsRunning = false;
                        }
                    }
                }
                self.Parent.Dispose();
            }
            else
            {
                //发送心跳检测（并等待签到，签到入口在TmTcpSession里，双向发向即：客户端向服务端发送，服务端向客户端发送）
                //XfsParameter mvc = XfsMessageHelper.ToParameter(TenCode.Zero, ElevenCode.Zero);
                ////mvc.PeerIds.Add(self.Parent.InstanceId);
                //(self.Parent as XfsSession).Send(mvc);
                //Console.WriteLine(XfsTimeHelper.CurrentTime() + " IsServer: " + (self.Parent as XfsSession).IsServer + " CdCount:{0}-{1} ", self.CdCount, self.MaxCdCount);
            }
        }

    }
}