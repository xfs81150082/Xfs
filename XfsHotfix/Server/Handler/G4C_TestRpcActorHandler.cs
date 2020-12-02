using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace Xfs
{
    [XfsActorMessageHandler]
    public class G4C_TestRpcActorHandler : XfsAMActorRpcHandler<Test1Entity, Actor_TestRequest, Actor_TestResponse>
    {
        protected override async XfsTask Run(Test1Entity unit, Actor_TestRequest request, Actor_TestResponse response, Action reply)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_TestRpcActorHandler-15, 冒个泡.");

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_TestRpcActorHandler-17: " + request.ActorId);

            reply();
        }

        private XfsVoid GetXfsVoid()
        {
            return new XfsVoid();
        }


    }
}
