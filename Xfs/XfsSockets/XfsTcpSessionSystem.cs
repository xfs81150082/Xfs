using System;

namespace Xfs
{
    public class XfsTcpSessionSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            ValTime = 4000;
            AddComponent(new XfsSession());
            AddComponent(new XfsCoolDown());
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

                if (isServer)
                {
                    XfsGame.XfsSence.GetComponent<XfsTcpServer>().Send(mvc);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " Server-CdCount:{0}-{1} ", cd.CdCount, cd.MaxCdCount);
                }
                else
                {
                    XfsGame.XfsSence.GetComponent<XfsTcpClient>().Send(mvc);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " Client-CdCount:{0}-{1} ", cd.CdCount, cd.MaxCdCount);
                }

                //if (XfsGame.XfsSence.GetComponent<XfsTcpServer>() != null)
                //{
                //    XfsGame.XfsSence.GetComponent<XfsTcpServer>().Send(mvc);
                //    Console.WriteLine(XfsTimerTool.CurrentTime() + " Server-CdCount:{0}-{1} ", cd.CdCount, cd.MaxCdCount);
                //}
                //if (XfsGame.XfsSence.GetComponent<XfsTcpClient>() != null)
                //{
                //    XfsGame.XfsSence.GetComponent<XfsTcpClient>().Send(mvc);
                //    Console.WriteLine(XfsTimerTool.CurrentTime() + " Client-CdCount:{0}-{1} ", cd.CdCount, cd.MaxCdCount);
                //}
            }
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " CdCount:{0}-{1} ", cd.CdCount, cd.MaxCdCount);
        }

    }
}