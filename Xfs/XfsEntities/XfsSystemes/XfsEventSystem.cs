using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
	public enum DLLType
	{
		Xfs,
		XfsConsoleClient,
		XfsConsoleTest,
		XfsDbServer,
		XfsGateSever,
		XfsWinFormsClient,
		XfsWinFormsServer,
		//Model,
		//Hotfix,
		Editor,
	}
	public sealed class XfsEventSystem
    {
		private readonly Dictionary<long, XfsComponent> allComponents = new Dictionary<long, XfsComponent>();

		private readonly Dictionary<DLLType, Assembly> assemblies = new Dictionary<DLLType, Assembly>();
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

		public void Add(DLLType dllType, Assembly assembly)
		{
			this.assemblies[dllType] = assembly;
			this.types.Clear();
			foreach (Assembly value in this.assemblies.Values)
			{
				foreach (Type type in value.GetTypes())
				{
					object[] objects = type.GetCustomAttributes(typeof(XfsBaseAttribute), false);
					if (objects.Length == 0)
					{
						continue;
					}

					XfsBaseAttribute baseAttribute = (XfsBaseAttribute)objects[0];
					this.types.Add(baseAttribute.AttributeType, type);
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
						//Log.Error($"{obj.GetType().Name} 没有继承IEvent");
						Console.WriteLine($"{obj.GetType().Name} 没有继承IEvent");
					}
					this.RegisterEvent(aEventAttribute.Type, iEvent);
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
		public Assembly Get(DLLType dllType)
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

		public XfsComponent Get(long instanceId)
		{
			XfsComponent component = null;
			this.allComponents.TryGetValue(instanceId, out component);
			return component;
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
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
				}
			}
		}

		public void Awake(XfsComponent component)
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
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
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
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
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
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
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
					//Log.Error(e);
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
				}
			}
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
					//Log.Error(e);
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
				}
			}
		}

		public void Load()
		{
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
						//Log.Error(e);
						Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
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
						Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
					}
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
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
				}
			}
		}

		public void Update()
		{
			this.Start();

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
						Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
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
						Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
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
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
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
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
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
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
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
					Console.WriteLine(XfsTimerTool.CurrentTime() + " : " + e);
				}
			}
		}

			
	}
}