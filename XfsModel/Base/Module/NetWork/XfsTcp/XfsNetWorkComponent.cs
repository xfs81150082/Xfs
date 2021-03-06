﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
namespace Xfs
{
    public abstract class XfsNetWorkComponent : XfsEntity
    {
        #region ///自定义属性
        public bool IsServer { get; private set; }                         //服务端？客户端？
        public string IpString { get; set; } = "127.0.0.1";                //监听的IP地址  
        public int Port { get; set; } = 2001;                              //监听的端口  
        public IPAddress Address { get; set; }                             //监听的IP地址  
        public IPEndPoint IPEndPoint { get; set; }                         //监听的IP地址  
        public bool IsRunning { get; set; } = false;                       //服务器是否正在运行
        public Socket NetSocket { get; set; }                              //服务器使用的异步socket
        public int MaxListenCount { get; set; } = 10;                      //服务器程序允许的最大客户端连接数  
        public IXfsMessageDispatcher MessageDispatcher { get; set; }

        public Queue<Socket> WaitingSockets = new Queue<Socket>();
        public Dictionary<long, XfsSession> Sessions { get; set; } = new Dictionary<long, XfsSession>();
        public XfsSession Session { get; set; } 
        public XfsDoubleMap<long, XfsSceneType> sences { get; set; } = new XfsDoubleMap<long, XfsSceneType>();
        #endregion

        #region ///服务端专用，启动保持监听
        public XfsNetWorkComponent()
        {
        }
        public XfsNetWorkComponent(bool isServer)
        {
            this.IsServer = isServer;
        }
        public void Init(string ipString, int port, int maxListenCount, bool isServer)
        {
            this.IpString = ipString;
            this.Port = port;
            this.MaxListenCount = maxListenCount;
            this.IsServer = isServer;
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsNetWorkComponent-IsServer: " + this.IsServer);
        }
        public void Listening()
        {
            if (!this.IsServer) return;
            if (!this.IsRunning)
            {
                if (this.NetSocket == null)
                {
                    this.Address = IPAddress.Parse(this.IpString);
                    this.IPEndPoint = new IPEndPoint(this.Address, this.Port);
                    this.NetSocket = new Socket(this.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    this.NetSocket.Bind(this.IPEndPoint);
                }
                this.NetSocket.Listen(this.MaxListenCount);
                this.IsRunning = true;

                Console.WriteLine("{0} 服务启动，监听{1}成功", XfsTimeHelper.CurrentTime(), this.NetSocket.LocalEndPoint);

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
        #endregion

        #region ///客户端专用，启动保持连接
        public void Init(string ipString, int port, bool isServer)
        {
            this.IpString = ipString;
            this.Port = port;
            this.IsServer = isServer;
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsNetWorkComponent-IsServer: " + this.IsServer);
        }
        public void Connecting()    //连接服务器
        {
            if (this.IsServer) return;
            if (!this.IsRunning || this.Session == null)
            {
                try
                {
                    if (this.NetSocket == null)
                    {
                        this.Address = IPAddress.Parse(this.IpString);
                        this.NetSocket = new Socket(this.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                        this.IPEndPoint = new IPEndPoint(this.Address, this.Port);
                    }
                    this.NetSocket.BeginConnect(this.IPEndPoint, new AsyncCallback(this.ConnectCallback), this.NetSocket);
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
            ///触发事件//创建一个Socket接收传递过来的Socket
            Socket client = (Socket)ar.AsyncState;
            try
            {
                if (null == client.Handle || !client.Connected)
                {
                    this.IsRunning = false;
                    return;
                }
                //得到成功的连接
                client.EndConnect(ar);
                ///创建一个方法接收socket (在方法里创建一个session来处理读取数据//开始接受来自该客户端的数据)
                this.ReceiveSocket(client);            
                this.IsRunning = true;

                Console.WriteLine("{0} 连接服务器成功 {1}", XfsTimeHelper.CurrentTime(), client.RemoteEndPoint.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion

        #region ///服务器与客户端共用
        private void ReceiveSocket(Socket socket)
        {
            if (this.IsServer)
            {
                ///限制监听数量
                if (this.Sessions.Count >= this.MaxListenCount)
                {
                    ///触发事件///在线排队等待
                    this.WaitingSockets.Enqueue(socket);
                }
                else
                {
                    ///创建一个XfsSession接收socket
                    XfsSession session = GreateSession();
                    if (this.Sessions.TryGetValue(session.InstanceId, out XfsSession peer))
                    {
                        this.Sessions.Remove(this.InstanceId);
                        peer.Dispose();
                    }
                    this.Sessions.Add(session.InstanceId, session);
                    session.BeginReceiveMessage(socket);

                    Console.WriteLine(XfsTimeHelper.CurrentTime() + " IsServer: " + this.Sessions.Count + " Sessions: " + this.Sessions.Count + " RemoteAddress: " + session.RemoteAddress);
                }
            }
            else
            {
                ///创建一个XfsSession接收socket
                this.Session = GreateSession();
                this.Session.BeginReceiveMessage(socket);

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " IsServer: " + this.Sessions.Count + " Sessions: " + this.Sessions.Count + " RemoteAddress: " + this.Session.RemoteAddress);
            }
        }
        public XfsSession GreateSession()
        {
            ///创建一个session接收socket
            XfsSession session = XfsEntityFactory.CreateWithParent<XfsSession>(this);
            session.IsServer = this.IsServer;
            session.AddComponent<XfsHeartComponent>(); ///加上心跳包
            return session;
        }
        public virtual void Remove(long id)
        {
            XfsSession session;
            if (!this.Sessions.TryGetValue(id, out session))
            {
                return;
            }
            this.Sessions.Remove(id);
            session.Dispose();
        }
        public XfsSession Get(long id)
        {
            XfsSession session;
            this.Sessions.TryGetValue(id, out session);
            return session;
        }
     
        #endregion


    }
}