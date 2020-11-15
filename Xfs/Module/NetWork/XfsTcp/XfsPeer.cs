using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Xfs
{
    public class XfsPeer : XfsEntity
    {
        public XfsPacketParser tcpSession { get; set; }
        public Socket Socket { get; set; }                ///创建一个套接字，用于储藏代理服务端套接字，与客户端通信///客户端Socket 
        public bool IsRunning { get; set; }
        public bool IsPeer { get; set; }
        public XfsSenceType SenceType { get; set; }
        public Queue<XfsParameter> RecvParameters { get; set; } = new Queue<XfsParameter>();
        protected Queue<XfsParameter> SendParameters { get; set; } = new Queue<XfsParameter>();
        protected Queue<XfsParameter> WaitingParameters { get; set; } = new Queue<XfsParameter>();
        public XfsPeer()
        {
            this.IsPeer = true;
            this.AddComponent<XfsHeartComponent>();
            this.tcpSession = new XfsPacketParser();
            //this.tcpSession.SessionRecvBufferByte += this.RecvBufferBytes;
            this.tcpSession.ReadCallback += this.RecvBufferBytes;

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsPeer:" + this.SenceType + ":" + this.IsPeer);
        }
        public void OnConnect()
        {
            ///显示与客户端连接
            Console.WriteLine("{0} 客户端 {1} 连接成功", XfsTimeHelper.CurrentTime(), Socket.RemoteEndPoint);
        }

        public void BeginReceiveMessage(Socket socket)
        {
            this.Socket = socket;
            this.OnConnect();
            tcpSession.BeginReceiveMessage(socket);
        }

        public void RecvBufferBytes(object obj, byte[] HeadBytes, byte[] BodyBytes)
        {
            //base.RecvBufferBytes(obj, HeadBytes, BodyBytes);

            ///一个包身BodyBytes消息包接收完毕，解析消息包
            string mvcString = Encoding.UTF8.GetString(BodyBytes, 0, BodyBytes.Length);

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " Recv HeadBytes {0} Bytes, BodyBytes {1} Bytes. ThreadId:{2} .", HeadBytes.Length, BodyBytes.Length, Thread.CurrentThread.ManagedThreadId);

            HeadBytes = null;
            BodyBytes = null;

            XfsParameter parameter = XfsJsonHelper.ToObject<XfsParameter>(mvcString);
            ///这个方法用来处理参数Mvc，并让结果给客户端响应（当客户端发起请求时调用）
            this.OnTransferParameter(this, parameter);
        }
        public void OnTransferParameter(object obj, XfsParameter request)
        {
            ///将字符串string,用json反序列化转换成MvcParameter参数
            if (request.TenCode == TenCode.Zero)
            {
                this.GetComponent<XfsHeartComponent>().CdCount = 0;

                Console.WriteLine("{0} 服务端心跳包 {1} 接收成功", XfsTimeHelper.CurrentTime(), Socket.RemoteEndPoint);

                return;
            }
            ///将MvcParameter参数分别列队并处理
            if (request.Back)
            {

            }

            request.PeerIds.Add(this.InstanceId);
            this.Recv(request);
        }
        #region ///接收参数信息
        public void Recv(XfsParameter parameter)
        {
            this.RecvParameters.Enqueue(parameter);
            this.OnrRecvParameters();
        }
        void OnrRecvParameters()
        {
            try
            {
                while (this.RecvParameters.Count > 0)
                {
                    XfsParameter parameter = this.RecvParameters.Dequeue();
                    XfsHandler handler = null;
                    XfsSockets.XfsHandlers.TryGetValue(this.SenceType, out handler);
                    if (handler != null)
                    {
                        handler.Recv(this, parameter);
                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " RecvParameters: " + this.RecvParameters.Count);
                    }
                    else
                    {
                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsHandler is null.");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + ex.Message);
            }
        }
        #endregion
        #region ///发送参数信息
        public void Send(XfsParameter mvc)
        {
            this.SendParameters.Enqueue(mvc);
            OnSendMvcParameters();
        }
        ///处理发送参数信息
        void OnSendMvcParameters()
        {
            try
            {
                while (this.SendParameters.Count > 0)
                {
                    XfsParameter response = SendParameters.Dequeue();

                    ///用Json将参数（MvcParameter）,序列化转换成字符串（string）
                    string mvcJsons = XfsJsonHelper.ToString<XfsParameter>(response);                  

                    this.tcpSession.SendString(mvcJsons);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " OnSendMvcParameters143: " + ex.Message);
            }
        }
        #endregion
        public override void Dispose()
        {
            base.Dispose();
            if ((this.Parent as XfsTcpServer) != null)
            {
                if ((this.Parent as XfsTcpServer).TPeers.Count > 0)
                {
                    //if ((this.Parent as XfsTcpServer).TPeers.TryGetValue(this.InstanceId, out XfsPeer peer))
                    {
                        (this.Parent as XfsTcpServer).TPeers.Remove(this.InstanceId);
                    }
                }
            }

            this.Socket.Close();
            this.IsRunning = false;
            //this.tcpSession.Dispose();
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " 一个客户端:已经中断连接, TPeers: " + (this.Parent as XfsTcpServer).TPeers.Count);

        }
    }
}