using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
namespace Xfs
{
    public abstract class XfsTcpServer : XfsTcpSocket
    {
        public abstract NodeType NodeType { get; }                          //服务器类型
        public Dictionary<string, XfsPeer> TPeers { get; set; } = new Dictionary<string, XfsPeer>();
        public XfsTcpServer()
        {
            XfsSockets.XfsTcpServers.Add(this.NodeType, this);
        }
        #region ///启动保持监听
        public void Listening()
        {
            if (!this.IsRunning)
            {               
                if (this.NetSocket == null)
                {
                    this.Address = IPAddress.Parse(this.IpString);
                    this.NetSocket = new Socket(this.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                }
                this.NetSocket.Bind(new IPEndPoint(this.Address, this.Port));
                this.NetSocket.Listen(this.MaxListenCount);               
                this.IsRunning = true;

                Console.WriteLine(" {0} 服务启动，监听{1}成功", XfsTimerTool.CurrentTime(), this.NetSocket.LocalEndPoint);
               
                ///开始一个异步操作以接受传入的一个连接尝试
                this.NetSocket.BeginAccept(new AsyncCallback(this.AcceptCallback), this.NetSocket);
            }
            //Console.WriteLine("54 {0} 服务启动，监听{1}成功", XfsTimerTool.CurrentTime(), this.NetSocket.LocalEndPoint);
        }
        private void AcceptCallback(IAsyncResult ar)
        {
            Socket server = (Socket)ar.AsyncState;
            Socket peerSocket = server.EndAccept(ar);
            ///触发事件///创建一个方法接收peerSocket (在方法里创建一个peer来处理读取数据//开始接受来自该客户端的数据)
            this.ReceiveSocket(peerSocket);
            ///接受下一个请求  
            server.BeginAccept(new AsyncCallback(this.AcceptCallback), server);
        }
        private void ReceiveSocket(Socket socket)
        {
            ///限制监听数量
            if (this.TPeers.Count >= this.MaxListenCount)
            {
                ///触发事件///在线排队等待
                this.WaitingSockets.Enqueue(socket);
            }
            else
            {
                ///创建一个TPeer接收socket
                new XfsPeer(this.NodeType).BeginReceiveMessage(socket);
            }
        }
        #endregion

        //#region ///接收参数信息
        //public void Recv(XfsParameter parameter)
        //{
        //        this.RecvParameters.Enqueue(parameter);
        //        this.OnrRecvParameters();
        //}
        //void OnrRecvParameters()
        //{
        //    try
        //    {
        //        while (this.RecvParameters.Count > 0)
        //        {
        //            XfsParameter parameter = this.RecvParameters.Dequeue();
        //            XfsHandler handler = null;
        //            XfsSockets.XfsHandlers.TryGetValue(this.NodeType, out handler);
        //            if (handler != null)
        //            {
        //                handler.Recv(this, parameter);
        //                Console.WriteLine(XfsTimerTool.CurrentTime() + " RecvParameters: " + this.RecvParameters.Count);
        //            }
        //            else
        //            {
        //                Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsHandler is null.");
        //                break;
        //            }                
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + ex.Message);
        //    }
        //}
        //#endregion       

        //#region ///发送参数信息
        //public void Send(XfsParameter mvc)
        //{
        //    this.SendParameters.Enqueue(mvc);
        //    OnSendMvcParameters();
        //}
        /////处理发送参数信息
        //void OnSendMvcParameters()
        //{
        //    try
        //    {
        //        while (this.SendParameters.Count > 0)
        //        {
        //            XfsParameter response = SendParameters.Dequeue();

        //            //Console.WriteLine(XfsTimerTool.CurrentTime() + " OnSendMvcParameters，Keys: " + response.Keys[0]);
        //            //Console.WriteLine(XfsTimerTool.CurrentTime() + " OnSendMvcParameters，TPeers: " + this.TPeers.ToList()[0]);


        //            while (response.Keys.Count > 0)
        //            {
        //                XfsPeer tpeer;
        //                this.TPeers.TryGetValue(response.Keys[0], out tpeer);
        //                ///用Json将参数（MvcParameter）,序列化转换成字符串（string）
        //                string mvcJsons = XfsJson.ToString<XfsParameter>(response);
        //                if (tpeer != null)
        //                {
        //                    tpeer.SendString(mvcJsons);
        //                }
        //                else
        //                {
        //                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 没找TPeer，用Key: " + response.Keys[0]);
        //                    break;
        //                }
        //                response.Keys.Remove(response.Keys[0]);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(XfsTimerTool.CurrentTime() + " OnSendMvcParameters143: " + ex.Message);
        //    }
        //}
        //#endregion
        
        
    }
}
