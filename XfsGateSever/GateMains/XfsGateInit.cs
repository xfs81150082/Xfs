using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    public class XfsGateInit
    {
        //程序启动入口
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
                XfsDLLType dllType2 = XfsDLLType.XfsGateSever;
                Assembly assembly1 = XfsDllHelper.GetAssembly(dllType1.ToString());
                Assembly assembly2 = XfsDllHelper.GetAssembly(dllType2.ToString());
                XfsGame.EventSystem.Add(dllType1, assembly1);
                XfsGame.EventSystem.Add(dllType2, assembly2);


                ///服务器加载组件
                XfsGame.XfsSence.Type = XfsSenceType.Gate;
                XfsGame.XfsSence.AddComponent<XfsGateHandler>();                                         ///服务器加载组件 : 通信组件Server
                XfsGame.XfsSence.AddComponent<XfsTcpServerGateNet>();                                    ///服务器加载组件 : 通信组件Server



                //XfsGame.XfsSence.GetComponent<XfsTcpServerGateNet>().Init("127.0.0.1", 2001, 10);        ///服务器加载组件 : 通信组件Server
                ///服务器加载组件驱动程序
                //XfsGame.XfsSence.AddComponent(new XfsClientSystem());                ///服务器加载组件 : 心跳包 组件
                //XfsGame.XfsSence.AddComponent(new XfsPeerSystem());                  ///服务器加载组件 : 心跳包 组件
                //XfsGame.XfsSence.AddComponent(new XfsTcpServerGateNetSystem());      ///服务器加载组件 : 套接字 外网 传输数据组件


                XfsGame.XfsSence.AddComponent<TestEntity1>();                                     ///服务器加载组件 : 通信组件Server

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " : 服务器配置完成： " /*+ AppType.AllServer + "  " */);

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



    }
}
