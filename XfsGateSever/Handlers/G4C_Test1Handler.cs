using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    [XfsMessageHandler(XfsSenceType.Gate)]
    public class G4C_Test1Handler : XfsAMHandler<XfsParameter>
    {
        protected override void Run(XfsSession session, XfsParameter message)
        {
            string va4 = XfsMessageHelper.GetValue<string>(message);

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " NodeHandler已收到客户端信息: " + va4);

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " 等待5秒后回复信息。。。");

            Thread.Sleep(5000);

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " 5秒时间到，发送回复信息。RpcId： " + message.RpcId);

            string res4 = "服务器已收到请求";
            XfsParameter response4 = XfsMessageHelper.ToParameter(TenCode.Code0004, ElevenCode.Code0004, res4);
            response4.RpcId = message.RpcId;

            session.Send(response4);

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " 向客户端，发送信息。RpcId： " + response4.Message);
        }
    }
}
