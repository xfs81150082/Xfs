﻿using System;
using System.Net;
using System.Net.Sockets;

namespace Xfs
{
    public abstract class XfsTcpClient : XfsTcpSocket
    {
        public abstract NodeType NodeType { get; }                         //服务器类型

        public XfsTcpClient() 
        {
            XfsSockets.XfsTcpClients.Add(this.NodeType, this);
        }


        #region ///启动保持连接  
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

                    Console.WriteLine(XfsTimerTool.CurrentTime() + " 38 " + ex.ToString());
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
                XfsReceiveSocket(tcpSocket);
                Console.WriteLine("{0} 连接服务器成功 {1}", XfsTimerTool.CurrentTime(), tcpSocket.RemoteEndPoint.ToString());
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
                this.TClient = new XfsClient(this.NodeType);
            }
            this.TClient.BeginReceiveMessage(socket);
            this.IsRunning = true;
        }
        #endregion

        #region ///接收参数信息
        public void Recv(XfsParameter parameter)
        {
            RecvParameters.Enqueue(parameter);
            OnrRecvParameters();
        }
        void OnrRecvParameters()
        {
            try
            {
                while (this.RecvParameters.Count > 0)
                {
                    XfsParameter parameter = this.RecvParameters.Dequeue();
                    XfsController controller = null;
                    XfsSockets.XfsControllers.TryGetValue(this.NodeType,out controller);
                    if (controller != null)
                    {
                        controller.Recv(this, parameter);
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " RecvParameters: " + this.RecvParameters.Count);
                    }
                    else
                    {
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " XfsController is null.");
                        break;
                    }                  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " " + ex.Message);
            }
        }
        #endregion

        #region ///发送参数信息
        public void Send(XfsParameter mvc)
        {
            SendParameters.Enqueue(mvc);
            OnSendParameters();
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
                    string mvcJsons = XfsJson.ToString<XfsParameter>(mvc);
                    if (this.TClient != null)
                    {
                        this.TClient.SendString(mvcJsons);
                    }
                    else
                    {
                        if (IsRunning)
                        {
                            IsRunning = false;
                            this.Connecting();
                            Console.WriteLine(XfsTimerTool.CurrentTime() + " TClient is Null. new TClient() 重新连接。");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " SendMvcParameters: " + ex.Message);
            }
        }
        #endregion

    }
}
