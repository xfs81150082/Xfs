using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsNodeServer
{
    public class XfsDbController : XfsController
    {
        public override NodeType NodeType => NodeType.Db;

        public override void Recv(object obj, XfsParameter parameter)
        {
            TenCode tenCode = parameter.TenCode;
            switch (tenCode)
            {
                case (TenCode.Code0001):
                    string va = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());
                    XfsModelObjects.Tests.Add(va);


                    Console.WriteLine(XfsTimerTool.CurrentTime() + "23 XfsHandlers: " + "" + va);

                    break;
                case (TenCode.Code0002):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsControllers: " + tenCode);

                    break;
                case (TenCode.Code0003):                  
                    string va3 = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsDbController，Node已收到回复信息: " + va3);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " parameter.PeerIds: " + parameter.PeerIds.Count);

                    //XfsGame.XfsSence.GetComponent<XfsTcpServerNodeNet>().Send(parameter);

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
