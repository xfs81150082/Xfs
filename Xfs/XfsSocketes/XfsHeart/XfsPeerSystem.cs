using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsPeerSystem : XfsUpdateSystem<XfsPeer>
    {
        public override void Update(XfsPeer self)
        {
            CheckSession(self);
        }
        void CheckSession(XfsPeer self)
        {
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

                //XfsTcpServer server = null;
                //XfsSockets.XfsTcpServers.TryGetValue((entity as XfsTcpSession).NodeType, out server);
                //if (server != null)
                //{
                //    server.Send(mvc);
                //    Console.WriteLine(XfsTimerTool.CurrentTime() + " Server-CdCount:{0}-{1} ", cd.CdCount, cd.MaxCdCount);
                //}
            }
        }

    }
}