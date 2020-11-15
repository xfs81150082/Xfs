using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Xfs
{
    public class XfsClient : XfsEntity
    {
        public XfsPacketParser tcpSession { get; set; }
        public Socket Socket { get; set; }                ///创建一个套接字，用于储藏代理服务端套接字，与客户端通信///客户端Socket 
        public bool IsRunning { get; set; }
        public bool IsPeer { get; set; }
        public XfsSenceType SenceType { get; set; }
        public Queue<XfsMessageInfo> RecvResponses { get; set; } = new Queue<XfsMessageInfo>();
        protected Queue<XfsMessageInfo> SendRequests { get; set; } = new Queue<XfsMessageInfo>();

        public Queue<XfsParameter> RecvParameters { get; set; } = new Queue<XfsParameter>();
        protected Queue<XfsParameter> SendParameters { get; set; } = new Queue<XfsParameter>();
        protected Queue<XfsParameter> WaitingParameters { get; set; } = new Queue<XfsParameter>();
        public XfsClient()
        {
            this.IsPeer = false;
            this.AddComponent<XfsHeartComponent>();
            this.tcpSession = new XfsPacketParser();
            //this.tcpSession.SessionRecvBufferByte += this.RecvBufferBytes;
            this.tcpSession.ReadCallback += this.RecvBufferBytes;

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsClient:" + this.SenceType + ":" + this.IsPeer);
        }   
       

        #region ///接收参数信息     
        public void OnConnect()
        {
            ///显示与客户端连接
            Console.WriteLine("{0} 服务端{1}连接成功", XfsTimeHelper.CurrentTime(), Socket.RemoteEndPoint);
        }///与服务器连接时调用  


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

        public void OnTransferParameter(object obj, XfsParameter parameter)
        {
            ///将字符串string,用json反序列化转换成MvcParameter参数
            if (parameter.TenCode == TenCode.Zero)
            {
                this.GetComponent<XfsHeartComponent>().CdCount = 0;
             
                Console.WriteLine("{0} 客户端心跳包 {1} 接收成功", XfsTimeHelper.CurrentTime(), Socket.RemoteEndPoint);
            
                return;
            }






            this.Recv(parameter);
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " 157 XfsTcpSession is Client");       
        }
        public void Recv(XfsParameter response)
        {
            this.RecvParameters.Enqueue(response);
            this.OnRecvParameters();
        }
        void OnRecvParameters()
        {
            try
            {
                while (this.RecvParameters.Count > 0)
                {
                    XfsParameter response = this.RecvParameters.Dequeue();

                    ///requestCallback 如果回复信息，侧调用回调委托2020.11.6
                    Action<XfsParameter> action;
                    if (this.requestCallback.TryGetValue(response.RpcId, out action))
                    {
                        this.requestCallback.Remove(response.RpcId);
                        action(response);
                        continue;
                    }

                    XfsController controller = null;
                    XfsSockets.XfsControllers.TryGetValue(this.SenceType, out controller);
                    if (controller != null)
                    {
                        controller.Recv(this, response);
                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " RecvParameters: " + this.RecvParameters.Count);
                    }
                    else
                    {
                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsController is null.");
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " " + ex.Message);
            }
        }
        #endregion
        #region ///发送参数信息,需要回复，Call
        private static int RpcId { get; set; }
        private readonly Dictionary<int, Action<XfsParameter>> requestCallback = new Dictionary<int, Action<XfsParameter>>();

        public XfsTask<XfsParameter> Call(XfsParameter request)
        {
            int rpcId = ++RpcId;
            var tcs = new XfsTaskCompletionSource<XfsParameter>();

            this.requestCallback[rpcId] = (response) =>
            {
                try
                {
                    tcs.SetResult(response);
                }
                catch (Exception e)
                {
                    tcs.SetException(new Exception($"Rpc Error: {request.EcsId}", e));
                }
            };

            request.RpcId = rpcId;
            this.Send(request);
            return tcs.Task;
        }
  
        #endregion
        #region ///发送参数信息
     
        public void Send(XfsParameter mvc)
        {
            this.SendParameters.Enqueue(mvc);
            this.OnSendParameters();
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
                    string mvcJsons = XfsJsonHelper.ToString<XfsParameter>(mvc);

                    this.tcpSession.SendString(mvcJsons);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " SendMvcParameters: " + ex.Message);
            }
        }
        #endregion
        public override void Dispose()
        {
            base.Dispose();
            if((this.Parent as XfsTcpClient).TClient != null)
            {
                if((this.Parent as XfsTcpClient).TClient.InstanceId == this.InstanceId)
                {
                    (this.Parent as XfsTcpClient).TClient = null;
                }
            }

            this.Socket.Close();
            this.IsRunning = false;
            //this.tcpSession.Dispose();
            Console.WriteLine("{0} 服务端 {1} 断开连接", XfsTimeHelper.CurrentTime(), InstanceId);

        }///与服务器断开时调用                      
    }
}