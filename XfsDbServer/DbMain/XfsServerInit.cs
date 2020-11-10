using System;
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
            /// 服务器加载组件(D)
            XfsGame.XfsSence.Type = XfsSenceType.Db;
            //XfsGame.XfsSence.AddComponent<XfsMysql>();                ///("127.0.0.1", "tumoworld", "root", ""));   服务器加载组件 : 数据库链接组件
            XfsGame.XfsSence.AddComponent<XfsDbHandler>();              ///服务器加载组件 : Handler处理器
            XfsGame.XfsSence.AddComponent<XfsTcpServerDbNet>();         ///Init("127.0.0.1", 1001, 10)服务器加载组件 : 通信组件Server


        }




    }
}