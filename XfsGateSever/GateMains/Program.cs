using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " ... ");
            // 异步方法全部会回掉到主线程
            //SynchronizationContext.SetSynchronizationContext(OneThreadSynchronizationContext.Instance);

            try
            {
                Console.WriteLine(XfsTimerTool.CurrentTime() + " ... ");
                Thread.Sleep(1);
			
                //XfsGame.EventSystem.Add(DLLType.Xfs, typeof(XfsGame).Assembly);
				//XfsGame.EventSystem.Add(DLLType.Xfs, XfsDllHelper.GetXfsAssembly());
				//XfsGame.EventSystem.Add(DLLType.XfsGateSever, XfsDllHelper.GetXfsGateSeverAssembly());


                ///服务器加载组件
                XfsGame.XfsSence.Type = XfsSenceType.Gate;
                XfsGame.XfsSence.AddComponent(new XfsGateHandler());                                     ///服务器加载组件 : 通信组件Server
                XfsGame.XfsSence.AddComponent(new XfsTcpServerGateNet("127.0.0.1", 2001, 10));           ///服务器加载组件 : 通信组件Server

                                                                                                         ///服务器加载组件驱动程序
                XfsGame.XfsSence.AddComponent(new XfsClientSystem());                ///服务器加载组件 : 心跳包 组件
                XfsGame.XfsSence.AddComponent(new XfsPeerSystem());                  ///服务器加载组件 : 心跳包 组件
                XfsGame.XfsSence.AddComponent(new XfsTcpServerGateNetSystem());      ///服务器加载组件 : 套接字 外网 传输数据组件


                Thread.CurrentThread.Name = "TumoWorld";
                Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadName: " + Thread.CurrentThread.Name);
                Console.WriteLine(XfsTimerTool.CurrentTime() + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);

                ///停在这里
                Console.ReadLine();

                #region
                //Options options = XfsGame.XfsSence.AddComponent<OptionComponent, string[]>(args).Options;
                //StartConfig startConfig = XfsGame.XfsSence.AddComponent<StartConfigComponent, string, int>(options.Config, options.AppId).StartConfig;

                //if (!options.AppType.Is(startConfig.AppType))
                //{
                //	Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + "命令行参数apptype与配置不一致");
                //	return;
                //}

                //IdGenerater.AppId = options.AppId;

                //LogManager.Configuration.Variables["appType"] = $"{startConfig.AppType}";
                //LogManager.Configuration.Variables["appId"] = $"{startConfig.AppId}";
                //LogManager.Configuration.Variables["appTypeFormat"] = $"{startConfig.AppType,-8}";
                //LogManager.Configuration.Variables["appIdFormat"] = $"{startConfig.AppId:0000}";

                //Log.Info($"server start........................ {startConfig.AppId} {startConfig.AppType}");

                //Game.Scene.AddComponent<TimerComponent>();
                //Game.Scene.AddComponent<OpcodeTypeComponent>();
                //Game.Scene.AddComponent<MessageDispatcherComponent>();

                //// 根据不同的AppType添加不同的组件
                //OuterConfig outerConfig = startConfig.GetComponent<OuterConfig>();
                //InnerConfig innerConfig = startConfig.GetComponent<InnerConfig>();
                //ClientConfig clientConfig = startConfig.GetComponent<ClientConfig>();



                //// 发送普通actor消息
                //XfsGame.XfsSence.AddComponent<ActorMessageSenderComponent>();

                //// 发送location actor消息
                //XfsGame.XfsSence.AddComponent<ActorLocationSenderComponent>();

                //XfsGame.XfsSence.AddComponent<DBComponent>();
                //XfsGame.XfsSence.AddComponent<DBProxyComponent>();

                //// location server需要的组件
                //XfsGame.XfsSence.AddComponent<LocationComponent>();

                //// 访问location server的组件
                //XfsGame.XfsSence.AddComponent<LocationProxyComponent>();

                //// 这两个组件是处理actor消息使用的
                //XfsGame.XfsSence.AddComponent<MailboxDispatcherComponent>();
                //XfsGame.XfsSence.AddComponent<ActorMessageDispatcherComponent>();

                //// 内网消息组件
                //XfsGame.XfsSence.AddComponent<NetInnerComponent, string>(innerConfig.Address);

                //// 外网消息组件
                //XfsGame.XfsSence.AddComponent<NetOuterComponent, string>(outerConfig.Address);

                //// manager server组件，用来管理其它进程使用
                //XfsGame.XfsSence.AddComponent<AppManagerComponent>();
                //XfsGame.XfsSence.AddComponent<RealmGateAddressComponent>();
                //XfsGame.XfsSence.AddComponent<GateSessionKeyComponent>();

                //// 配置管理
                //XfsGame.XfsSence.AddComponent<ConfigComponent>();

                //#region ///20190621
                //// 调用方法
                ////Game.Scene.AddComponent<PingComponent, long, long, Action<long>>(5000, 6, OnExit);
                //// 调用匿名方法
                ////XfsGame.XfsSence.AddComponent<BongComponent, long, long, Action<long>>(5000, 10, sessionId => {
                ////	XfsGame.XfsSence.GetComponent<NetOuterComponent>().Remove(sessionId);
                ////	XfsGame.XfsSence.GetComponent<NetInnerComponent>().Remove(sessionId);
                ////		});

                //XfsGame.XfsSence.AddComponent<NumericWatcherComponent>();     //创建数值组件NumericWatcherComponent
                //XfsGame.XfsSence.AddComponent<AoiGridComponent>();            //创建 AOI 组件
                //XfsGame.XfsSence.AddComponent<InventoryComponent>();          //创建 Inventory 组件
                //XfsGame.XfsSence.AddComponent<SkillComponent>();              //创建 Skill 组件


                //#endregion


                //// recast寻路组件
                //XfsGame.XfsSence.AddComponent<PathfindingComponent>();
                //XfsGame.XfsSence.AddComponent<PlayerComponent>();
                //XfsGame.XfsSence.AddComponent<UnitComponent>();

                //XfsGame.XfsSence.AddComponent<ConsoleComponent>();
                //		// Game.Scene.AddComponent<HttpComponent>();

                //		Console.WriteLine(" 内网： " + innerConfig.Address + " 外网： " + outerConfig.Address);


                //#region ///20190613
                //XfsGame.XfsSence.AddComponent<MonsterUnitComponent>();
                //XfsGame.XfsSence.AddComponent<MonsterComponent>();
                //XfsGame.XfsSence.AddComponent<UserComponent>();

                //XfsGame.XfsSence.AddComponent<TestComponent>();
                //XfsGame.XfsSence.AddComponent<SpawnComponent>();
                #endregion


                Console.WriteLine(XfsTimerTool.CurrentTime() + " : 服务器配置完成： " /*+ AppType.AllServer + "  " */);

                while (true)
                {
                    try
                    {
                        Thread.Sleep(1);
                        //OneThreadSynchronizationContext.Instance.Update();
                        XfsGame.EventSystem.Update();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
                    }
                }
            }
			catch (Exception e)
			{
				Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
			}
            
        }
    }
}