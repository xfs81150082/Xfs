using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Xfs
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
                XfsGame.EventSystem.Add(XfsDllHelper.GetAssembly("XfsHotfix"));

                ///服务器加载组件
                XfsGame.Scene.AddComponent<XfsStartConfigComponent>();                               ///服务器加载组件 : 信息组件
                XfsGame.Scene.AddComponent<XfsOpcodeTypeComponent>();                                ///服务器加载组件 : 操作号码
                 
                //XfsGame.Scene.AddComponent<XfsTcpClientNodeNet>();                                 ///服务器加载组件 : 通信组件Server
                XfsGame.Scene.AddComponent<XfsNetOuterComponent>().Init("127.0.0.1", 2001, false);                            ///服务器加载组件 : 通信组件Server
                XfsGame.Scene.AddComponent<XfsNetInnerComponent>();                                  ///服务器加载组件 : 通信组件Server


                XfsGame.Scene.AddComponent<XfsTest>();                                      ///服务器加载组件 : 通信组件Server


                Console.WriteLine(XfsTimeHelper.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " 客户端配置完成： XfsClientInit");

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
