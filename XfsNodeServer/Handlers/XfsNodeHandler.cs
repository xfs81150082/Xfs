using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Xfs;

namespace XfsNodeServer
{
    public class XfsNodeHandler : XfsHandler
    {
        public override NodeType NodeType => NodeType.Node;
        public XfsNodeHandler()
        {
        }
        public override void Recv(object obj, XfsParameter parameter)
        {
            TenCode tenCode = parameter.TenCode;
            switch (tenCode)
            {
                case (TenCode.Code0001):
                    string va = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsDbHandler，已收到客户端信息: " + va);

                    string sv = XfsTimerTool.CurrentTime() + " 服务器" + this.NodeType + "回复，收到并返回原信息：";
                    string tt = sv + "(" + va + ")";
                    XfsParameter repsonse = XfsParameterTool.ToParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(), tt);
                    repsonse.Back = parameter.Back;
                    repsonse.Keys = parameter.Keys;

                    XfsTcpServer server = null;
                    XfsSockets.XfsTcpServers.TryGetValue(NodeType.Node, out server);
                    if (server != null)
                    {
                        (server as XfsTcpServerNodeNet).Send(repsonse);
                    }

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 服务器" + this.NodeType + "已完成发送回的信息");

                    ///继续向Db服务请求数据
                    
                    XfsParameter requst = XfsParameterTool.ToParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(), tt);
                    requst.Back = parameter.Back;
                    requst.Keys = parameter.Keys;

                    XfsTcpClient clientDb = null;
                    XfsSockets.XfsTcpClients.TryGetValue(NodeType.Db, out clientDb);
                    (clientDb as XfsTcpClientDbNet).Send(requst);


                    //XfsGame.XfsSence.GetComponent<XfsTcpServerNodeNet>().Send(repsonse, NodeType.Node);

                    break;
                case (TenCode.Code0002):
                    string va2 = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());


                    Console.WriteLine(XfsTimerTool.CurrentTime() + " Node，已收到客户端信息: " + tenCode + " : " + va2);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsNodeHandler: parameter.PeerIds" + parameter.PeerIds.Count);

                    XfsParameter request2 = XfsParameterTool.ToParameter(TenCode.Code0002, ElevenCode.Code0002, ElevenCode.Code0002.ToString(), va2);
                    request2.Back = parameter.Back;
                    request2.Keys = parameter.Keys;
                    request2.PeerIds = parameter.PeerIds;

                    XfsTcpClient client2 = null;
                    XfsSockets.XfsTcpClients.TryGetValue(NodeType.Db, out client2);
                    if (client2 != null)
                    {
                        (client2 as XfsTcpClientDbNet).Send(request2);
                    }                   

                    break;
                case (TenCode.Code0003):
                    string va3 = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsHandlers: " + tenCode+""+va3);

                    XfsTcpServer server3 = null;
                    XfsSockets.XfsTcpServers.TryGetValue(NodeType.Node, out server3);
                    if (server3 != null)
                    {
                        (server3 as XfsTcpServerNodeNet).Send(parameter);
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