using System;
using System.Net.Sockets;
namespace Xfs
{
    public class XfsPeer : XfsTcpSession
    {
        public XfsPeer()
        {
            AddComponent(new XfsPeerSession());
            AddComponent(new XfsCoolDown(this.EcsId));
            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsPeer:" + this.NodeType + ":" + this.IsServer);
        }
        public XfsPeer(NodeType nodeType)
        {
            this.IsServer = true;
            this.NodeType = nodeType;
            AddComponent(new XfsPeerSession());
            AddComponent(new XfsCoolDown(this.EcsId));

            Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsPeer:" + this.NodeType + ":" + this.IsServer);
        }
        public override void OnConnect()
        {
            XfsTcpServer server = null;
            XfsSockets.XfsTcpServers.TryGetValue(this.NodeType, out server);
            if (server != null)
            {
                ///显示与客户端连接
                Console.WriteLine("{0} 客户端{1}连接成功", XfsTimerTool.CurrentTime(), Socket.RemoteEndPoint);

                ///tpeer已经加入字典
                server.TPeers.Add(this.EcsId, this);
                Console.WriteLine(XfsTimerTool.CurrentTime() + " ComponentId: " + this.EcsId + " 已经加入字典");
                ///显示客户端群中的客户端数量
                Console.WriteLine(XfsTimerTool.CurrentTime() + " TPeers Count: " + server.TPeers.Count);              
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
               
            request.Keys.Add(this.EcsId);

            this.Recv(request);

            //XfsTcpServer server = null;
            //XfsSockets.XfsTcpServers.TryGetValue(this.NodeType, out server);
            //if (server != null)
            //{
            //    server.Recv(request);
            //}

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
                    XfsSockets.XfsHandlers.TryGetValue(this.NodeType, out handler);
                    if (handler != null)
                    {
                        handler.Recv(this, parameter);
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " RecvParameters: " + this.RecvParameters.Count);
                    }
                    else
                    {
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsHandler is null.");
                        break;
                    }
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
                Console.WriteLine(XfsTimerTool.CurrentTime() + " OnSendMvcParameters143: " + ex.Message);
            }
        }
        #endregion

        public override void XfsDispose()
        {
            XfsTcpServer server = null;
            XfsSockets.XfsTcpServers.TryGetValue(this.NodeType, out server);
            if (server != null)
            {
                base.XfsDispose();
                ///删除掉心跳包群中对应的peer
                server.TPeers.Remove(EcsId);
                Console.WriteLine(XfsTimerTool.CurrentTime() + "{0} 服务端{1}断开连接", XfsTimerTool.CurrentTime(), EcsId);
                Console.WriteLine(XfsTimerTool.CurrentTime() + " 一个客户端:已经中断连接" + " TPeers: " + server.TPeers.Count);

            }
        }

    }
}