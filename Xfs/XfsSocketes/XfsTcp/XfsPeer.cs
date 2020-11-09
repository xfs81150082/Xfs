using System;
using System.Net.Sockets;
namespace Xfs
{
    public class XfsPeer : XfsTcpSession
    {
        public XfsPeer()
        {
            this.IsPeer = true;
            AddComponent(new XfsPeerSession());
            AddComponent(new XfsCoolDown(this.InstanceId));
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsPeer:" + this.SenceType + ":" + this.IsPeer);
        }
        public XfsPeer(XfsSenceType senceType)
        {
            this.SenceType = senceType;
            this.IsPeer = true;
            AddComponent(new XfsPeerSession());
            AddComponent(new XfsCoolDown(this.InstanceId));

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsPeer:" + this.SenceType + ":" + this.IsPeer);
        }
        public override void OnConnect()
        {
            XfsTcpServer server = null;
            XfsSockets.XfsTcpServers.TryGetValue(this.SenceType, out server);
            if (server != null)
            {
                ///显示与客户端连接
                Console.WriteLine("{0} 客户端{1}连接成功", XfsTimeHelper.CurrentTime(), Socket.RemoteEndPoint);

                ///tpeer已经加入字典
                server.TPeers.Add(this.InstanceId, this);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " ComponentId: " + this.InstanceId + " 已经加入字典");
                ///显示客户端群中的客户端数量
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " TPeers Count: " + server.TPeers.Count);              
            }
        }
        public override void OnTransferParameter(object obj, XfsParameter request)
        {
            ///将字符串string,用json反序列化转换成MvcParameter参数
            if (request.TenCode == TenCode.Zero)
            {
                this.GetComponent<XfsCoolDown>().CdCount = 0;
                return;
            }
            ///将MvcParameter参数分别列队并处理
            if (request.Back)
            {
            }               
            request.Keys.Add(this.InstanceId);
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
                        break;
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

                    //Console.WriteLine(XfsTimerTool.CurrentTime() + " OnSendMvcParameters，Keys: " + response.Keys[0]);
                    //Console.WriteLine(XfsTimerTool.CurrentTime() + " OnSendMvcParameters，TPeers: " + this.TPeers.ToList()[0]);


                    while (response.Keys.Count > 0)
                    {
                        ///用Json将参数（MvcParameter）,序列化转换成字符串（string）
                        string mvcJsons = XfsJson.ToString<XfsParameter>(response);
                        this.SendString(mvcJsons);

                        response.Keys.Remove(response.Keys[0]);

                        //XfsPeer tpeer;
                        //this.TPeers.TryGetValue(response.Keys[0], out tpeer);
                        //if (tpeer != null)
                        //{
                        //    tpeer.SendString(mvcJsons);
                        //}
                        //else
                        //{
                        //    Console.WriteLine(XfsTimerTool.CurrentTime() + " 没找TPeer，用Key: " + response.Keys[0]);
                        //    break;
                        //}
                        //response.Keys.Remove(response.Keys[0]);
                    }
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
            XfsTcpServer server = null;
            XfsSockets.XfsTcpServers.TryGetValue(this.SenceType, out server);
            if (server != null)
            {
                base.Dispose();
                ///删除掉心跳包群中对应的peer
                server.TPeers.Remove(InstanceId);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + "{0} 服务端{1}断开连接", XfsTimeHelper.CurrentTime(), InstanceId);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " 一个客户端:已经中断连接" + " TPeers: " + server.TPeers.Count);

            }
        }

    }
}