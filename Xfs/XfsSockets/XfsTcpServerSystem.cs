using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Xfs
{
    public class XfsTcpServerSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            ValTime = 4000;
        }
        public override void XfsUpdate()
        {
            StartListen();
        }

        #region Methods Callbacks ///启动服务 ///接收参数消息     

        XfsTcpServer tcpServer = XfsGame.XfsSence.GetComponent<XfsTcpServer>();
        public void StartListen()
        {
            Console.WriteLine("29 {0} 服务启动，准备监听{1}", XfsTimerTool.CurrentTime(), tcpServer.IsRunning);

            if (tcpServer != null && !tcpServer.IsRunning)
            {
                if (tcpServer.NetSocket != null)
                {
                    tcpServer.NetSocket.Shutdown(SocketShutdown.Both);
                    tcpServer.NetSocket.Close();
                    tcpServer.NetSocket = null;
                }
                tcpServer.Address = IPAddress.Parse(tcpServer.IpString);
                tcpServer.NetSocket = new Socket(tcpServer.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                //tcpServer.Init();

                tcpServer.NetSocket.Bind(new IPEndPoint(tcpServer.Address, tcpServer.Port));
                tcpServer.NetSocket.Listen(tcpServer.MaxListenCount);
                tcpServer.NetSocket.BeginAccept(new AsyncCallback(this.AcceptCallback), tcpServer.NetSocket);

                tcpServer.IsRunning = true;

                Console.WriteLine("50 {0} 服务启动，监听{1}成功", XfsTimerTool.CurrentTime(), tcpServer.NetSocket.LocalEndPoint);
                Console.WriteLine("51 {0} 服务启动，监听{1}成功", XfsTimerTool.CurrentTime(), tcpServer.IsRunning);
            }

            Console.WriteLine("54 {0} 服务启动，监听{1}成功", XfsTimerTool.CurrentTime(), tcpServer.NetSocket.LocalEndPoint);
        }
        private void AcceptCallback(IAsyncResult ar)
        {
            Socket server = (Socket)ar.AsyncState;
            Socket peerSocket = server.EndAccept(ar);
            ///触发事件///创建一个方法接收peerSocket (在方法里创建一个peer来处理读取数据//开始接受来自该客户端的数据)
            TmReceiveSocket(peerSocket);
            ///接受下一个请求  
            server.BeginAccept(new AsyncCallback(this.AcceptCallback), server);
        }
        private void TmReceiveSocket(Socket socket)
        {
            ///限制监听数量
            if (tcpServer.TPeers.Count >= tcpServer.MaxListenCount)
            {
                ///触发事件///在线排队等待
                tcpServer.WaitingSockets.Enqueue(socket);
            }
            else
            {
                ///创建一个TPeer接收socket
                new XfsPeer().BeginReceiveMessage(socket);
                tcpServer.IsRunning = true;
            }
        }
        #endregion



    }
}
