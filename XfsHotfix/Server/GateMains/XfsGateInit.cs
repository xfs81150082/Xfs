using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace Xfs
{
    public class XfsGateInit : XfsEntity
    {       
        ///程序启动入口
        public void Start()
        {      
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " ... ");              
            Thread.Sleep(1);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);

            // 异步方法全部会回掉到主线程
            SynchronizationContext.SetSynchronizationContext(XfsOneThreadSynchronizationContext.Instance);

            try
            {
                //XfsGame.EventSystem.Add(typeof(XfsGame).Assembly);
                XfsGame.EventSystem.Add(XfsDllHelper.GetAssembly("XfsHotfix"));

                ///服务器加载组件
                //XfsGame.Scene.AddComponent<ConfigComponent>();
                //XfsGame.Scene.AddComponent<XfsStartConfigComponent>();                         ///服务器加载组件 : 信息组件
                // 根据不同的AppType添加不同的组件
                //XfsGame.Scene.AddComponent<XfsTimerComponent>();


                XfsGame.Scene.AddComponent<XfsOpcodeTypeComponent>();                           ///服务器加载组件 : 操作号码
                XfsGame.Scene.AddComponent<XfsMessageDispatcherComponent>();                    ///服务器加载组件 : 外网Handler组件


                /// 发送普通actor消息
                XfsGame.Scene.AddComponent<XfsActorMessageSenderComponent>();
                /// 这两个组件是处理actor消息使用的
                XfsGame.Scene.AddComponent<XfsActorMessageDispatcherComponent>();


                /// 发送location actor消息
                //XfsGame.Scene.AddComponent<XfsCoroutineLockComponent>();
                //XfsGame.Scene.AddComponent<ActorLocationSenderComponent>();
                /// location server需要的组件
                //XfsGame.Scene.AddComponent<LocationComponent>();
                //// 访问location server的组件
                //XfsGame.Scene.AddComponent<LocationProxyComponent>();
                /// 这两个组件是处理actor消息使用的
                //XfsGame.Scene.AddComponent<XfsMailboxDispatcherComponent>();
                //XfsGame.Scene.AddComponent<XfsActorMessageDispatcherComponent>();
                //// 内网消息组件
                //XfsGame.Scene.AddComponent<NetInnerComponent, string>(innerConfig.Address);
                //// 外网消息组件
                //XfsGame.Scene.AddComponent<NetOuterComponent, string>(outerConfig.Address);


                XfsGame.Scene.AddComponent<XfsTcpServerGateNet>();                             ///服务器加载组件 : 通信组件Server


                ///manager server组件，用来管理其它进程使用
                //XfsGame.Scene.AddComponent<AppManagerComponent>();
                //XfsGame.Scene.AddComponent<GateSessionKeyComponent>();
                ///配置管理
                //XfsGame.Scene.AddComponent<XfsConfigComponent>();


                XfsGame.Scene.AddComponent<GateTests>();                                      ///服务器加载组件 : 通信组件Server


                //XfsGame.EventSystem.Publish(new XfsAppStart());

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " 服务器配置完成： XfsGateInit");

                while (true)
                {
                    try
                    {
                        Thread.Sleep(1);
                        XfsOneThreadSynchronizationContext.Instance.Update();
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
