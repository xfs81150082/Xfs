using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Xfs
{
	public enum XfsDLLType
	{
		Xfs,
		XfsConsoleClient,
		XfsGateServer,
		XfsScene,
		XfsDbServer,
		XfsChatServer,		
		Editor,
	}

	public sealed class XfsEventSystem
	{
		private readonly Dictionary<long, XfsComponent> allComponents = new Dictionary<long, XfsComponent>();

		private readonly Dictionary<XfsDLLType, Assembly> assemblies = new Dictionary<XfsDLLType, Assembly>();
		private readonly XfsUnOrderMultiMap<Type, Type> types = new XfsUnOrderMultiMap<Type, Type>();

		private readonly Dictionary<string, List<IXfsEvent>> allEvents = new Dictionary<string, List<IXfsEvent>>();
	
		private readonly XfsUnOrderMultiMap<Type, IXfsAwakeSystem> awakeSystems = new XfsUnOrderMultiMap<Type, IXfsAwakeSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsStartSystem> startSystems = new XfsUnOrderMultiMap<Type, IXfsStartSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsUpdateSystem> updateSystems = new XfsUnOrderMultiMap<Type, IXfsUpdateSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsChangeSystem> changeSystems = new XfsUnOrderMultiMap<Type, IXfsChangeSystem>();
	
		private readonly XfsUnOrderMultiMap<Type, IXfsDestroySystem> destroySystems = new XfsUnOrderMultiMap<Type, IXfsDestroySystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsLoadSystem> loadSystems = new XfsUnOrderMultiMap<Type, IXfsLoadSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsLateUpdateSystem> lateUpdateSystems = new XfsUnOrderMultiMap<Type, IXfsLateUpdateSystem>();

		private readonly XfsUnOrderMultiMap<Type, IXfsDeserializeSystem> deserializeSystems = new XfsUnOrderMultiMap<Type, IXfsDeserializeSystem>();
	
		private readonly Queue<long> starts = new Queue<long>();
	
		private Queue<long> updates = new Queue<long>();
		private Queue<long> updates2 = new Queue<long>();

		private Queue<long> lateUpdates = new Queue<long>();
		private Queue<long> lateUpdates2 = new Queue<long>();

		private Queue<long> loaders = new Queue<long>();
		private Queue<long> loaders2 = new Queue<long>();
	
		public void Add(XfsDLLType dllType, Assembly assembly)
		{
			this.assemblies[dllType] = assembly;
			this.types.Clear();
			foreach (Assembly value in this.assemblies.Values)
			{
				foreach (Type type in value.GetTypes())
				{
					object[] objects = type.GetCustomAttributes(typeof(XfsBaseAttribute), true);
					if (objects.Length == 0)
					{
						continue;
					}
					XfsBaseAttribute baseAttribute = (XfsBaseAttribute)objects[0];
					this.types.Add(baseAttribute.AttributeType, type);
				}					
			}
				
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " types: " + this.types.Count);	
			for(int i = 0; i < this.types.Count; i++)
            {
				Console.WriteLine(XfsTimeHelper.CurrentTime() + " Keys:{0} + {1} ",i, this.types.Keys()[i]);
				for(int k=0;k< this.types.Values()[i].Count; k++)
                {
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " Values: {0} + {1} ", k, this.types.Values()[i][k]);
				}
			}

			this.awakeSystems.Clear();
			this.lateUpdateSystems.Clear();
			this.updateSystems.Clear();
			this.startSystems.Clear();
			this.loadSystems.Clear();
			this.changeSystems.Clear();
			this.destroySystems.Clear();
			this.deserializeSystems.Clear();

			if (types.Count == 0) return;

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
					case IXfsAwakeSystem awakeSystem:
						this.awakeSystems.Add(awakeSystem.Type(), awakeSystem);
						break;
					case IXfsStartSystem startSystem:
						this.startSystems.Add(startSystem.Type(), startSystem);
						break;
					case IXfsUpdateSystem updateSystem:
						this.updateSystems.Add(updateSystem.Type(), updateSystem);
						break;
					case IXfsLateUpdateSystem lateUpdateSystem:
						this.lateUpdateSystems.Add(lateUpdateSystem.Type(), lateUpdateSystem);
						break;
					case IXfsChangeSystem changeSystem:
						this.changeSystems.Add(changeSystem.Type(), changeSystem);
						break;
					case IXfsLoadSystem loadSystem:
						this.loadSystems.Add(loadSystem.Type(), loadSystem);
						break;
					case IXfsDestroySystem destroySystem:
						this.destroySystems.Add(destroySystem.Type(), destroySystem);
						break;
					case IXfsDeserializeSystem deserializeSystem:
						this.deserializeSystems.Add(deserializeSystem.Type(), deserializeSystem);
						break;
				}
			}

			///20201122 看看系统运行中的具体内容
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " this[XfsObjectSystem]-Count: " + types[typeof(XfsObjectSystemAttribute)].Count);
            for (int i = 0; i < this.types[typeof(XfsObjectSystemAttribute)].Count; i++)
            {
                Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsObjectSystem:{0} + {1} ", i, this.types[typeof(XfsObjectSystemAttribute)][i]);
            }
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " awakeSystems: " + this.awakeSystems.Count);
            foreach (var tem in  this.awakeSystems.Keys())
			{
				Console.WriteLine(XfsTimeHelper.CurrentTime() + " awakeSystems:{0} + {1} ", tem, this.awakeSystems[tem].Count);
			}
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " updateSystems: " + this.updateSystems.Count);
			foreach (var tem in this.updateSystems.Keys())
			{
				Console.WriteLine(XfsTimeHelper.CurrentTime() + " updateSystems:{0} + {1} ", tem, this.updateSystems[tem].Count);
			}
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " lateUpdateSystems: " + this.lateUpdateSystems.Count);
			foreach (var tem in this.lateUpdateSystems.Keys())
			{
				Console.WriteLine(XfsTimeHelper.CurrentTime() + " lateUpdateSystems:{0} + {1} ", tem, this.lateUpdateSystems[tem].Count);
			}

			this.allEvents.Clear();
			foreach (Type type in types[typeof(XfsEventAttribute)])
			{
				object[] attrs = type.GetCustomAttributes(typeof(XfsEventAttribute), false);

				foreach (object attr in attrs)
				{
					XfsEventAttribute aEventAttribute = (XfsEventAttribute)attr;
					object obj = Activator.CreateInstance(type);
					IXfsEvent iEvent = obj as IXfsEvent;
					if (iEvent == null)
					{
						Console.WriteLine($"{obj.GetType().Name} 没有继承IEvent");
					}
					this.RegisterEvent(aEventAttribute.Type, iEvent);
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " allEvents: " + this.allEvents.Count);
				}
			}
			this.Load();
		}
	
		public void RegisterEvent(string eventId, IXfsEvent e)
		{
			if (!this.allEvents.ContainsKey(eventId))
			{
				this.allEvents.Add(eventId, new List<IXfsEvent>());
			}
			this.allEvents[eventId].Add(e);
		}
	
		public Assembly Get(XfsDLLType dllType)
		{
			return this.assemblies[dllType];
		}
	
		public List<Type> GetTypes(Type systemAttributeType)
		{
			if (!this.types.ContainsKey(systemAttributeType))
			{
				return new List<Type>();
			}
			return this.types[systemAttributeType];
		}
	
		public void Add(XfsComponent component)
		{
			this.allComponents.Add(component.InstanceId, component);

			Type type = component.GetType();

			if (this.startSystems.ContainsKey(type))
			{
				this.starts.Enqueue(component.InstanceId);
				Console.WriteLine(XfsTimeHelper.CurrentTime() + " starts: " + this.starts.Count);
			}

			if (this.updateSystems.ContainsKey(type))
			{
				this.updates.Enqueue(component.InstanceId);
				Console.WriteLine(XfsTimeHelper.CurrentTime() + " updates: " + this.updates.Count);
			}

			if (this.lateUpdateSystems.ContainsKey(type))
			{
				this.lateUpdates.Enqueue(component.InstanceId);
			}
		
			if (this.loadSystems.ContainsKey(type))
			{
				this.loaders.Enqueue(component.InstanceId);
			}

		}
	
		public void Remove(long instanceId)
		{
			this.allComponents.Remove(instanceId);
		}		
	
		public XfsComponent Get(long instanceId)
		{
			XfsComponent component = null;
			this.allComponents.TryGetValue(instanceId, out component);
			return component;
		}
	
		private void Start()
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
						Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
					}
				}
			}
		}
	
		public void Update()
		{
			this.Start();
            //Console.WriteLine(XfsTimeHelper.CurrentTime() + " : Update1.... " + this.updates.Count);

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
						//Console.WriteLine(XfsTimerTool.CurrentTime() + " : Update2.... " + this.updates.Count);
					}
					catch (Exception e)
					{
						Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
					}
				}
			}

			XfsObjectHelper.Swap(ref this.updates, ref this.updates2);
		}
	
		public void Deserialize(XfsComponent component)
		{
			List<IXfsDeserializeSystem> iDeserializeSystems = this.deserializeSystems[component.GetType()];
			if (iDeserializeSystems == null)
			{
				return;
			}

			foreach (IXfsDeserializeSystem deserializeSystem in iDeserializeSystems)
			{
				if (deserializeSystem == null)
				{
					continue;
				}

				try
				{
					deserializeSystem.Run(component);
				}
				catch (Exception e)
				{
					//Log.Error(e);
				}
			}
		}
	
		public void Awake(XfsComponent component)
		{
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsEventSystem-Awake1: " + component.GetType());

            List<IXfsAwakeSystem> iAwakeSystems = this.awakeSystems[component.GetType()];

            if (iAwakeSystems == null)
			{			
				Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsEventSystem-Awake2: iAwakeSystems == null");
				return;
			}

			foreach (IXfsAwakeSystem aAwakeSystem in iAwakeSystems)
			{
				if (aAwakeSystem == null)
				{
					continue;
				}
				IXfsAwake iAwake = aAwakeSystem as IXfsAwake;
				if (iAwake == null)
				{
					continue;
				}

				try
				{
					iAwake.Run(component);
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsEventSystem-Awake5: " + component.GetType());
				}
				catch (Exception e)
				{
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
				}
			}
		}
	
		public void Awake<P1>(XfsComponent component, P1 p1)
		{
			List<IXfsAwakeSystem> iAwakeSystems = this.awakeSystems[component.GetType()];
			if (iAwakeSystems == null)
			{
				return;
			}

			foreach (IXfsAwakeSystem aAwakeSystem in iAwakeSystems)
			{
				if (aAwakeSystem == null)
				{
					continue;
				}

				IXfsAwake<P1> iAwake = aAwakeSystem as IXfsAwake<P1>;
				if (iAwake == null)
				{
					continue;
				}

				try
				{
					iAwake.Run(component, p1);
				}
				catch (Exception e)
				{
					//Log.Error(e);
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
				}
			}
		}
	
		public void Awake<P1, P2>(XfsComponent component, P1 p1, P2 p2)
		{
			List<IXfsAwakeSystem> iAwakeSystems = this.awakeSystems[component.GetType()];
			if (iAwakeSystems == null)
			{
				return;
			}

			foreach (IXfsAwakeSystem aAwakeSystem in iAwakeSystems)
			{
				if (aAwakeSystem == null)
				{
					continue;
				}

				IXfsAwake<P1, P2> iAwake = aAwakeSystem as IXfsAwake<P1, P2>;
				if (iAwake == null)
				{
					continue;
				}

				try
				{
					iAwake.Run(component, p1, p2);
				}
				catch (Exception e)
				{
					//Log.Error(e);
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
				}
			}
		}
	
		public void Awake<P1, P2, P3>(XfsComponent component, P1 p1, P2 p2, P3 p3)
		{
			List<IXfsAwakeSystem> iAwakeSystems = this.awakeSystems[component.GetType()];
			if (iAwakeSystems == null)
			{
				return;
			}

			foreach (IXfsAwakeSystem aAwakeSystem in iAwakeSystems)
			{
				if (aAwakeSystem == null)
				{
					continue;
				}

				IXfsAwake<P1, P2, P3> iAwake = aAwakeSystem as IXfsAwake<P1, P2, P3>;
				if (iAwake == null)
				{
					continue;
				}

				try
				{
					iAwake.Run(component, p1, p2, p3);
				}
				catch (Exception e)
				{
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
				}
			}
		}
	
		public void Destroy(XfsComponent component)
		{
			List<IXfsDestroySystem> iDestroySystems = this.destroySystems[component.GetType()];
			if (iDestroySystems == null)
			{
				return;
			}

			foreach (IXfsDestroySystem iDestroySystem in iDestroySystems)
			{
				if (iDestroySystem == null)
				{
					continue;
				}

				try
				{
					iDestroySystem.Run(component);
				}
				catch (Exception e)
				{
					//Log.Error(e);
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
				}
			}
		}
	
		public void Load()
		{
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsEventSystem-Load:{0} + {1} ", 503, this.loaders.Count);

			while (this.loaders.Count > 0)
			{
				long instanceId = this.loaders.Dequeue();
				XfsComponent component;
				if (!this.allComponents.TryGetValue(instanceId, out component))
				{
					continue;
				}
				if (component.IsDisposed)
				{
					continue;
				}

				List<IXfsLoadSystem> iLoadSystems = this.loadSystems[component.GetType()];
				if (iLoadSystems == null)
				{
					continue;
				}

				this.loaders2.Enqueue(instanceId);

				foreach (IXfsLoadSystem iLoadSystem in iLoadSystems)
				{
					try
					{
						iLoadSystem.Run(component);
					}
					catch (Exception e)
					{
						Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
					}
				}
			}

			XfsObjectHelper.Swap(ref this.loaders, ref this.loaders2);
		}
	
		public void Change(XfsComponent component)
		{
			List<IXfsChangeSystem> iChangeSystems = this.changeSystems[component.GetType()];
			if (iChangeSystems == null)
			{
				return;
			}

			foreach (IXfsChangeSystem iChangeSystem in iChangeSystems)
			{
				if (iChangeSystem == null)
				{
					continue;
				}

				try
				{
					iChangeSystem.Run(component);
				}
				catch (Exception e)
				{
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + e);
				}
			}
		}
		
		public void LateUpdate()
		{
			while (this.lateUpdates.Count > 0)
			{
				long instanceId = this.lateUpdates.Dequeue();
				XfsComponent component;
				if (!this.allComponents.TryGetValue(instanceId, out component))
				{
					continue;
				}
				if (component.IsDisposed)
				{
					continue;
				}

				List<IXfsLateUpdateSystem> iLateUpdateSystems = this.lateUpdateSystems[component.GetType()];
				if (iLateUpdateSystems == null)
				{
					continue;
				}

				this.lateUpdates2.Enqueue(instanceId);

				foreach (IXfsLateUpdateSystem iLateUpdateSystem in iLateUpdateSystems)
				{
					try
					{
						iLateUpdateSystem.Run(component);
					}
					catch (Exception e)
					{
						//Log.Error(e);
					}
				}
			}

			XfsObjectHelper.Swap(ref this.lateUpdates, ref this.lateUpdates2);
		}
		
		public void Run(string type)
		{
			List<IXfsEvent> iEvents;
			if (!this.allEvents.TryGetValue(type, out iEvents))
			{
				return;
			}
			foreach (IXfsEvent iEvent in iEvents)
			{
				try
				{
					iEvent?.Handle();
				}
				catch (Exception e)
				{
					//Log.Error(e);
				}
			}
		}
	
		public void Run<A>(string type, A a)
		{
			List<IXfsEvent> iEvents;
			if (!this.allEvents.TryGetValue(type, out iEvents))
			{
				return;
			}
			foreach (IXfsEvent iEvent in iEvents)
			{
				try
				{
					iEvent?.Handle(a);
				}
				catch (Exception e)
				{
					//Log.Error(e);
				}
			}
		}
	
		public void Run<A, B>(string type, A a, B b)
		{
			List<IXfsEvent> iEvents;
			if (!this.allEvents.TryGetValue(type, out iEvents))
			{
				return;
			}
			foreach (IXfsEvent iEvent in iEvents)
			{
				try
				{
					iEvent?.Handle(a, b);
				}
				catch (Exception e)
				{
					//Log.Error(e);
				}
			}
		}
	
		public void Run<A, B, C>(string type, A a, B b, C c)
		{
			List<IXfsEvent> iEvents;
			if (!this.allEvents.TryGetValue(type, out iEvents))
			{
				return;
			}
			foreach (IXfsEvent iEvent in iEvents)
			{
				try
				{
					iEvent?.Handle(a, b, c);
				}
				catch (Exception e)
				{
					//Log.Error(e);
				}
			}
		}


	}

}