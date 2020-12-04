using System;

namespace Xfs
{
    public class XfsActorMessageSenderComponentAwakeSystem : XfsAwakeSystem<XfsActorMessageSenderComponent>
    {
        public override void Awake(XfsActorMessageSenderComponent self)
        {
            XfsActorMessageSenderComponent.Instance = self;

            //self.TimeoutCheckTimer = XfsTimerComponent.Instance.NewRepeatedTimer(10 * 1000, self.Check);
        }
    }

    public class ActorMessageSenderComponentDestroySystem : XfsDestroySystem<XfsActorMessageSenderComponent>
    {
        public override void Destroy(XfsActorMessageSenderComponent self)
        {
            XfsActorMessageSenderComponent.Instance = null;
            //XfsTimerComponent.Instance.Remove(self.TimeoutCheckTimer);
            //self.TimeoutCheckTimer = 0;
            //self.TimeoutActorMessageSenders.Clear();
        }
    }

    public static class ActorMessageSenderComponentSystem
    {
        public static void Send(this XfsActorMessageSenderComponent self, long actorId, IXfsActorMessage message)
        {
            if (actorId == 0)
            {
                //throw new Exception($"actor id is 0: {MongoHelper.ToJson(message)}");
            }
            XfsSession session = XfsGame.Scene.GetComponent<XfsNetInnerComponent>().GetSession();
            message.ActorId = actorId;
            session.Send(message);
        }      
        public static XfsTask<IXfsActorResponse> Call(this XfsActorMessageSenderComponent self, long actorId, IXfsActorRequest message, bool exception = true)
        {
            if (actorId == 0)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " Call-71: ");
            }
            var tcs = new XfsTaskCompletionSource<IXfsActorResponse>();
            message.RpcId = ++self.RpcId;
            XfsSession session = XfsGame.Scene.GetComponent<XfsNetInnerComponent>().GetSession();

            self.requestCallback.Add(message.RpcId, new XfsActorMessageSender((response) =>
            {
                if (exception && XfsErrorCode.IsRpcNeedThrowException(response.Error))
                {
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " Call-93: ");
                    return;
                }

                tcs.SetResult(response);
            }));
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " Call-97: ");

            session.Send(message);

            return tcs.Task;
        }

        //public static async XfsTask<IXfsActorResponse> CallWithoutException(this XfsActorMessageSenderComponent self, long actorId, IXfsActorRequest message)
        //{
        //    return await self.Call(actorId, message, false);
        //}

        public static void RunMessage(this XfsActorMessageSenderComponent self, IXfsActorResponse response)
        {
            XfsActorMessageSender actorMessageSender;
            if (!self.requestCallback.TryGetValue(response.RpcId, out actorMessageSender))
            {
                //Log.Error($"not found rpc, maybe request timeout, response message: {StringHelper.MessageToStr(response)}");
                return;
            }
            self.requestCallback.Remove(response.RpcId);

            actorMessageSender.Callback(response);
        }


    }
}