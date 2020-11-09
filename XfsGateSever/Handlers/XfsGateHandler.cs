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
                    string va = XfsParameterTool.GetValue<string>(request, request.ElevenCode.ToString());

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsDbHandler，已收到客户端信息: " + va);

                    string sv = XfsTimeHelper.CurrentTime() + " 服务器" + this.SenceType + "回复，收到并返回原信息：";
                    string tt = sv + "(" + va + ")";
                    XfsParameter repsonse = XfsParameterTool.ToParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(), tt);
                    repsonse.Back = request.Back;
                    repsonse.Keys = request.Keys;

                    XfsTcpServer server = null;
                    XfsSockets.XfsTcpServers.TryGetValue(XfsSenceType.Gate, out server);
                    if (server != null)
                    {
                        //(server as XfsTcpServerNodeNet).Send(repsonse);
                    }

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 服务器" + this.SenceType + "已完成发送回的信息");

                    ///继续向Db服务请求数据
                    
                    XfsParameter requst = XfsParameterTool.ToParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(), tt);
                    requst.Back = request.Back;
                    requst.Keys = request.Keys;

                    XfsTcpClient clientDb = null;
                    XfsSockets.XfsTcpClients.TryGetValue(XfsSenceType.Db, out clientDb);
                    //(clientDb as XfsTcpClientDbNet).Send(requst);


                    //XfsGame.XfsSence.GetComponent<XfsTcpServerNodeNet>().Send(repsonse, NodeType.Node);

                    break;
                case (TenCode.Code0002):
                    string va2 = XfsParameterTool.GetValue<string>(request, request.ElevenCode.ToString());


                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " Node，已收到客户端信息: " + tenCode + " : " + va2);
                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsNodeHandler: parameter.PeerIds" + request.PeerIds.Count);

                    XfsParameter request2 = XfsParameterTool.ToParameter(TenCode.Code0002, ElevenCode.Code0002, ElevenCode.Code0002.ToString(), va2);
                    request2.Back = request.Back;
                    request2.Keys = request.Keys;
                    request2.PeerIds = request.PeerIds;

                    XfsTcpClient client2 = null;
                    XfsSockets.XfsTcpClients.TryGetValue(XfsSenceType.Db, out client2);
                    if (client2 != null)
                    {
                        //(client2 as XfsTcpClientDbNet).Send(request2);
                    }                   

                    break;
                case (TenCode.Code0003):
                    string va3 = XfsParameterTool.GetValue<string>(request, request.ElevenCode.ToString());

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsHandlers: " + tenCode + "" + va3);

                    XfsTcpServer server3 = null;
                    XfsSockets.XfsTcpServers.TryGetValue(XfsSenceType.Gate, out server3);
                    if (server3 != null)
                    {
                        //(server3 as XfsTcpServerNodeNet).Send(request);
                    }

                    break;
                case (TenCode.Code0004):
                    string va4 = XfsParameterTool.GetValue<string>(request, request.ElevenCode.ToString());

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " NodeHandler已收到客户端信息: " + va4);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 等待5秒后回复信息。。。" );

                    Thread.Sleep(5000);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 5秒时间到，发送回复信息。");

                    string res4 = "服务器已收到请求-20201106";
                    XfsParameter response4 = XfsParameterTool.ToParameter(TenCode.Code0004, ElevenCode.Code0004, ElevenCode.Code0004.ToString(), res4);
                    if (request.Back)
                    {
                        response4.RpcId = request.RpcId;
                        response4.Back = true;
                    }
                    response4.Keys = request.Keys;
                    response4.PeerIds = request.PeerIds;

                    XfsTcpServer server4 = null;
                    XfsSockets.XfsTcpServers.TryGetValue(XfsSenceType.Gate, out server4);
                    if (server4 != null)
                    {
                        //(server4 as XfsTcpServerNodeNet).Send(request4);
                        //server4.Send(response4);

                        XfsPeer peer = null;
                        server4.TPeers.TryGetValue(response4.Keys[0], out peer);
                        if (peer != null)
                        {
                            peer.Send(response4);
                        }
                    }

                    break;
                case (TenCode.End):
                    break;
                default:
                    break;
            }
        }


    }
}