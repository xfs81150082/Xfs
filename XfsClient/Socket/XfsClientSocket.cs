using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Xfs;

namespace XfsClient
{
    public class XfsClientSocket : XfsSystem 
    {
        public XfsTcpClient XfsClient = null;
        public XfsClientSocket()
        {
            XfsClient = new XfsTcpClient();
        }
        public override void XfsUpdate()
        {
            ConnectToServer();
            ClientRecvParamers();
        }
        void ConnectToServer()
        {
            if (!XfsClient.IsRunning)
            {
                XfsClient.Init("127.0.0.1", 8115);
                //TmClient.Init("172.17.16.15", 8115);
                XfsClient.StartConnect();
                Console.WriteLine(XfsTimerTool.CurrentTime() + " Connecting...");
            }
        }
        void ClientRecvParamers()
        {
            try
            {
                while (XfsClient.RecvParameters.Count > 0)
                {
                    XfsParameter parameter = XfsClient.RecvParameters.Dequeue();
                    if (XfsConnectController.Instance != null)
                    {
                        XfsConnectController.Instance.OnTransferParameter(this, parameter); ///与客户端的接口函数
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " RecvParameters: " + XfsClient.RecvParameters.Count);
                    }
                    else
                    {
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " TumoConnect is null.");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " " + ex.Message);
            }
        }
    }
}