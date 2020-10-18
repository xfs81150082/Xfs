using Xfs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XfsServer
{
    public class XfsGateHandler : XfsEntity
    {
        private static XfsGateHandler _instance;
        public static XfsGateHandler Instance { get => _instance; }  
        public override void XfsAwake()
        {
            base.XfsAwake();
            _instance = this;
        }        
        //这个方法用来处理TPeer参数Mvc，并让结果给客户端响应（当客户端发起请求时调用）
        public override void OnTransferParameter(object obj, XfsParameter parameter)
        {
            TenCode tenCode = parameter.TenCode;
            switch (tenCode)
            {              
                case (TenCode.Code0001):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " TmGate: " + tenCode);
                    XfsGame.XfsSence.GetComponent<XfsBookerHandler>().OnTransferParameter(this, parameter);
                    break;
                case (TenCode.Code0002):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " TmGate: " + tenCode);
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
