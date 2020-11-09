﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsDbServer
{
    public class XfsServerInit
    {
       ///程序启动入口
        public void ConsoleInit()
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " ... ");
            Thread.Sleep(1);

            Init();

            Thread.CurrentThread.Name = "TumoWorld";
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " ThreadName: " + Thread.CurrentThread.Name);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);

            Console.ReadKey();
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " 退出监听，并关闭程序。");
        }

        public void Init()
        {
            //XfsGame.XfsSence/服务器加载组件(D)
            XfsGame.XfsSence.Type = XfsSenceType.Db;
            XfsGame.XfsSence.AddComponent(new XfsDbHandler());                                        ///服务器加载组件 : 通信组件Server
            //XfsGame.XfsSence.AddComponent(new XfsMysql("127.0.0.1", "tumoworld", "root", ""));     ///服务器加载组件 : 数据库链接组件
            XfsGame.XfsSence.AddComponent(new XfsTcpServerDbNet("127.0.0.1", 1001, 10));                                    ///服务器加载组件 : 通信组件Server


            ///服务器加载组件驱动程序
            //XfsGame.XfsSence.AddComponent(new XfsMysqlSystem());               ///服务器加载组件 : 数据库链接组件TmSystem类型
            XfsGame.XfsSence.AddComponent(new XfsTcpServerDbNetSystem());      ///服务器加载组件 : 套接字 外网 传输数据组件
            //XfsGame.XfsSence.AddComponent(new XfsTcpSessionSystem());            ///服务器加载组件 : 心跳包 组件

        }




    }
}