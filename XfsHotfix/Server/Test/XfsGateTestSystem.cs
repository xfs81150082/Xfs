using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace Xfs
{
    [XfsObjectSystem]
     public class XfsGateTestSystem : XfsUpdateSystem<XfsGateTest>
    {
        public override void Update(XfsGateTest self)
        {
            //TestCall2(self);

            TestCall3(self).Coroutine();

        }

        int time = 0;
        int restime = 6000;

        async XfsVoid TestCall3(XfsGateTest self)
        {
            time += 1;
            if (time > restime)
            {
                time = 0;

                Actor_TestRequest actorRequest = new Actor_TestRequest();
                actorRequest.ActorId = self.InstanceId;
                actorRequest.Message = self.call;

                Actor_TestResponse actor_TestResponse = (Actor_TestResponse)await XfsActorMessageSenderComponent.Instance.Call(actorRequest.ActorId, actorRequest);

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-43: " + actor_TestResponse.Message);
            }
        }
        async void TestCall2(XfsGateTest self)
        {
            time += 1;
            if (time > restime)
            {
                time = 0;
                XfsOpcodeTypeComponent xfsOpcode = XfsGame.Scene.GetComponent<XfsOpcodeTypeComponent>();
                XfsMessageDispatcherComponent xfsMessage = XfsGame.Scene.GetComponent<XfsMessageDispatcherComponent>();

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-38: " + xfsMessage.Handlers.Count);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-38: " + xfsMessage.Handlers.Values);


                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsTestSystem-38: ");

                await new XfsTask();
            }
        }




    }


  
}
