using System;
using System.Threading;
using Xfs;
namespace XfsClient
{
    class XfsClientMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ... ");
            Thread.Sleep(4000);
            TmGameSenceInit();

            XfsGame.XfsSence.AddComponent(new XfsTest());                 ///客户端加载组件 : 测试组件1
            //XfsGame.XfsSence.AddComponent(new SyncTest());               ///客户端加载组件 : 测试组件2


            Thread.CurrentThread.Name = "TumoWorld";
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadName:" + Thread.CurrentThread.Name);
            Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadId:" + Thread.CurrentThread.ManagedThreadId);

            Console.ReadKey();
            Console.WriteLine(XfsTimerTool.CurrentTime() + " 退出联接，并关闭程序。");
        }
        static void TmGameSenceInit()
        {
            XfsGame.XfsSence.AddComponent(new XfsConnectController());              ///客户端加载组件 : 接收分发组件
            XfsGame.XfsSence.AddComponent(new XfsClientSocket());         ///客户端加载组件 : 套接字网络组件
            //TmGame.TmSence.AddComponent(new TmUserController());       ///客户端加载组件 : User处理组件
            //TmGame.TmSence.AddComponent(new TmEngineerController());   ///客户端加载组件 : Engineer处理组件
            //TmGame.TmSence.AddComponent(new TmBookerController());     ///客户端加载组件 : Booker处理组件
            //TmGame.TmSence.AddComponent(new TmTeacherController());    ///客户端加载组件 : Teacher处理组件
            //TmGame.TmSence.AddComponent(new TmStatusSyncController());    ///客户端加载组件 : Teacher处理组件
        }
    }
}
