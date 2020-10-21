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
            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsControllers: " + "已启用");
        }
        public override void Recv(object obj, XfsParameter parameter,NodeType nodeType)
        {
            TenCode tenCode = parameter.TenCode;
            switch (tenCode)
            {
                case (TenCode.Code0001):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + "18 XfsControllers: " + tenCode);
                    Console.WriteLine(XfsTimerTool.CurrentTime() + "19 XfsControllers: " + parameter.ElevenCode);

                    string va = XfsParameterTool.GetValue<string>(parameter, parameter.ElevenCode.ToString());
                    XfsModelObjects.Tests.Add(va);

                    Console.WriteLine(XfsTimerTool.CurrentTime() + "23 XfsHandlers: " + "" + va);

                    //XfsGame.XfsSence.GetComponent<XfsBookerHandler>().OnTransferParameter(this, parameter);

                    break;
                case (TenCode.Code0002):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsControllers: " + tenCode);

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
