using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Sockets;
namespace Xfs
{
    public abstract class XfsTcpServer : XfsComponent
    {
        ///Properties
        public string IpString { get; set; } = "127.0.0.1";                //监听的IP地址  
        public int Port { get; set; } = 2001;                              //监听的端口  
        public IPAddress Address { get; set; }                             //监听的IP地址  
        public bool IsRunning { get; set; } = false;                       //服务器是否正在运行
        public int ValTime { get; set; } = 4000;
        public Socket NetSocket { get; set; }                              //服务器使用的异步socket
        public int MaxListenCount { get; set; } = 10;                      //服务器程序允许的最大客户端连接数  
      
        public Queue<Socket> WaitingSockets = new Queue<Socket>();
        public abstract XfsSenceType SenceType { get; }                          //服务器类型
        public Dictionary<long, XfsPeer> TPeers { get; set; } = new Dictionary<long, XfsPeer>();
        public XfsTcpServer()
        {
            XfsSockets.XfsTcpServers.Add(this.SenceType, this);
        }
        public void Init(string ipString, int port, int maxListenCount)
        {
            this.IpString = ipString;
            this.Port = port;
            this.MaxListenCount = maxListenCount;
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

                Console.WriteLine(" {0} 服务启动，监听{1}成功", XfsTimeHelper.CurrentTime(), this.NetSocket.LocalEndPoint);
               
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
                XfsPeer xfsPeer = XfsComponentFactory.Create<XfsPeer>();
                xfsPeer.SenceType = this.SenceType;
                xfsPeer.BeginReceiveMessage(socket);
                xfsPeer.OnConnect();

                //XfsComponentFactory.Create<XfsPeer>().BeginReceiveMessage(socket);
            }
        }
        #endregion        
        
    }
}