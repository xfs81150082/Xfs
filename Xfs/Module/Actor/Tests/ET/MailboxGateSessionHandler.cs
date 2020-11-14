using System;
using ETModel;

namespace Xfs
{
    /// <summary>
    /// gate session类型的Mailbox，收到的actor消息直接转发给客户端
    /// </summary>
    [XfsMailboxHandler(XfsSenceType.Gate, XfsMailboxType.GateSession)]
    public class MailboxGateSessionHandler : IXfsMailboxHandler
    {
        //public async XfsTask Handle(XfsSession session, XfsEntity entity, object actorMessage)
        //{
        //	//try
        //	//{
        //	//	IXfsActorMessage iActorMessage = actorMessage as IXfsActorMessage;
        //	//	// 发送给客户端
        //	//	XfsSession clientSession = entity as XfsSession;
        //	//	iActorMessage.ActorId = 0;
        //	//	clientSession.Send(iActorMessage);
        //	//	await XfsTask.CompletedTask;
        //	//}
        //	//catch (Exception e)
        //	//{
        //	//	//Log.Error(e);
        //	//}
        //}
        public XfsTask Handle(XfsSession session, XfsEntity entity, object actorMessage)
        {
            throw new NotImplementedException();
        }
    }
}
