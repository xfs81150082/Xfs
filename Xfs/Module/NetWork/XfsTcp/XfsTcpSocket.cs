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
        ///Properties
        public string IpString { get; set; } = "127.0.0.1";                //监听的IP地址  
        public int Port { get; set; } = 8115;                              //监听的端口  
        public IPAddress Address { get; set; }                             //监听的IP地址  
        public bool IsRunning { get; set; } = false;                       //服务器是否正在运行
        public int ValTime { get; set; } = 4000;
        public Socket NetSocket { get; set; }                              //服务器使用的异步socket

    }  
}