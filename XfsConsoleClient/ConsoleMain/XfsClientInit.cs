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
            XfsGame.XfsSence.AddComponent(new XfsNode(XfsNodeType.Client));                        ///服务器加载组件 : 服务器类型组件
            XfsGame.XfsSence.AddComponent(new XfsTcpClientNodeNet("127.0.0.1", 8115, 10));         ///服务器加载组件 : 通信组件Server
            XfsGame.XfsSence.AddComponent(new XfsControllers());                                   ///服务器加载组件 : 通信组件Server 

            
            XfsGame.XfsSence.AddComponent(new XfsTest());                                          ///服务器加载组件 : 通信组件Server


            ///服务器加载组件驱动程序
            XfsGame.XfsSystemMananger.AddComponent(new XfsTcpClientNodeNetSystem());        ///服务器加载组件 : 套接字 外网 传输数据组件
            XfsGame.XfsSystemMananger.AddComponent(new XfsTcpSessionSystem());              ///服务器加载组件 : 心跳包 组件


            XfsGame.XfsSystemMananger.AddComponent(new XfsTestSystem());                    ///服务器加载组件 : 套接字 外网 传输数据组件



            //XfsGame.XfsSence.AddComponent(new XfsTest());                 ///客户端加载组件 : 测试组件1
            //XfsGame.XfsSence.AddComponent(new XfsConnectController());    ///客户端加载组件 : 接收分发组件
            //XfsGame.XfsSence.AddComponent(new XfsClientSocket());         ///客户端加载组件 : 套接字网络组件
            //TmGame.TmSence.AddComponent(new TmUserController());          ///客户端加载组件 : User处理组件
            //TmGame.TmSence.AddComponent(new TmEngineerController());      ///客户端加载组件 : Engineer处理组件
            //TmGame.TmSence.AddComponent(new TmBookerController());        ///客户端加载组件 : Booker处理组件
            //TmGame.TmSence.AddComponent(new TmTeacherController());       ///客户端加载组件 : Teacher处理组件
            //TmGame.TmSence.AddComponent(new TmStatusSyncController());    ///客户端加载组件 : Teacher处理组件


        }








    }
}
