using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;
using Xfs;

namespace XfsGateSever
{
    public class XfsGateHandler : XfsHandler
    {
        public override XfsSenceType SenceType => XfsSenceType.Gate;
        public XfsGateHandler()
        {
        }
        public override void Recv(object obj, XfsParameter request)
        {
            TenCode tenCode = request.TenCode;
            switch (tenCode)
            {
                case (TenCode.Code0001):
                   

                    break;
                case (TenCode.Code0002):
                   
                    break;
                case (TenCode.Code0003):
                   
                    break;
                case (TenCode.Code0004):
                    string va4 = XfsMessageHelper.GetValue<string>(request);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " NodeHandler已收到客户端信息: " + va4);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 等待5秒后回复信息。。。" );

                    Thread.Sleep(5000);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 5秒时间到，发送回复信息。RpcId： " + request.RpcId);

                    string res4 = "服务器已收到请求";
                    XfsParameter response4 = XfsMessageHelper.ToParameter(TenCode.Code0004, ElevenCode.Code0004, res4);
                    response4.RpcId = request.RpcId;

                    XfsSession peer44 = obj as XfsSession;
                    peer44.Send(response4);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 向客户端，发送信息。RpcId： " + response4.RpcId);
               

                    break;
                case (TenCode.End):
                    break;
                default:
                    break;
            }
        }


    }
}