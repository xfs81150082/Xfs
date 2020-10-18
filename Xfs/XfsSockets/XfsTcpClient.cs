using System;
using System.Net;
using System.Net.Sockets;

namespace Xfs
{
    public class XfsTcpClient : XfsTcpSocket
    {
        //#region Methods Callbacks ///接收参数消息
        //public override void StartConnect()    //开始连接
        //{
        //    if (!IsRunning)
        //    {
        //        try
        //        {
        //            netSocket.BeginConnect(new IPEndPoint(address, Port), new AsyncCallback(this.ConnectCallback), netSocket);
        //        }
        //        catch (Exception ex)
        //        {
        //            netSocket.Close();
        //            IsRunning = false;
        //            Console.WriteLine(ex.ToString());
        //        }
        //    }
        //}
        //private void ConnectCallback(IAsyncResult ar)
        //{
        //    //创建一个Socket接收传递过来的TmSocket
        //    Socket tcpSocket = (Socket)ar.AsyncState;
        //    try
        //    {
        //        //得到成功的连接
        //        tcpSocket.EndConnect(ar);
        //        ///触发事件///创建一个方法接收peerSocket (在方法里创建一个peer来处理读取数据//开始接受来自该客户端的数据)
        //        TmReceiveSocket(tcpSocket);
        //        Console.WriteLine("{0} 连接服务器成功 {1}", XfsTimerTool.CurrentTime(), tcpSocket.RemoteEndPoint.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}
        //public void TmReceiveSocket(Socket socket)
        //{
        //    ///创建一个TClient接收socket       
        //    TClient = new XfsClient();
        //    TClient.BeginReceiveMessage(socket);
        //    IsRunning = true;
        //}
        //#endregion

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
                        XfsController.Instance.Recv(parameter);
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
