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
    public class C2G_TestRequestHandler : XfsAMRpcHandler<C2G_TestRequest, G2C_TestResponse>
    {
        protected override void Run(XfsSession session, C2G_TestRequest message, Action<G2C_TestResponse> reply)
        {      
            ///从客户端发来的信息
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_PingHandler-16: " + message.Message);

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_PingHandler-19: 等待5秒后回复信息...");
            Thread.Sleep(5000);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " 5秒时间到，发送回复信息。RpcId： " + message.RpcId);

            G2C_TestResponse resG = new G2C_TestResponse();
            resG.RpcId = message.RpcId;
            resG.Message = "从服务器发回客户端...";
            session.Send(resG);

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " G4C_PingHandler-31, 已发送回消息." );         
        }
          


    }
}
