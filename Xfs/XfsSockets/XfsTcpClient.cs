using System;
using System.Net;
using System.Net.Sockets;

namespace Xfs
{
    public class XfsTcpClient : XfsTcpSocket
    {
        #region ///处理接受的参数信息
        public void Recv(XfsParameter parameter)
        {  
            RecvParameters.Enqueue(parameter);
            OnrRecvParameters();
        }
        void OnrRecvParameters()
        {
            try
            {
                while (this.RecvParameters.Count > 0)
                {
                    XfsParameter parameter = this.RecvParameters.Dequeue();
                    if (XfsController.Instance != null)
                    {
                        XfsController.Instance.Recv(this, parameter);
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " RecvParameters: " + this.RecvParameters.Count);
                    }
                    else
                    {
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " TumoGate is null.");
                        break;
                    }

                    //if (XfsConnectController.Instance != null)
                    //{
                    //    XfsConnectController.Instance.OnTransferParameter(this, parameter); ///与客户端的接口函数
                    //    Console.WriteLine(XfsTimerTool.CurrentTime() + " RecvParameters: " + this.RecvParameters.Count);
                    //}
                    //else
                    //{
                    //    Console.WriteLine(XfsTimerTool.CurrentTime() + " TumoConnect is null.");
                    //    break;
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " " + ex.Message);
            }
        }
        #endregion

        #region ///发送参数信息
        public void Send(XfsParameter mvc)
        {
            SendParameters.Enqueue(mvc);
            OnSendParameters();
        }
        ///处理发送参数信息
        void OnSendParameters()
        {
            try
            {
                while (SendParameters.Count > 0)
                {
                    XfsParameter mvc = SendParameters.Dequeue();
                    ///用Json将参数（MvcParameter）,序列化转换成字符串（string）
                    string mvcJsons = XfsJson.ToString<XfsParameter>(mvc);
                    if (TClient != null)
                    {
                        TClient.SendString(mvcJsons);
                    }
                    //else
                    //{
                    //    if (IsRunning)
                    //    {
                    //        IsRunning = false;
                    //        StartConnect();
                    //        Console.WriteLine(TmTimerTool.CurrentTime() + " TClient is Null. new TClient() 重新连接。");
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " SendMvcParameters: " + ex.Message);
            }
        }
        #endregion

    }
}
