using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    public class XfsClientInit
    {
        public XfsClientInit()  {    }
        public void Start()
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " ... ");
            Thread.Sleep(1);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);

            // 异步方法全部会回掉到主线程
            //SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);

            try
            {
                XfsDLLType dllType1 = XfsDLLType.Xfs;
                XfsDLLType dllType2 = XfsDLLType.XfsConsoleClient;
                Assembly assembly1 = XfsDllHelper.GetAssembly(dllType1.ToString());
                Assembly assembly2 = XfsDllHelper.GetAssembly(dllType2.ToString());
                XfsGame.EventSystem.Add(dllType1, assembly1);
                XfsGame.EventSystem.Add(dllType2, assembly2);


                ///服务器加载组件
                XfsGame.XfsSence.Type = XfsSenceType.Client;
                XfsGame.XfsSence.AddComponent<XfsStartConfigComponent>();                         ///服务器加载组件 : 信息组件
                XfsGame.XfsSence.AddComponent<XfsOpcodeTypeComponent>();                          ///服务器加载组件 : 操作号码
                 XfsGame.XfsSence.AddComponent<XfsTcpClientNodeNet>();                          ///服务器加载组件 : 通信组件Server


                XfsGame.XfsSence.AddComponent<XfsTest>();                                      ///服务器加载组件 : 通信组件Server


                Console.WriteLine(XfsTimeHelper.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " 客户端配置完成： " + XfsGame.XfsSence.Type);

                while (true)
                {
                    try
                    {
                        Thread.Sleep(1);
                        //XfsOneThreadSynchronizationContext.Instance.Update();
                        XfsGame.EventSystem.Update();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
            }
        }
        public void ConsoelInit()
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " ... ");
            Thread.Sleep(2000);

            Init();

            Thread.CurrentThread.Name = "TumoWorld";
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " ThreadName:" + Thread.CurrentThread.Name);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " ThreadId:" + Thread.CurrentThread.ManagedThreadId);

            Console.ReadKey();
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " 退出联接，并关闭程序。");

        }

        public void Init()
        {
            ///服务器加载组件
            XfsGame.XfsSence.Type = XfsSenceType.Client;
            XfsGame.XfsSence.AddComponent<XfsTcpClientNodeNet>();                          ///服务器加载组件 : 通信组件Server

            XfsGame.XfsSence.AddComponent<XfsTest>();                                      ///服务器加载组件 : 通信组件Server




            ///服务器加载组件驱动程序
            //XfsGame.XfsSystemMananger.AddComponent(new XfsTcpClientDbNetSystem());        ///服务器加载组件 : 套接字 外网 传输数据组件
            //XfsGame.XfsSence.AddComponent(new XfsTcpClientNodeNetSystem());        ///服务器加载组件 : 套接字 外网 传输数据组件
            //XfsGame.XfsSence.AddComponent(new XfsClientSystem());            ///服务器加载组件 : 心跳包 组件
            //XfsGame.XfsSystemMananger.AddComponent(new XfsTcpSessionSystem());              ///服务器加载组件 : 心跳包 组件
            //XfsGame.XfsSence.AddComponent(new XfsTestSystem());                    ///服务器加载组件 : 套接字 外网 传输数据组件


        }



    }
}
