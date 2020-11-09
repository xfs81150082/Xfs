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
        public abstract XfsSenceType SenceType { get; }                          //服务器类型
        public Dictionary<long, XfsPeer> TPeers { get; set; } = new Dictionary<long, XfsPeer>();
        public XfsTcpServer()
        {
            XfsSockets.XfsTcpServers.Add(this.SenceType, this);
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
                new XfsPeer(this.SenceType).BeginReceiveMessage(socket);
            }
        }
        #endregion        
        
    }
}