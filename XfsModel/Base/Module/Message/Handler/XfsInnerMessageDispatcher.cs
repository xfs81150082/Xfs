using System;

namespace Xfs
{
    public class XfsInnerMessageDispatcher : IXfsMessageDispatcher
    {
        public void Dispatch(XfsSession session, int opcode, object message)
        {
            DispatchAsync(session, opcode, message).Coroutine();
        }
        public async XfsVoid DispatchAsync(XfsSession session, int opcode, object message)
        {
            // 根据消息接口判断是不是Actor消息，不同的接口做不同的处理
            switch (message)
            {
                case IXfsActorRequest actorRequest:   // 分发IActorRequest消息，目前没有用到，需要的自己添加
                    {
                        XfsEntity entity = XfsGame.EventSystem.Get(actorRequest.ActorId);

                        await XfsGame.Scene.GetComponent<XfsActorMessageDispatcherComponent>().Handle(entity, session, actorRequest);
                        break;
                    }
                case IXfsActorResponse actorResponse:  // 分发XfsActorResponse消息，目前没有用到，需要的自己添加
                    {
                        XfsGame.Scene.GetComponent<XfsActorMessageSenderComponent>().RunMessage(actorResponse);
                        break;
                    }
                default:
                    {
                        // 非Actor消息
                        XfsGame.Scene.GetComponent<XfsMessageDispatcherComponent>().Handle(session, new XfsMessageInfo(opcode, message));
                        break;
                    }
            }

        }


    }
}