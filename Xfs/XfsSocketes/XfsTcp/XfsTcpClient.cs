using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Xfs
{
    public abstract class XfsTcpClient : XfsTcpSocket
    {
        public abstract XfsSenceType SenceType { get; }                         //服务器类型
        public XfsClient TClient { get; set; }
        public XfsTcpClient() 
        {
            XfsSockets.XfsTcpClients.Add(this.SenceType, this);
        }
        #region ///启动保持连接  
        public void Init(string ipString, int port)
        {
            this.IpString = ipString;
            this.Port = port;           
        }

        public void Connecting()    //连接服务器
        {
            if (!this.IsRunning)
            {
                try
                {
                    if (this.NetSocket == null)
                    {
                        this.Address = IPAddress.Parse(this.IpString);
                        this.NetSocket = new Socket(this.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    }
                    this.NetSocket.BeginConnect(new IPEndPoint(this.Address, this.Port), new AsyncCallback(this.ConnectCallback), this.NetSocket);
                }
                catch (Exception ex)
                {
                    if (this.NetSocket != null)
                    {
                        this.NetSocket.Close();
                        this.NetSocket = null;
                    }
                    this.IsRunning = false;

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " 38 " + ex.ToString());
                }
            }
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            ///触发事件//创建一个Socket接收传递过来的TmSocket
            Socket tcpSocket = (Socket)ar.AsyncState;
            try
            {
                //得到成功的连接
                tcpSocket.EndConnect(ar);
                ///创建一个方法接收peerSocket (在方法里创建一个peer来处理读取数据//开始接受来自该客户端的数据)
                this.XfsReceiveSocket(tcpSocket);
                Console.WriteLine("{0} 连接服务器成功 {1}", XfsTimeHelper.CurrentTime(), tcpSocket.RemoteEndPoint.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void XfsReceiveSocket(Socket socket)
        {
            if (this.TClient == null)
            {
                ///创建一个TClient接收socket 
                //this.TClient = new XfsClient();

                this.TClient = XfsComponentFactory.Create<XfsClient>();
                this.TClient.SenceType = this.SenceType;
            }
            this.TClient.BeginReceiveMessage(socket);
            this.IsRunning = true;
        }
        #endregion

    }
}