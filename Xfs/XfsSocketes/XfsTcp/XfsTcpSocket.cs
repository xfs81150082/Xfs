using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Policy;
using System.Threading;

namespace Xfs
{
    public abstract class XfsTcpSocket : XfsComponent
    {
        #region Properties
        public string IpString { get; set; } = "127.0.0.1";                //监听的IP地址  
        public IPAddress Address { get; set; }                             //监听的IP地址  
        public int Port { get; set; } = 8115;                              //监听的端口  
        public int MaxListenCount { get; set; } = 10;                      //服务器程序允许的最大客户端连接数  
        public bool IsRunning { get; set; } = false;                       //服务器是否正在运行
        public Socket NetSocket { get; set; }                              //服务器使用的异步socket
        public Queue<Socket> WaitingSockets = new Queue<Socket>();
        public Dictionary<string, XfsTcpSession> TPeers { get; set; } = new Dictionary<string, XfsTcpSession>();
        public XfsTcpSession TClient { get; set; }
        public Queue<XfsParameter> RecvParameters { get; set; } = new Queue<XfsParameter>();
        protected Queue<XfsParameter> SendParameters { get; set; } = new Queue<XfsParameter>();
        #endregion

        #region Constructor ///构造函数 ///初始化方法
        public void Init(string ipString, int port, int maxListenCount)
        {
            this.IpString = ipString;
            this.Port = port;
            this.MaxListenCount = maxListenCount;
        }
        #endregion

    }

    public enum NodeType
        {
            Client,
            Node,
            Gate,
            Login,
            Db,
            Game,
            All,
            None,

        }

}