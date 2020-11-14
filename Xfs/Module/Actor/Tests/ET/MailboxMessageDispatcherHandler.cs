using System;
using ETModel;

namespace Xfs
{
    /// <summary>
    /// 消息分发类型的Mailbox,对mailbox中的消息进行分发处理
    /// </summary>
    [XfsMailboxHandler(XfsSenceType.AllServer, XfsMailboxType.MessageDispatcher)]
    public class MailboxMessageDispatcherHandler : IXfsMailboxHandler
    {
        //public async XfsTask Handle(XfsSession session, XfsEntity entity, object actorMessage)
        //{
        //	try
        //	{
        //		await XfsGame.XfsSence.GetComponent<XfsActorMessageDispatcherComponent>().Handle(
        //			entity, new XfsActorMessageInfo() { Session = session, Message = actorMessage });
        //	}
        //	catch (Exception e)
        //	{
        //		//Log.Error(e);
        //	}
        //}
        public XfsTask Handle(XfsSession session, XfsEntity entity, object actorMessage)
        {
            throw new NotImplementedException();
        }
    }
}
