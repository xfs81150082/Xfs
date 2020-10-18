using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsServer
{
    class XfsServerInit
    {
        public string IpString { get; set; } = "127.0.0.1";           //监听的IP地址  
        public int Port { get; set; } = 8115;                              //监听的端口  
        public int MaxListenCount { get; set; } = 10;                      //服务器程序允许的最大客户端连接数 
    
        public XfsServerInit()
        {
            
        }

        public XfsServerInit(string ipString, int port, int maxListenCount)
        {

        } 

        //程序启动入口
        void Init()
        {
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ... ");
            Thread.Sleep(1);
            SenceInit();
            SystemManager();


            //Thread.CurrentThread.Name = "TumoWorld";
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadName:" + Thread.CurrentThread.Name);
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadId:" + Thread.CurrentThread.ManagedThreadId);

            //Console.ReadKey();
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " 退出监听，并关闭程序。");

        }
        void Init(string ipString, int port, int maxListenCount)
        {
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ... ");
            Thread.Sleep(1);
            SenceInit();
            SystemManager();


            //Thread.CurrentThread.Name = "TumoWorld";
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadName:" + Thread.CurrentThread.Name);
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadId:" + Thread.CurrentThread.ManagedThreadId);

            //Console.ReadKey();
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " 退出监听，并关闭程序。");

        }
        void SenceInit()
        {
            XfsGame.XfsSence.AddComponent(new XfsServerSocket());          ///服务器加载组件 : 套接字 外网 传输数据组件
            XfsGame.XfsSence.AddComponent(new XfsMysqlConnection());       ///服务器加载组件 : 数据库链接组件TmSystem类型
            XfsGame.XfsSence.AddComponent(new XfsGateHandler());           ///服务器加载组件 : 服务器网关组件
            XfsGame.XfsSence.AddComponent(new XfsMysqlHandler());          ///服务器加载组件 : 数据库表格组件集
            XfsGame.XfsSence.AddComponent(new XfsUserHandler());           ///服务器加载组件 : 用户处理组件
            
            //XfsGame.TmSence.AddComponent(new TmEngineerHandler());       ///服务器加载组件 : Engineer 处理组件
            //XfsGame.TmSence.AddComponent(new TmBookerHandler());         ///服务器加载组件 : Booker 处理组件
            //XfsGame.TmSence.AddComponent(new TmTeacherHandler());        ///服务器加载组件 : Teacher 处理组件
            //XfsGame.TmSence.AddComponent(new TmStatusSyncHandler());     ///服务器加载组件 : TmStatusSyncHandler 处理组件
            //XfsGame.TmSence.AddComponent(new TmAbilityHandler());        ///服务器加载组件 : TmAbilityHandler 处理组件
            //XfsGame.TmSence.AddComponent(new TmKnapsackHandler());       ///服务器加载组件 : TmKnapsackHandler 处理组件


        }

        void SystemManager()
        {
            XfsGame.XfsSystemMananger.AddComponent(new ServerTest());     ///测试用
           
            //XfsGame.XfsSystemMananger.AddComponent(new XfsBookerDBSystem());        ///服务器加载组件 : 数据库链接组件TmSystem类型
            //XfsGame.TmSystemMananger.AddComponent(new TmEngineerSystem());        ///服务器加载组件 : 数据库链接组件TmSystem类型
            //XfsGame.TmSystemMananger.AddComponent(new TmSoulerDBSystem());       ///服务器加载组件 : 数据库链接组件TmSystem类型
            //XfsGame.XfsSystemMananger.AddComponent(new TmEngineerDBSystem());      ///服务器加载组件 : 数据库链接组件TmSystem类型
            //XfsGame.XfsSystemMananger.AddComponent(new TmTeacherDBSystem());       ///服务器加载组件 : 数据库链接组件TmSystem类型
            //XfsGame.XfsSystemMananger.AddComponent(new TmInventoryDBSystem());     ///服务器加载组件 : 数据库链接组件TmSystem类型
            //XfsGame.XfsSystemMananger.AddComponent(new TmSkillDBSystem());         ///服务器加载组件 : 数据库链接组件TmSystem类型


        }

        void Init(XfsNodeType type)
        {
            switch (type)
            {
                case XfsNodeType.Login:
                    Thread.Sleep(1);

                    ///服务器加载组件
                    XfsGame.XfsSence.AddComponent(new XfsNode(XfsNodeType.Login));                        ///服务器加载组件 : 服务器类型组件
                    XfsGame.XfsSence.AddComponent(new XfsMysql("127.0.0.1","tumoworld","root",""));       ///服务器加载组件 : 数据库链接组件
                    XfsGame.XfsSence.AddComponent(new XfsTcpServer());                                    ///服务器加载组件 : 通信组件Server
                    //XfsGame.XfsSence.AddComponent(new XfsTcpClient());                                    ///服务器加载组件 : 通信组件Client
                    
                    ///服务器加载组件驱动程序
                    XfsGame.XfsSence.AddComponent(new XfsMysqlSystem());                                  ///服务器加载组件 : 数据库链接组件TmSystem类型
                    XfsGame.XfsSence.AddComponent(new XfsTcpServerSystem());                              ///服务器加载组件 : 套接字 外网 传输数据组件
                    //XfsGame.XfsSence.AddComponent(new XfsTcpClientSystem());                              ///服务器加载组件 : 套接字 外网 传输数据组件
                   
                    break;
                case XfsNodeType.Gate:

                    break;
                case XfsNodeType.Game:

                    break;
                case XfsNodeType.Node1:

                    break;
                case XfsNodeType.Node2:

                    break;
                case XfsNodeType.Node3:

                    break;
                case XfsNodeType.Node4:

                    break;
                case XfsNodeType.BD:

                    break;
                case XfsNodeType.All:

                    break;
                default:

                    break;
            }
        }


        }


}
