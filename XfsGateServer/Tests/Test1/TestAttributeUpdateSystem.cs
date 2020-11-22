using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateServer
{
    class TestAttributeUpdateSystem : XfsUpdateSystem<TestAttribute>
    {
        private readonly Dictionary<long, XfsComponent> allComponents = new Dictionary<long, XfsComponent>();

        private readonly Dictionary<XfsDLLType, Assembly> assemblies = new Dictionary<XfsDLLType, Assembly>();
        private readonly XfsUnOrderMultiMap<Type, Type> types = new XfsUnOrderMultiMap<Type, Type>();

        private readonly Dictionary<string, List<IXfsEvent>> allEvents = new Dictionary<string, List<IXfsEvent>>();

        private readonly XfsUnOrderMultiMap<Type, IXfsAwakeSystem> awakeSystems = new XfsUnOrderMultiMap<Type, IXfsAwakeSystem>();

        private readonly XfsUnOrderMultiMap<Type, IXfsStartSystem> startSystems = new XfsUnOrderMultiMap<Type, IXfsStartSystem>();

        private readonly XfsUnOrderMultiMap<Type, IXfsDestroySystem> destroySystems = new XfsUnOrderMultiMap<Type, IXfsDestroySystem>();

        private readonly XfsUnOrderMultiMap<Type, IXfsLoadSystem> loadSystems = new XfsUnOrderMultiMap<Type, IXfsLoadSystem>();

        private readonly XfsUnOrderMultiMap<Type, IXfsUpdateSystem> updateSystems = new XfsUnOrderMultiMap<Type, IXfsUpdateSystem>();

        private readonly XfsUnOrderMultiMap<Type, IXfsLateUpdateSystem> lateUpdateSystems = new XfsUnOrderMultiMap<Type, IXfsLateUpdateSystem>();

        private readonly XfsUnOrderMultiMap<Type, IXfsChangeSystem> changeSystems = new XfsUnOrderMultiMap<Type, IXfsChangeSystem>();

        private readonly XfsUnOrderMultiMap<Type, IXfsDeserializeSystem> deserializeSystems = new XfsUnOrderMultiMap<Type, IXfsDeserializeSystem>();

        private Queue<long> updates = new Queue<long>();
        private Queue<long> updates2 = new Queue<long>();

        private readonly Queue<long> starts = new Queue<long>();

        private Queue<long> loaders = new Queue<long>();
        private Queue<long> loaders2 = new Queue<long>();

        private Queue<long> lateUpdates = new Queue<long>();
        private Queue<long> lateUpdates2 = new Queue<long>();




        public override void Update(TestAttribute self)
        {


        }

      

        void TestAttribute3(TestAttribute self)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " TestSimyy1 ... ");

            XfsDLLType dllType1 = XfsDLLType.Xfs;
            XfsDLLType dllType2 = XfsDLLType.XfsGateServer;

            Assembly assembly1 = XfsDllHelper.GetAssembly(dllType1.ToString());
            Assembly assembly2 = XfsDllHelper.GetAssembly(dllType2.ToString());

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + assembly1);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + assembly2);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + assembly1.GetTypes().Length);
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + assembly2.GetTypes().Length);


            XfsGame.EventSystem.Add(XfsDLLType.Xfs, XfsDllHelper.GetAssembly(XfsDLLType.Xfs.ToString()));
            XfsGame.EventSystem.Add(XfsDLLType.XfsGateServer, XfsDllHelper.GetXfsGateServerAssembly());

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " types.count: " + this.types.Count);

        }

        ///测试 用反射 找程序集 找类 找接口 找特性 并运行类或接口的方法
        void TestSimyy1(TestAttribute self)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " TestSimyy1 ... ");
            try
            {


                XfsDLLType dllType1 = XfsDLLType.Xfs;
                XfsDLLType dllType2 = XfsDLLType.XfsGateServer;

                Assembly assembly1 = XfsDllHelper.GetAssembly(dllType1.ToString());
                Assembly assembly2 = XfsDllHelper.GetAssembly(dllType2.ToString());

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + assembly1);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + assembly2);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + assembly1.GetTypes().Length);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + assembly2.GetTypes().Length);

                this.assemblies[dllType1] = assembly1;
                this.assemblies[dllType2] = assembly2;

                Console.WriteLine(XfsTimeHelper.CurrentTime() + " assemblies.count: " + this.assemblies.Count);


                this.types.Clear();
                foreach (Assembly value in this.assemblies.Values)
                {
                    foreach (Type type in value.GetTypes())
                    {
                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " Type: " + type.Name);

                        object[] objects = type.GetCustomAttributes(typeof(XfsBaseAttribute), true);
                        //object[] object1s = type.GetCustomAttributes(typeof(XfsObjectSystemAttribute), false);

                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " Type: " + type.Name + " GetCustomAttributes: " + objects.Length);

                        if (objects.Length == 0)
                        {
                            continue;
                        }

                        XfsBaseAttribute baseAttribute = (XfsBaseAttribute)objects[0];
                        this.types.Add(baseAttribute.AttributeType, type);
                    }
                }
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " types.count: " + this.types.Count);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " Keys[0]: " + this.types.Keys()[0]);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " Keys[0]: " + this.types[typeof(XfsObjectSystemAttribute)][1]);
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " this[t]: " + typeof(XfsObjectSystemAttribute));

                this.awakeSystems.Clear();
                this.lateUpdateSystems.Clear();
                this.updateSystems.Clear();
                this.startSystems.Clear();
                this.loadSystems.Clear();
                this.changeSystems.Clear();
                this.destroySystems.Clear();
                this.deserializeSystems.Clear();

                foreach (Type type in types[typeof(XfsObjectSystemAttribute)])
                {
                    object[] attrs = type.GetCustomAttributes(typeof(XfsObjectSystemAttribute), false);

                    if (attrs.Length == 0)
                    {
                        continue;
                    }

                    object obj = Activator.CreateInstance(type);

                    switch (obj)
                    {
                        case IXfsAwakeSystem objectSystem:
                            this.awakeSystems.Add(objectSystem.Type(), objectSystem);
                            Console.WriteLine(XfsTimeHelper.CurrentTime() + " IXfsAwakeSystems: " + this.awakeSystems.Count);
                            break;
                        case IXfsUpdateSystem updateSystem:
                            this.updateSystems.Add(updateSystem.Type(), updateSystem);
                            Console.WriteLine(XfsTimeHelper.CurrentTime() + " IXfsUpdateSystem: " + this.updateSystems.Count);
                            break;
                        case IXfsLateUpdateSystem lateUpdateSystem:
                            this.lateUpdateSystems.Add(lateUpdateSystem.Type(), lateUpdateSystem);
                            break;
                        case IXfsStartSystem startSystem:
                            this.startSystems.Add(startSystem.Type(), startSystem);
                            Console.WriteLine(XfsTimeHelper.CurrentTime() + " IXfsStartSystem: " + this.startSystems.Count);
                            break;
                        case IXfsDestroySystem destroySystem:
                            this.destroySystems.Add(destroySystem.Type(), destroySystem);
                            break;
                        case IXfsLoadSystem loadSystem:
                            this.loadSystems.Add(loadSystem.Type(), loadSystem);
                            break;
                        case IXfsChangeSystem changeSystem:
                            this.changeSystems.Add(changeSystem.Type(), changeSystem);
                            break;
                        case IXfsDeserializeSystem deserializeSystem:
                            this.deserializeSystems.Add(deserializeSystem.Type(), deserializeSystem);
                            break;
                    }
                }

                while (true)
                {
                    try
                    {
                        Thread.Sleep(1);

                        this.TestUpdate(self);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " Update发生异常: " + e);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " 异常: " + e);
            }
        }
        private void Start(TestAttribute self)
        {
            while (this.starts.Count > 0)
            {
                long instanceId = this.starts.Dequeue();
                XfsComponent component;
                if (!this.allComponents.TryGetValue(instanceId, out component))
                {
                    continue;
                }

                List<IXfsStartSystem> iStartSystems = this.startSystems[component.GetType()];
                if (iStartSystems == null)
                {
                    continue;
                }

                foreach (IXfsStartSystem iStartSystem in iStartSystems)
                {
                    try
                    {
                        iStartSystem.Run(component);
                    }
                    catch (Exception e)
                    {
                        //Log.Error(e);
                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
                    }
                }
            }
        }
        public void TestUpdate(TestAttribute self)
        {
            this.Start(self);

            while (this.updates.Count > 0)
            {
                long instanceId = this.updates.Dequeue();
                XfsComponent component;
                if (!this.allComponents.TryGetValue(instanceId, out component))
                {
                    continue;
                }
                if (component.IsDisposed)
                {
                    continue;
                }

                List<IXfsUpdateSystem> iUpdateSystems = this.updateSystems[component.GetType()];
                if (iUpdateSystems == null)
                {
                    continue;
                }

                this.updates2.Enqueue(instanceId);

                foreach (IXfsUpdateSystem iUpdateSystem in iUpdateSystems)
                {
                    try
                    {
                        iUpdateSystem.Run(component);
                    }
                    catch (Exception e)
                    {
                        //Log.Error(e);
                        Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
                    }
                }
            }

            XfsObjectHelper.Swap(ref this.updates, ref this.updates2);
        }


        void TestSimyy2(TestAttribute self)
        {
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " TestSimyy2 ... ");

        }




    }
}
