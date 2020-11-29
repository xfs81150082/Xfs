using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    [XfsMessageHandler]
    public class C4G_Test1Handler : XfsAMHandler<T2M_CreateUnit>
    {
        protected override void Run(XfsSession session, T2M_CreateUnit message)
        {




            Console.WriteLine(XfsTimeHelper.CurrentTime() + " C4G_Test1Handler: " + message.RpcId);
        }
    }
}
