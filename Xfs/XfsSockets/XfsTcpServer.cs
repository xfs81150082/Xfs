using System;
using System.Net;
using System.Net.Sockets;
namespace Xfs
{
    public class XfsTcpServer : XfsTcpSocket
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

                    if (XfsHandler.Instance != null)
                    {
                        XfsHandler.Instance.Recv(this, parameter);
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " RecvParameters: " + this.RecvParameters.Count);
                    }
                    else
                    {
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsHandler is null.");
                        break;
                    }

                    //if (TmGateHandler.Instance != null)
                    //{
                    //    TmGateHandler.Instance.OnTransferParameter(this, parameter);
                    //    Console.WriteLine(TmTimerTool.CurrentTime() + " RecvParameters: " + TcpServer.RecvParameters.Count);
                    //}
                    //else
                    //{
                    //    Console.WriteLine(TmTimerTool.CurrentTime() + " XfsHandler is null.");
                    //    break;
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + ex.Message);
            }
        }
        #endregion       

        #region ///发送参数信息
        public void Send(XfsParameter mvc)
        {
            SendParameters.Enqueue(mvc);
            OnSendMvcParameters();
        }
        ///处理发送参数信息
        void OnSendMvcParameters()
        {
            try
            {
                while (SendParameters.Count > 0)
                {
                    XfsParameter mvc = SendParameters.Dequeue();
                    while (mvc.Keys.Count > 0)
                    {
                        XfsTcpSession tpeer;
                        TPeers.TryGetValue(mvc.Keys[0], out tpeer);
                        ///用Json将参数（MvcParameter）,序列化转换成字符串（string）
                        string mvcJsons = XfsJson.ToString<XfsParameter>(mvc);
                        if (tpeer != null)
                        {
                            tpeer.SendString(mvcJsons);
                        }
                        else
                        {
                            Console.WriteLine(XfsTimerTool.CurrentTime() + " 没找TPeer，用Key: " + mvc.Keys[0]);
                            break;
                        }
                        mvc.Keys.Remove(mvc.Keys[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " OnSendMvcParameters: " + ex.Message);
            }
        }
        #endregion

    }
}
