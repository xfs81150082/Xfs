using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateServer
{
    public class C4G_PingHandler : XfsAMRpcHandler<C4G_Ping, G4C_Ping>
    {
        protected override void Run(XfsSession session, C4G_Ping message, Action<G4C_Ping> reply)
        {



            Console.WriteLine(XfsTimeHelper.CurrentTime() + " C4G_PingHandler-17: " + message.Message);

        }


    }
}
