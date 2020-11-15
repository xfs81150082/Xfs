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
                    string va4 = XfsParameterTool.GetValue<string>(request, request.ElevenCode.ToString());

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " NodeHandler已收到客户端信息: " + va4);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 等待5秒后回复信息。。。" );

                    Thread.Sleep(5000);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 5秒时间到，发送回复信息。RpcId： " + request.RpcId);

                    string res4 = "服务器已收到请求";
                    XfsParameter response4 = XfsParameterTool.ToParameter(TenCode.Code0004, ElevenCode.Code0004, ElevenCode.Code0004.ToString(), res4);
                    response4.RpcId = request.RpcId;

                    XfsSession peer44 = obj as XfsSession;
                    peer44.Send(response4);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 向客户端，发送信息。RpcId： " + response4.RpcId);


                    //response4.PeerIds = request.PeerIds;

                    //XfsPeer peer4 = null;
                    //for (int i = 0; i < response4.PeerIds.Count; i++)
                    //{
                    //    XfsGame.XfsSence.GetComponent<XfsTcpServerGateNet>().TPeers.TryGetValue(response4.PeerIds[i], out peer4);
                    //    if (peer4 != null)
                    //    {
                    //        peer4.Send(response4);
                    //        Console.WriteLine(XfsTimeHelper.CurrentTime() + " 向客户端，发送信息。RpcId： " + response4.RpcId);
                    //    }
                    //}


                    //XfsTcpServer server4 = null;
                    //XfsSockets.XfsTcpServers.TryGetValue(XfsSenceType.Gate, out server4);
                    //if (server4 != null)
                    //{
                    //    //(server4 as XfsTcpServerNodeNet).Send(request4);
                    //    //server4.Send(response4);

                    //    XfsPeer peer = null;
                    //    server4.TPeers.TryGetValue(response4.Keys[0], out peer);
                    //    if (peer != null)
                    //    {
                    //        peer.Send(response4);
                    //    }
                    //}

                    break;
                case (TenCode.End):
                    break;
                default:
                    break;
            }
        }


    }
}