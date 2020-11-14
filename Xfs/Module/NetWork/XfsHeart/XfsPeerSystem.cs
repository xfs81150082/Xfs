using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    [XfsObjectSystem]
    public class XfsPeerAwakeSystem : XfsStartSystem<XfsPeer>
    {
        public override void Start(XfsPeer self)
        {
            self.OnConnect();
        }
    }
    [XfsObjectSystem]
    public class XfsPeerUpdateSystem : XfsUpdateSystem<XfsPeer>
    {
        public override void Update(XfsPeer self)
        {
            CheckSession(self);
        }

        int ti = 0;
        int timer = 4000;
        void CheckSession(XfsPeer self)
        {
            ti += 1;
            if (ti < timer) return;
            ti = 0;


            XfsCoolDown cd = self.GetComponent<XfsCoolDown>();
            bool isServer = self.IsPeer;
            if (!cd.Counting)
            {
                self.Dispose();
            }
            else
            {
                //发送心跳检测（并等待签到，签到入口在TmTcpSession里，双向发向即：客户端向服务端发送，服务端向客户端发送）
                XfsParameter mvc = XfsParameterTool.ToParameter(TenCode.Zero, ElevenCode.Zero);
                mvc.Keys.Add(self.InstanceId);
                self.Send(mvc);

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " Server-CdCount:{0}-{1} ", cd.CdCount, cd.MaxCdCount);
            }
        }

    }
}