using System;
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
                        (server as XfsTcpServerNodeNet).Send(repsonse, NodeType.Node);
                    }


                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 服务器" + this.NodeType + "已完成发送回的信息");

                    //XfsGame.XfsSence.GetComponent<XfsTcpServerNodeNet>().Send(repsonse, NodeType.Node);
                  
                    break;
                case (TenCode.Code0002):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsHandlers: " + tenCode);


                    //XfsGame.XfsSence.GetComponent<XfsStatusSyncHandler>().OnTransferParameter(this, parameter);

                    break;
                case (TenCode.End):
                    break;
                default:
                    break;
            }
        }


    }
}