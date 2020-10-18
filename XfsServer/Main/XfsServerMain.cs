using System;
using System.Collections.Generic;
using System.Threading;
using Xfs;

namespace XfsServer
{
    class XfsServerMain
    { 
        //程序启动入口
        static void Main(string[] args)
        {
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ... ");  
            Thread.Sleep(1);

            XfsSenceInit();
            XfsSystemManager();

            Thread.CurrentThread.Name = "TumoWorld";
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadName: " + Thread.CurrentThread.Name);
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);

            Console.ReadKey();
            Console.WriteLine(XfsTimerTool.CurrentTime() + " 退出监听，并关闭程序。");
        }

        static void XfsSenceInit()
        {
            ///服务器加载组件
            XfsGame.XfsSence.AddComponent(new XfsNode(XfsNodeType.Login));                        ///服务器加载组件 : 服务器类型组件
            //XfsGame.XfsSence.AddComponent(new XfsMysql("127.0.0.1", "tumoworld", "root", ""));    ///服务器加载组件 : 数据库链接组件
            XfsGame.XfsSence.AddComponent(new XfsTcpServer());                                    ///服务器加载组件 : 通信组件Server

        }

        static void XfsSystemManager()
        {
            ///服务器加载组件驱动程序
            //XfsGame.XfsSystemMananger.AddComponent(new ServerTest());     ///测试用
            //XfsGame.XfsSence.AddComponent(new XfsMysqlSystem());          ///服务器加载组件 : 数据库链接组件TmSystem类型
            XfsGame.XfsSence.AddComponent(new XfsTcpServerSystem());      ///服务器加载组件 : 套接字 外网 传输数据组件

        }

    }
}