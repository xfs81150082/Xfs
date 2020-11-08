using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsClientSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            ValTime = 4000;
            AddComponent(new XfsCoolDown());
            AddComponent(new XfsClientSession());
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
            bool isServer = (entity as XfsTcpSession).IsPeer;
            if (!cd.Counting)
            {
                entity.Dispose();
            }
            else
            {
                //发送心跳检测（并等待签到，签到入口在TmTcpSession里，双向发向即：客户端向服务端发送，服务端向客户端发送）
                XfsParameter mvc = XfsParameterTool.ToParameter(TenCode.Zero, ElevenCode.Zero);
                mvc.Keys.Add(entity.EcsId);
                (entity as XfsClient).Send(mvc);

                Console.WriteLine(XfsTimerTool.CurrentTime() + " Client-CdCount:{0}-{1} ", cd.CdCount, cd.MaxCdCount);

                //XfsTcpClient client = null;
                //XfsSockets.XfsTcpClients.TryGetValue((entity as XfsTcpSession).NodeType, out client);
                //if (client != null)
                //{
                //    client.Send(mvc);
                //    Console.WriteLine(XfsTimerTool.CurrentTime() + " Client-CdCount:{0}-{1} ", cd.CdCount, cd.MaxCdCount);
                //}
            }
        }

    }
}