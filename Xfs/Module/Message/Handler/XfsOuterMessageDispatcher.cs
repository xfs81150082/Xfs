using System;

namespace Xfs
{
	public class XfsOuterMessageDispatcher : IXfsMessageDispatcher
	{
		public void Dispatch(XfsSession session, int opcode, object message)
		{
            DispatchAsync(session, opcode, message).Coroutine();
        }

        //private void DispatchAsync(XfsSession session, int opcode, object message)
        //{
        //    // 非Actor消息
        //    XfsGame.Scene.GetComponent<XfsMessageDispatcherComponent>().Handle(session, new XfsMessageInfo(opcode, message));
        //}
        public async XfsVoid DispatchAsync(XfsSession session, int opcode, object message)
        {
            // 根据消息接口判断是不是Actor消息，不同的接口做不同的处理
            switch (message)
            {
                case IXfsActorLocationRequest actorLocationRequest: // gate session收到actor rpc消息，先向actor 发送rpc请求，再将请求结果返回客户端
                    {

                        //long unitId = session.GetComponent<XfsSessionPlayerComponent>().Player.UnitId;
                        //XfsActorLocationSender actorLocationSender = XfsGame.Scene.GetComponent<ActorLocationSenderComponent>().Get(unitId);
                        //int rpcId = actorLocationRequest.RpcId; // 这里要保存客户端的rpcId
                        //long instanceId = session.InstanceId;
                        //IXfsResponse response = await actorLocationSender.Call(actorLocationRequest);
                        //response.RpcId = rpcId;

                        //// session可能已经断开了，所以这里需要判断
                        //if (session.InstanceId == instanceId)
                        //{
                        //    session.Reply(response);
                        //}

                        await new XfsTaskCompleted();

                        break;
                    }
                case IXfsActorLocationMessage actorLocationMessage:
                    {

                        //long unitId = session.GetComponent<SessionPlayerComponent>().Player.UnitId;
                        //ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(unitId);
                        //actorLocationSender.Send(actorLocationMessage);

                        break;
                    }
                case IXfsActorRequest actorRequest:  // 分发IActorRequest消息，目前没有用到，需要的自己添加
                    {
                        break;
                    }
                case XfsActorResponse actorResponse:  // 分发IActorRequest消息，目前没有用到，需要的自己添加
                    {

                        XfsGame.Scene.GetComponent<XfsActorMessageSenderComponent>().RunMessage(actorResponse);

                        break;
                    }
                case IXfsActorMessage actorMessage:  // 分发IActorMessage消息，目前没有用到，需要的自己添加
                    {
                        break;
                    }
                default:
                    {
                        // 非Actor消息
                        XfsGame.Scene.GetComponent<XfsMessageDispatcherComponent>().Handle(session, new XfsMessageInfo(opcode, message) );
                        break;
                    }
            }

        }
    }
}