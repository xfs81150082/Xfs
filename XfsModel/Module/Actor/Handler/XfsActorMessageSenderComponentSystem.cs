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
        //public static void Check(this XfsActorMessageSenderComponent self, bool isTimeOut)
        //{
        //    long timeNow = XfsTimeHelper.Now();
        //    //foreach ((int key, XfsActorMessageSender value) in self.requestCallback)
        //    //{
        //    //    if (timeNow < value.CreateTime + XfsActorMessageSenderComponent.TIMEOUT_TIME)
        //    //    {
        //    //        continue;
        //    //    }
        //    //    self.TimeoutActorMessageSenders.Add(key);
        //    //}

        //    foreach (int rpcId in self.TimeoutActorMessageSenders)
        //    {
        //        XfsActorMessageSender actorMessageSender = self.requestCallback[rpcId];
        //        self.requestCallback.Remove(rpcId);
        //        //Log.Error($"actor request timeout: {rpcId}");
        //        actorMessageSender.Callback.Invoke(new XfsActorResponse() { Error = XfsErrorCode.ERR_ActorTimeout });
        //    }

        //    self.TimeoutActorMessageSenders.Clear();
        //}

        public static void Send(this XfsActorMessageSenderComponent self, long actorId, IXfsActorMessage message)
        {
            if (actorId == 0)
            {
                //throw new Exception($"actor id is 0: {MongoHelper.ToJson(message)}");
            }
            int process = XfsIdGeneraterHelper.GetProcess(actorId);
            //string address = StartProcessConfigCategory.Instance.Get(process).InnerAddress;
            //XfsSession session = XfsNetInnerComponent.Instance.Get(address);
           
            XfsSession session = null;
            message.ActorId = actorId;
            session.Send(message);
        }

        public static XfsTask<IXfsActorResponse> Call(this XfsActorMessageSenderComponent self, long actorId, IXfsActorRequest message, bool exception = true)
        {
            if (actorId == 0)
            {
                //throw new Exception($"actor id is 0: {MongoHelper.ToJson(message)}");
            }

            var tcs = new XfsTaskCompletionSource<IXfsActorResponse>();

            //int process = XfsIdGeneraterHelper.GetProcess(actorId);
            //string address = StartProcessConfigCategory.Instance.Get(process).InnerAddress;
            //Session session = NetInnerComponent.Instance.Get(address);            
            //instanceIdStruct.Process = XfsIdGeneraterHelper.Process;
 
            InstanceIdStruct instanceIdStruct = new InstanceIdStruct(actorId);

            XfsSession session = null;
            message.ActorId = instanceIdStruct.ToLong();
            message.RpcId = ++self.RpcId;

            self.requestCallback.Add(message.RpcId, new XfsActorMessageSender((response) =>
            {
                if (exception && XfsErrorCode.IsRpcNeedThrowException(response.Error))
                {
                    //tcs.SetException(new Exception($"Rpc error: {MongoHelper.ToJson(response)}"));
                    return;
                }

                tcs.SetResult(response);
            }));
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