﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Threading;

namespace Xfs
{
    public abstract class XfsTcpSocket : XfsComponent
    {
        //private static XfsTcpSocket _instance;
        //public static XfsTcpSocket Instance { get => _instance;  }
        //public XfsTcpSocket() { _instance = this; }
        #region Properties
        public string IpString { get; set; } = "127.0.0.1";           //监听的IP地址  
        public int Port { get; set; } = 8115;                              //监听的端口  
        public int MaxListenCount { get; set; } = 10;                      //服务器程序允许的最大客户端连接数  
        public bool IsRunning { get; set; } = false;                       //服务器是否正在运行
        public IPAddress Address { get; set; }                             //监听的IP地址  
        public Socket NetSocket { get; set; }                              //服务器使用的异步socket   
        public Queue<Socket> WaitingSockets = new Queue<Socket>();
        public Dictionary<string, XfsTcpSession> TPeers { get; set; } = new Dictionary<string, XfsTcpSession>();
        public XfsTcpSession TClient { get; set; }
        public Queue<XfsParameter> RecvParameters { get; set; } = new Queue<XfsParameter>();
        protected Queue<XfsParameter> SendParameters { get; set; } = new Queue<XfsParameter>();
        #endregion
        #region Constructor ///构造函数 ///初始化方法
        public void Init()
        {
            Address = IPAddress.Parse(IpString);
            NetSocket = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Init(string ipString, int port)
        {
            this.IpString = ipString;
            this.Port = port;
            if (NetSocket != null)
            {
                NetSocket.Close();
            }
            Address = IPAddress.Parse(ipString);
            NetSocket = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Init(string ipString, int port, int maxListenCount)
        {
            this.IpString = ipString;
            this.Port = port;
            this.MaxListenCount = maxListenCount;
            Address = IPAddress.Parse(ipString);
            NetSocket = new Socket(Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
        public virtual void StartConnect()  { }
        public virtual void StartListen()  { }
        #endregion
       
        //#region ///发送参数信息
        //public void Send(XfsParameter mvc)
        //{
        //    SendParameters.Enqueue(mvc);
        //    OnSendMvcParameters();
        //}
        //public abstract void OnSendMvcParameters();
        //#endregion
    }
}
