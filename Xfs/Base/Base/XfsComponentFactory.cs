using System;

namespace Xfs
{
	public static class XfsComponentFactory
	{
		public static XfsComponent CreateWithParent(Type type, XfsComponent parent, bool fromPool = true)
		{
			XfsComponent component;
			if (fromPool)
			{
				component = XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (XfsComponent)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			component.Parent = parent;
			if (component is XfsComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			XfsGame.EventSystem.Awake(component);
			return component;
		}

		public static T CreateWithParent<T>(XfsComponent parent, bool fromPool = true) where T : XfsComponent
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			component.Parent = parent;
			if (component is XfsComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			XfsGame.EventSystem.Awake(component);
			return component;
		}

		public static T CreateWithParent<T, A>(XfsComponent parent, A a, bool fromPool = true) where T : XfsComponent
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			component.Parent = parent;
			if (component is XfsComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			XfsGame.EventSystem.Awake(component, a);
			return component;
		}

		public static T CreateWithParent<T, A, B>(XfsComponent parent, A a, B b, bool fromPool = true) where T : XfsComponent
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			component.Parent = parent;
			if (component is XfsComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			XfsGame.EventSystem.Awake(component, a, b);
			return component;
		}

		public static T CreateWithParent<T, A, B, C>(XfsComponent parent, A a, B b, C c, bool fromPool = true) where T : XfsComponent
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			component.Parent = parent;
			if (component is XfsComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			XfsGame.EventSystem.Awake(component, a, b, c);
			return component;
		}

		public static T Create<T>(bool fromPool = true) where T : XfsComponent
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			if (component is XfsComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			XfsGame.EventSystem.Awake(component);
			return component;
		}

		public static T Create<T, A>(A a, bool fromPool = true) where T : XfsComponent
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			if (component is XfsComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			XfsGame.EventSystem.Awake(component, a);
			return component;
		}

		public static T Create<T, A, B>(A a, B b, bool fromPool = true) where T : XfsComponent
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			if (component is XfsComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			XfsGame.EventSystem.Awake(component, a, b);
			return component;
		}

		public static T Create<T, A, B, C>(A a, B b, C c, bool fromPool = true) where T : XfsComponent
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			if (component is XfsComponentWithId componentWithId)
			{
				componentWithId.Id = component.InstanceId;
			}
			XfsGame.EventSystem.Awake(component, a, b, c);
			return component;
		}

		public static T CreateWithId<T>(long id, bool fromPool = true) where T : XfsComponentWithId
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			component.Id = id;
			XfsGame.EventSystem.Awake(component);
			return component;
		}

		public static T CreateWithId<T, A>(long id, A a, bool fromPool = true) where T : XfsComponentWithId
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			component.Id = id;
			XfsGame.EventSystem.Awake(component, a);
			return component;
		}

		public static T CreateWithId<T, A, B>(long id, A a, B b, bool fromPool = true) where T : XfsComponentWithId
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			component.Id = id;
			XfsGame.EventSystem.Awake(component, a, b);
			return component;
		}

		public static T CreateWithId<T, A, B, C>(long id, A a, B b, C c, bool fromPool = true) where T : XfsComponentWithId
		{
			Type type = typeof(T);

			T component;
			if (fromPool)
			{
				component = (T)XfsGame.ObjectPool.Fetch(type);
			}
			else
			{
				component = (T)Activator.CreateInstance(type);
			}

			XfsGame.EventSystem.Add(component);

			component.Id = id;
			XfsGame.EventSystem.Awake(component, a, b, c);
			return component;
		}
	}
}
