using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace Xfs
{
    [XfsActorMessageHandler]
    public class G4C_TestRpcActorHandler : XfsAMActorRpcHandler<XfsGateTest, Actor_TestRequest, Actor_TestResponse>
    {
        protected override async XfsTask Run(XfsGateTest unit, Actor_TestRequest request, Actor_TestResponse response, Action reply)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_TestRpcActorHandler-15, 冒个泡." + request.Message);

            response.RpcId = request.RpcId;
            response.ActorId = request.ActorId;
            response.Message = "G4C_TestRpcActorHandler is getting 20201203";
            //XfsActorMessageSenderComponent xfsActorMessageSenderComponent = XfsGame.Scene.GetComponent<XfsActorMessageSenderComponent>();

            XfsActorMessageSenderComponent.Instance.Send(response.ActorId, response);

            reply();
        }

        private XfsVoid GetXfsVoid()
        {
            return new XfsVoid();
        }


    }
}
