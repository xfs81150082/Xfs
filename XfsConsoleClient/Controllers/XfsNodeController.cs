using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    public class XfsNodeController : XfsController
    {
        public override NodeType NodeType => NodeType.Node;
        public XfsNodeController()
        {
            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsNodeController: " + "已启用");
        }
        public override void Recv(object obj, XfsParameter parameter)
        {
            TenCode tenCode = parameter.TenCode;
            switch (tenCode)
            {
                case (TenCode.Code0001):
                    string va = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());
                    XfsModelObjects.Tests.Add(va);

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 23 XfsNodeController: " + "" + va);

                    //XfsGame.XfsSence.GetComponent<XfsBookerHandler>().OnTransferParameter(this, parameter);

                    break;
                case (TenCode.Code0002):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsNodeController: " + tenCode);

                    //XfsGame.XfsSence.GetComponent<XfsStatusSyncHandler>().OnTransferParameter(this, parameter);

                    break;
                case (TenCode.Code0003):
                    string va3 = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 40 XfsNodeController: " + TenCode.Code0003);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + "41 XfsNodeController: " + tenCode + " : " + va3);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 42 XfsNodeController: " + TenCode.Code0003);

                    break;
                case (TenCode.Code0004):
                    string va4 = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());


                    Console.WriteLine(XfsTimerTool.CurrentTime() + "41 XfsNodeController: " + tenCode + " : " + va4);


                    break;
                case (TenCode.End):
                    break;
                default:
                    break;
            }
        }
    }
}
