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
        public XfsDbHandler()
        {
            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsDbHandler: " + "已启用");
        }

        public override void Recv(object obj, XfsParameter parameter,NodeType nodeType)
        {
            TenCode tenCode = parameter.TenCode;
            switch (tenCode)
            {
                case (TenCode.Code0001):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + "22 XfsHandlers: " + tenCode);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + "23 XfsHandlers: " + parameter.ElevenCode);

                    string va = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());

                    Console.WriteLine(XfsTimerTool.CurrentTime() + "27 XfsHandlers: " + "" + va);


                    string sv = "--(" + XfsTimerTool.CurrentTime() + "+服务器回复)";
                    string tt = va + sv;
                    XfsParameter repsonse = XfsParameterTool.ToParameter(TenCode.Code0001, ElevenCode.Code0001, ElevenCode.Code0001.ToString(), tt);
                    repsonse.Back = parameter.Back;
                    repsonse.Keys = parameter.Keys;

                    XfsTcpServer.Instance.Send(repsonse, NodeType.Db);
                    XfsGame.XfsSence.GetComponent<XfsTcpServerDbNet>().Send(repsonse, NodeType.Db);

                    Console.WriteLine(XfsTimerTool.CurrentTime() + "37 XfsHandlers: " + "服务器已发送回信息: " + tt);

                    //XfsGame.XfsSence.GetComponent<XfsBookerHandler>().OnTransferParameter(this, parameter);
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
