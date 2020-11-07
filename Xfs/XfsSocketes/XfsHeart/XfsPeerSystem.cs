using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsPeerSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            ValTime = 4000;
            AddComponent(new XfsCoolDown());
            AddComponent(new XfsPeerSession());
        }
        public override void XfsUpdate()
        {
            foreach (XfsEntity entity in GetTmEntities())
            {
                CheckSession(entity);
            }
        }
        void CheckSession(XfsEntity entity)
        {
            XfsCoolDown cd = entity.GetComponent<XfsCoolDown>();
            bool isServer = (entity as XfsTcpSession).IsServer;
            if (!cd.Counting)
            {
                entity.Dispose();
            }
            else
            {
                //发送心跳检测（并等待签到，签到入口在TmTcpSession里，双向发向即：客户端向服务端发送，服务端向客户端发送）
                XfsParameter mvc = XfsParameterTool.ToParameter(TenCode.Zero, ElevenCode.Zero);
                mvc.Keys.Add(entity.EcsId);
                (entity as XfsPeer).Send(mvc);

                Console.WriteLine(XfsTimerTool.CurrentTime() + " Server-CdCount:{0}-{1} ", cd.CdCount, cd.MaxCdCount);

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