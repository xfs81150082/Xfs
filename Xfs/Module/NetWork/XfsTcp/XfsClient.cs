using System;
using System.Collections.Generic;

namespace Xfs
{
    public class XfsClient : XfsTcpSession
    {
        public XfsClient()
        {
            this.IsPeer = false;
            this.AddComponent<XfsHeartComponent>();

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsClient:" + this.SenceType + ":" + this.IsPeer);
        }
        public void OnConnect()
        {
            ///显示与客户端连接
            Console.WriteLine("{0} 服务端{1}连接成功", XfsTimeHelper.CurrentTime(), Socket.RemoteEndPoint);
        }///与服务器连接时调用  
        public override void OnTransferParameter(object obj, XfsParameter parameter)
        {
            ///将字符串string,用json反序列化转换成MvcParameter参数
            if (parameter.TenCode == TenCode.Zero)
            {
                this.GetComponent<XfsHeartComponent>().CdCount = 0;
                return;
            }
            this.Recv(parameter);
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " 157 XfsTcpSession is Client");       
        }
        #region ///接收参数信息
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
                        break;
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
                        break;
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
        private readonly Dictionary<int, Action<XfsMessageInfo>> request2Callback = new Dictionary<int, Action<XfsMessageInfo>>();
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
  
        public XfsTask<XfsMessageInfo> Call(XfsMessageInfo request)
        {
            int rpcId = ++RpcId;
            var tcs = new XfsTaskCompletionSource<XfsMessageInfo>();

            this.request2Callback[rpcId] = (response) =>
            {
                try
                {
                    tcs.SetResult(response);
                }
                catch (Exception e)
                {
                    tcs.SetException(new Exception($"Rpc Error: {request.GetType()}", e));
                }
            };

            request.RpcId = rpcId;
            this.Send(request);
            return tcs.Task;
        }

        #endregion
        #region ///发送参数信息
        public void Send(XfsMessageInfo request)
        {
            this.SendRequests.Enqueue(request);
            this.OnSendRequests();
        }
        ///处理发送参数信息
        void OnSendRequests()
        {
            try
            {
                while (SendRequests.Count > 0)
                {
                    XfsMessageInfo request = SendRequests.Dequeue();

                    ///用Json将参数（MvcParameter）,序列化转换成字符串（string）
                    string mvcJsons = XfsJsonHelper.ToString<XfsMessageInfo>(request);
                    this.SendString(mvcJsons);                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " SendMvcParameters: " + ex.Message);
            }
        }
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
                    this.SendString(mvcJsons);

                    //if (this.TClient != null)
                    //{
                    //    this.TClient.SendString(mvcJsons);
                    //}
                    //else
                    //{
                    //    if (IsRunning)
                    //    {
                    //        IsRunning = false;
                    //        this.Connecting();
                    //        Console.WriteLine(XfsTimerTool.CurrentTime() + " TClient is Null. new TClient() 重新连接。");
                    //    }
                    //}
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
            XfsTcpClient client = null;
            XfsSockets.XfsTcpClients.TryGetValue(this.SenceType, out client);
            if (client != null)
            {
                base.Dispose();
                if (client.TClient != null && client.TClient.InstanceId == this.InstanceId)
                {
                    client.TClient = null;
                }
                ///设置连接中断，40秒后会自动重连
                client.IsRunning = false;
                Console.WriteLine("{0} 服务端{1}断开连接", XfsTimeHelper.CurrentTime(), InstanceId);

            }


            //if (XfsTcpClient.Instance.NodeType == this.NodeType)
            //{
            //    base.XfsDispose();
            //    if (XfsTcpClient.Instance.TClient != null && XfsTcpClient.Instance.TClient.EcsId == this.EcsId)
            //    {
            //        XfsTcpClient.Instance.TClient = null;
            //    }
            //    ///设置连接中断，40秒后会自动重连
            //    XfsTcpClient.Instance.IsRunning = false;
            //    Console.WriteLine("{0} 服务端{1}断开连接", XfsTimerTool.CurrentTime(), EcsId);
            //}


            //if (XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>() != null)
            //{
            //    if (XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().TClient != null && XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().TClient.EcsId == this.EcsId)
            //    {
            //        XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().TClient = null;
            //    }
            //    ///设置连接中断，40秒后会自动重连
            //    XfsGame.XfsSence.GetComponent<XfsTcpClientNodeNet>().IsRunning = false;
            //}

        }///与服务器断开时调用                      
    }
}