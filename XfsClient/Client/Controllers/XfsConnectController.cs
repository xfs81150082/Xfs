using Xfs;
using System;
using System.Collections.Generic;

namespace XfsClient
{
    public class XfsConnectController : XfsEntity
    {
        private static XfsConnectController _instance;
        public static XfsConnectController Instance { get => _instance; }
        public override void XfsAwake() { _instance = this; }
        //这个方法用来处理TPeer参数Mvc，并让结果给客户端响应（当客户端发起请求时调用）
        public override void OnTransferParameter(object obj, XfsParameter parameter)
        {
            TenCode tenCode = parameter.TenCode;
            switch (tenCode)
            {
                case (TenCode.Code0001):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " TmConnect: " + tenCode);
                    XfsGame.XfsSence.GetComponent<XfsBookerController>().OnTransferParameter(this, parameter);
                    break;
                case (TenCode.Code0002):
                    Console.WriteLine(XfsTimerTool.CurrentTime() + " TmConnect: " + tenCode);
                    XfsGame.XfsSence.GetComponent<XfsBookerController>().OnTransferParameter(this, parameter);
                    break;
                case (TenCode.End):
                    break;
                default:
                    break;
            }
        }

    }
}