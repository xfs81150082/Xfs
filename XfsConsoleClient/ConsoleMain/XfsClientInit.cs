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
                XfsGame.EventSystem.Add(XfsDLLType.Xfs, typeof(XfsGame).Assembly);
                XfsGame.EventSystem.Add(XfsDLLType.XfsGateServer, XfsDllHelper.GetXfsConsoleClientAssembly());

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


    }
}
