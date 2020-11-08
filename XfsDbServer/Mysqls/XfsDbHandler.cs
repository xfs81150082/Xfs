using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsDbServer
{
    public class XfsDbHandler : XfsHandler
    {
        public override XfsSenceType SenceType => XfsSenceType.Db;
        public XfsDbHandler()
        {
        }
        public override void Recv(object obj, XfsParameter parameter)
        {
            TenCode tenCode = parameter.TenCode;
            switch (tenCode)
            {
                case (TenCode.Code0001):
                    string va = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsDbHandler，已收到客户端信息: " + tenCode + " : " + va);
                    
                    string sv = XfsTimerTool.CurrentTime() + " 服务器" + this.SenceType + "回复，收到并返回原信息：";
                    string tt = sv + "(" + va + ")";
                    XfsParameter repsonse = XfsParameterTool.ToParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(), tt);
                    repsonse.Back = parameter.Back;
                    repsonse.Keys = parameter.Keys;

                    XfsTcpServer server = null;
                    XfsSockets.XfsTcpServers.TryGetValue(XfsSenceType.Db, out server);
                    //server.Send(repsonse);

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 服务器" + this.SenceType + "已完成发送回的信息");

                    //XfsGame.XfsSence.GetComponent<XfsTcpServerDbNet>().Send(repsonse, NodeType.Db);

                    break;
                case (TenCode.Code0002):
                    string va2 = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsDbHandler: " + tenCode + " : " + va2);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsDbHandler: parameter.PeerIds: " + parameter.PeerIds.Count);

                    string tt2 = "服务器" + this.SenceType + "回复，收到信息：" + "(" + va2 + ")";

                    XfsParameter repsonse2 = XfsParameterTool.ToParameter(TenCode.Code0003, ElevenCode.Code0003, ElevenCode.Code0003.ToString(), tt2);
                    repsonse2.Back = parameter.Back;
                    repsonse2.Keys = parameter.Keys;
                    repsonse2.PeerIds = parameter.PeerIds;


                    XfsTcpServer server2 = null;
                    XfsSockets.XfsTcpServers.TryGetValue(XfsSenceType.Db, out server2);
                    if (server2 != null)
                    {
                        //(server2 as XfsTcpServerDbNet).Send(repsonse2);
                    }

                    break;
                case (TenCode.End):
                    break;
                default:
                    break;
            }
        }

        XfsTcpServerDbNet DbServer()
        {
            XfsTcpServer ser = null;
            XfsSockets.XfsTcpServers.TryGetValue(this.SenceType, out ser);
            if (ser != null)
            {
                return ser as XfsTcpServerDbNet;
            }
            else
            {
                return null;
            }
        }



    }
}