using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace Xfs
{
    [XfsMessageHandler]
    public class G4C_PingHandler : XfsAMRpcHandler<C4G_Ping, G4C_Pong>
    {
        protected override void Run(XfsSession session, C4G_Ping message, Action<G4C_Pong> reply)
        {
            string mes = message.Message; ///从客户端发来的信息

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_PingHandler-16: " + mes);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_PingHandler-19: 等待5秒后回复信息...");

            //WaitTimer(5000);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_PingHandler-22: 等待5秒后回复信息...");

            Thread.Sleep(5000);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " 5秒时间到，发送回复信息。RpcId： " + message.RpcId);

            G4C_Pong resG = new G4C_Pong();
            resG.RpcId = message.RpcId;
            resG.Message = "从服务器发回客户端...";
            session.Send(resG);

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_PingHandler-31, 已发送回消息." );

            //XfsActorMessageDispatcherComponent actorMessageDispatcherComponent = XfsGame.Scene.AddComponent<XfsActorMessageDispatcherComponent>();

            //Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_PingHandler-36: "+ actorMessageDispatcherComponent.ActorMessageHandlers.Count);

            //Actor_TestRequest actorRequest = new Actor_TestRequest();
            //Test1Entity entity =XfsEntityFactory.Create<Test1Entity>(XfsGame.Scene);
            //actorRequest.ActorId = entity.InstanceId;
            //TestActorRpcCall(actorRequest.ActorId, actorRequest).Coroutine();

        }




        private async XfsVoid TestActorRpcCall(long actorId , IXfsActorRequest actor_TestRequest)
        {
            XfsActorMessageSenderComponent xfsActorMessageSenderComponent = XfsGame.Scene.GetComponent<XfsActorMessageSenderComponent>();

            Actor_TestResponse actor_TestResponse = (Actor_TestResponse)await xfsActorMessageSenderComponent.Call(actorId, actor_TestRequest);

        }


    }
}
