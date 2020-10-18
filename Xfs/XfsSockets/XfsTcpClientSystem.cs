using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsTcpClientSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            ValTime = 4000;
        }
        public override void XfsUpdate()
        {
            StartConnect();
        }
        #region Methods Callbacks ///启动服务 ///接收参数消息  
        XfsTcpClient client = XfsGame.XfsSence.GetComponent<XfsTcpClient>();
        public void StartConnect()    //开始连接
        {
            if (client != null && !client.IsRunning)
            {
                try
                {
                    client.NetSocket.BeginConnect(new IPEndPoint(client.Address, client.Port), new AsyncCallback(this.ConnectCallback), client.NetSocket);
                }
                catch (Exception ex)
                {
                    client.NetSocket.Close();
                    client.IsRunning = false;
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private void ConnectCallback(IAsyncResult ar)
        {
            //创建一个Socket接收传递过来的TmSocket
            Socket tcpSocket = (Socket)ar.AsyncState;
            try
            {
                //得到成功的连接
                tcpSocket.EndConnect(ar);
                ///触发事件///创建一个方法接收peerSocket (在方法里创建一个peer来处理读取数据//开始接受来自该客户端的数据)
                TmReceiveSocket(tcpSocket);
                Console.WriteLine("{0} 连接服务器成功 {1}", XfsTimerTool.CurrentTime(), tcpSocket.RemoteEndPoint.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void TmReceiveSocket(Socket socket)
        {
            if (client.TClient == null)
            {
                ///创建一个TClient接收socket       
                client.TClient = new XfsClient();
            }
            client.TClient.BeginReceiveMessage(socket);
            client.IsRunning = true;
        }
        #endregion

    }
}