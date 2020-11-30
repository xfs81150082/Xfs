

namespace Xfs
{
    public class InnerMessageDispatcher : IXfsMessageDispatcher
    {
        public void Dispatch(XfsSession session, int opcode, object message)
        {
            // 收到actor消息,放入actor队列
            switch (message)
            {
                case IXfsActorRequest iActorRequest:
                    {
                        //XfsInnerMessageDispatcherHelper.HandleIActorRequest(session, iActorRequest).Coroutine();
                        return;
                    }
                case IXfsActorMessage iactorMessage:
                    {
                        //XfsInnerMessageDispatcherHelper.HandleIActorMessage(session, iactorMessage).Coroutine();
                        return;
                    }
                case IXfsActorResponse iActorResponse:
                    {
                        //XfsInnerMessageDispatcherHelper.HandleIActorResponse(session, iActorResponse).Coroutine();
                        return;
                    }
                default:
                    {
                        XfsMessageDispatcherComponent.Instace.Handle(session, new XfsMessageInfo(opcode, message));
                        break;
                    }
            }
        }


    }
}