using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateServer
{
    [XfsActorMessageHandler]
    public class G4C_TestRpcActorHandler : XfsAMActorRpcHandler<Test1Entity, Actor_TestRequest, Actor_TestResponse>
    {
        protected override XfsTask Run(Test1Entity unit, Actor_TestRequest request, Actor_TestResponse response, Action reply)
        {
            throw new NotImplementedException();
        }

        private XfsVoid GetXfsVoid()
        {
            return new XfsVoid();
        }


    }
}
