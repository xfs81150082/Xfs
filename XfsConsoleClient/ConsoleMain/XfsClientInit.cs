using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    public class XfsClientInit
    {
        public XfsClientInit()
        {
            
        }

        public void ConsoelInit()
        {
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ... ");
            Thread.Sleep(2000);

            Init();

            Thread.CurrentThread.Name = "TumoWorld";
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadName:" + Thread.CurrentThread.Name);
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadId:" + Thread.CurrentThread.ManagedThreadId);

            Console.ReadKey();
            Console.WriteLine(XfsTimerTool.CurrentTime() + " 退出联接，并关闭程序。");

        }

        public void Init()
        {
            ///服务器加载组件
            XfsGame.XfsSence.Type = XfsSenceType.Client;
            XfsGame.XfsSence.AddComponent(new XfsTcpClientNodeNet("127.0.0.1", 2001, 10));         ///服务器加载组件 : 通信组件Server
            //XfsGame.XfsSence.AddComponent(new XfsTcpClientDbNet("127.0.0.1", 1001, 10));         ///服务器加载组件 : 通信组件Server
            //XfsGame.XfsSence.AddComponent(new XfsDbController());                                ///服务器加载组件 : 通信组件Server 
            XfsGame.XfsSence.AddComponent(new XfsNodeController());                                ///服务器加载组件 : 通信组件Server 

            XfsGame.XfsSence.AddComponent(new XfsTest());                                          ///服务器加载组件 : 通信组件Server

            ///服务器加载组件驱动程序
            //XfsGame.XfsSystemMananger.AddComponent(new XfsTcpClientDbNetSystem());        ///服务器加载组件 : 套接字 外网 传输数据组件
            XfsGame.XfsSence.AddComponent(new XfsTcpClientNodeNetSystem());        ///服务器加载组件 : 套接字 外网 传输数据组件
            XfsGame.XfsSence.AddComponent(new XfsClientSystem());            ///服务器加载组件 : 心跳包 组件
            //XfsGame.XfsSystemMananger.AddComponent(new XfsTcpSessionSystem());              ///服务器加载组件 : 心跳包 组件

            XfsGame.XfsSence.AddComponent(new XfsTestSystem());                    ///服务器加载组件 : 套接字 外网 传输数据组件


        }



    }
}
