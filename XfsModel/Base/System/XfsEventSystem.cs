using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Xfs
{
	public sealed class XfsEventSystem: IDisposable
	{
		private static XfsEventSystem instance;
		public static XfsEventSystem Instance
		{
			get
			{
				return instance ?? (instance = new XfsEventSystem());
			}
		}
		
		private readonly Dictionary<long, XfsEntity> allComponents = new Dictionary<long, XfsEntity>();

		private readonly Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();
		
		public readonly XfsUnOrderMultiMapSet<Type, Type> types = new XfsUnOrderMultiMapSet<Type, Type>();

		private readonly Dictionary<Type, List<object>> allEvents = new Dictionary<Type, List<object>>();

		private readonly UnOrderMultiMap<Type, IXfsAwakeSystem> awakeSystems = new UnOrderMultiMap<Type, IXfsAwakeSystem>();

		private readonly UnOrderMultiMap<Type, IXfsStartSystem> startSystems = new UnOrderMultiMap<Type, IXfsStartSystem>();

		private readonly UnOrderMultiMap<Type, IXfsDestroySystem> destroySystems = new UnOrderMultiMap<Type, IXfsDestroySystem>();

		private readonly UnOrderMultiMap<Type, IXfsLoadSystem> loadSystems = new UnOrderMultiMap<Type, IXfsLoadSystem>();

		private readonly UnOrderMultiMap<Type, IXfsUpdateSystem> updateSystems = new UnOrderMultiMap<Type, IXfsUpdateSystem>();

		private readonly UnOrderMultiMap<Type, IXfsLateUpdateSystem> lateUpdateSystems = new UnOrderMultiMap<Type, IXfsLateUpdateSystem>();

		private readonly UnOrderMultiMap<Type, IXfsChangeSystem> changeSystems = new UnOrderMultiMap<Type, IXfsChangeSystem>();
		
		private readonly UnOrderMultiMap<Type, IXfsDeserializeSystem> deserializeSystems = new UnOrderMultiMap<Type, IXfsDeserializeSystem>();
		
		private Queue<long> updates = new Queue<long>();
		private Queue<long> updates2 = new Queue<long>();
		
		private readonly Queue<long> starts = new Queue<long>();

		private Queue<long> loaders = new Queue<long>();
		private Queue<long> loaders2 = new Queue<long>();

		private Queue<long> lateUpdates = new Queue<long>();
		private Queue<long> lateUpdates2 = new Queue<long>();

		private XfsEventSystem()
		{
            this.Add(typeof(XfsEventSystem).Assembly);
        }

		public void Add(Assembly assembly)
		{
			this.assemblies[assembly.ManifestModule.ScopeName] = assembly;
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsEventSystem-64: " + assembly.ManifestModule.ScopeName);
			this.types.Clear();
			foreach (Assembly value in this.assemblies.Values)
			{
				foreach (Type type in value.GetTypes())
				{
					if (type.IsAbstract)
					{
						continue;
					}

					object[] objects = type.GetCustomAttributes(typeof(XfsBaseAttribute), true);
					if (objects.Length == 0)
					{
						continue;
					}

					foreach (XfsBaseAttribute baseAttribute in objects)
					{
						this.types.Add(baseAttribute.AttributeType, type);
					}
				}
			}

			Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsEventSystem-87: " + this.types.Count);
			foreach(var tem in this.types.GetDictionary())
            {
				foreach(var type11 in this.types.GetDictionary()[tem.Key])
                {
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsEventSystem-94-Key: " + tem.Key + " : " + type11);
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
			
			foreach (Type type in this.GetTypes(typeof(XfsObjectSystemAttribute)))
			{
				object obj = Activator.CreateInstance(type);
				switch (obj)
				{
					case IXfsAwakeSystem objectSystem:
						this.awakeSystems.Add(objectSystem.Type(), objectSystem);
						break;
					case IXfsUpdateSystem updateSystem:
						this.updateSystems.Add(updateSystem.Type(), updateSystem);
						break;
					case IXfsLateUpdateSystem lateUpdateSystem:
						this.lateUpdateSystems.Add(lateUpdateSystem.Type(), lateUpdateSystem);
						break;
					case IXfsStartSystem startSystem:
						this.startSystems.Add(startSystem.Type(), startSystem);
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

			//foreach (var tem in this.awakeSystems.GetDictionary())
			//{
			//	foreach (var type11 in this.awakeSystems.GetDictionary()[tem.Key])
			//	{
			//		Console.WriteLine(XfsTimeHelper.CurrentTime() + " awakeSystems-142-Key: " + tem.Key + " : " + type11);
			//	}
			//}
			//foreach (var tem in this.updateSystems.GetDictionary())
			//{
			//	foreach (var type11 in this.updateSystems.GetDictionary()[tem.Key])
			//	{
			//		Console.WriteLine(XfsTimeHelper.CurrentTime() + " updateSystems-149-Key: " + tem.Key + " : " + type11);
			//	}
			//}

			this.allEvents.Clear();
			foreach (Type type in types[typeof(XfsEventAttribute)])
			{
				IXfsEvent obj = Activator.CreateInstance(type) as IXfsEvent;
				if (obj == null)
				{
					throw new Exception($"type not is AEvent: {obj.GetType().Name}");
				}

				Type eventType = obj.GetEventType();
				if (!this.allEvents.ContainsKey(eventType))
				{
					this.allEvents.Add(eventType, new List<object>());
				}
				this.allEvents[eventType].Add(obj);
			}
			
			this.Load();
		}
		
		public Assembly GetAssembly(string name)
		{
			return this.assemblies[name];
		}
		
		public HashSet<Type> GetTypes(Type systemAttributeType)
		{
			if (!this.types.ContainsKey(systemAttributeType))
			{
				return new HashSet<Type>();
			}
			return this.types[systemAttributeType];
		}
		
		public List<Type> GetTypes()
		{
			List<Type> allTypes = new List<Type>();
			foreach (Assembly assembly in this.assemblies.Values)
			{
				allTypes.AddRange(assembly.GetTypes());
			}
			return allTypes;
		}

		public void RegisterSystem(XfsEntity component, bool isRegister = true)
		{
			if (!isRegister)
			{
				this.Remove(component.InstanceId);
				return;
			}
			this.allComponents.Add(component.InstanceId, component);
			
			Type type = component.GetType();

			if (this.loadSystems.ContainsKey(type))
			{ 
				this.loaders.Enqueue(component.InstanceId);
			}

			if (this.updateSystems.ContainsKey(type))
			{
				this.updates.Enqueue(component.InstanceId);
			}

			if (this.startSystems.ContainsKey(type))
			{
				this.starts.Enqueue(component.InstanceId);
			}

			if (this.lateUpdateSystems.ContainsKey(type))
			{
				this.lateUpdates.Enqueue(component.InstanceId);
			}
		}

		public void Remove(long instanceId)
		{
			this.allComponents.Remove(instanceId);
		}
		public XfsEntity Get(long instanceId)
		{
			XfsEntity component = null;
			this.allComponents.TryGetValue(instanceId, out component);
			return component;
		}
		
		public bool IsRegister(long instanceId)
		{
			return this.allComponents.ContainsKey(instanceId);
		}
		
		public void Deserialize(XfsEntity component)
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
					Console.WriteLine(e);
				}
			}
		}

		public void Awake(XfsEntity component)
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

				IXfsAwake iAwake = aAwakeSystem as IXfsAwake;
				if (iAwake == null)
				{
					continue;
				}

				try
				{
					iAwake.Run(component);
				}
				catch (Exception e)
				{
					//Log.Error(e);
					Console.WriteLine(e);
				}
			}
		}

		public void Awake<P1>(XfsEntity component, P1 p1)
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
					Console.WriteLine(e);
				}
			}
		}

		public void Awake<P1, P2>(XfsEntity component, P1 p1, P2 p2)
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
					Console.WriteLine(e);
				}
			}
		}

		public void Awake<P1, P2, P3>(XfsEntity component, P1 p1, P2 p2, P3 p3)
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
					//Log.Error(e);
					Console.WriteLine(e);
				}
			}
		}

        public void Awake<P1, P2, P3, P4>(XfsEntity component, P1 p1, P2 p2, P3 p3, P4 p4)
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

				IXfsAwake<P1, P2, P3, P4> iAwake = aAwakeSystem as IXfsAwake<P1, P2, P3, P4>;
                if (iAwake == null)
                {
                    continue;
                }

                try
                {
                    iAwake.Run(component, p1, p2, p3, p4);
                }
                catch (Exception e)
                {
					//Log.Error(e);
					Console.WriteLine(e);
				}
			}
        }

        public void Change(XfsEntity component)
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
					//Log.Error(e);
					Console.WriteLine(e);
				}
			}
		}

		public void Load()
		{
			while (this.loaders.Count > 0)
			{
				long instanceId = this.loaders.Dequeue();
				XfsEntity component;
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
						//Log.Error(e);
						Console.WriteLine(e);
					}
				}
			}

			XfsObjectHelper.Swap(ref this.loaders, ref this.loaders2);
		}

		private void Start()
		{
			while (this.starts.Count > 0)
			{
				long instanceId = this.starts.Dequeue();
				XfsEntity component;
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
						Console.WriteLine(e);
					}
				}
			}
		}

		public void Destroy(XfsEntity component)
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
					Console.WriteLine(e);
				}
			}
		}
		
		public void Update()
		{
			this.Start();
			
			while (this.updates.Count > 0)
			{
				long instanceId = this.updates.Dequeue();
				XfsEntity component;
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
						Console.WriteLine(e);
					}
				}
			}

			XfsObjectHelper.Swap(ref this.updates, ref this.updates2);
		}

		public void LateUpdate()
		{
			while (this.lateUpdates.Count > 0)
			{
				long instanceId = this.lateUpdates.Dequeue();
				XfsEntity component;
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
						Console.WriteLine(e);
					}
				}
			}

			XfsObjectHelper.Swap(ref this.lateUpdates, ref this.lateUpdates2);
		}
		
		public async XfsTask Publish<T>(T a) where T: struct
		{
			List<object> iEvents;
			if (!this.allEvents.TryGetValue(typeof(T), out iEvents))
			{
				return;
			}
			foreach (object obj in iEvents)
			{
				try
				{
					if (!(obj is XfsAEvent<T> aEvent))
					{
						Console.WriteLine($"event error: {obj.GetType().Name}");
						//Log.Error($"event error: {obj.GetType().Name}");
						continue;
					}
					await aEvent.Run(a);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					//Log.Error(e);
				}
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			HashSet<Type> noParent = new HashSet<Type>();
			Dictionary<Type, int> typeCount = new Dictionary<Type, int>();
			
			HashSet<Type> noDomain = new HashSet<Type>();
			
			foreach (var kv in this.allComponents)
			{
				Type type = kv.Value.GetType();
				if (kv.Value.Parent == null)
				{
					noParent.Add(type);
				}
				
				if (kv.Value.Domain == null)
				{
					noDomain.Add(type);
				}
				
				if (typeCount.ContainsKey(type))
				{
					typeCount[type]++;
				}
				else
				{
					typeCount[type] = 1;
				}
			}

			sb.AppendLine("not set parent type: ");
			foreach (Type type in noParent)
			{
				sb.AppendLine($"\t{type.Name}");	
			}
			
			sb.AppendLine("not set domain type: ");
			foreach (Type type in noDomain)
			{
				sb.AppendLine($"\t{type.Name}");	
			}

			IOrderedEnumerable<KeyValuePair<Type, int>> orderByDescending = typeCount.OrderByDescending(s => s.Value);
			
			sb.AppendLine("Entity Count: ");
			foreach (var kv in orderByDescending)
			{
				if (kv.Value == 1)
				{
					continue;
				}
				sb.AppendLine($"\t{kv.Key.Name}: {kv.Value}");
			}

			return sb.ToString();
		}

		public void Dispose()
		{
			instance = null;
		}

	}
}