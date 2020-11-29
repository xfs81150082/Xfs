using System;

namespace Xfs
{
	public static class XfsEntityFactory
	{
		public static XfsEntity CreateWithParent(Type type, XfsEntity parent)
		{
			XfsEntity component = XfsEntity.Create(type, true);
			component.Id = XfsIdGeneraterHelper.GenerateId();
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component);
			return component;
		}

		public static T CreateWithParent<T>(XfsEntity parent) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Id = XfsIdGeneraterHelper.GenerateId();
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component);
			return component;
		}

		public static T CreateWithParent<T, A>(XfsEntity parent, A a) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Id = XfsIdGeneraterHelper.GenerateId();
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component, a);
			return component;
		}

		public static T CreateWithParent<T, A, B>(XfsEntity parent, A a, B b) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Id = XfsIdGeneraterHelper.GenerateId();
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component, a, b);
			return component;
		}

		public static T CreateWithParent<T, A, B, C>(XfsEntity parent, A a, B b, C c, bool fromPool = true) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Id = XfsIdGeneraterHelper.GenerateId();
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component, a, b, c);
			return component;
		}

		public static T CreateWithParent<T, A, B, C, D>(XfsEntity parent, A a, B b, C c, D d, bool fromPool = true) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Id = XfsIdGeneraterHelper.GenerateId();
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component, a, b, c, d);
			return component;
		}


		public static XfsEntity CreateWithParentAndId(Type type, XfsEntity parent, long id)
		{
			XfsEntity component = XfsEntity.Create(type, true);
			component.Id = id;
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component);
			return component;
		}

		public static T CreateWithParentAndId<T>(XfsEntity parent, long id) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Id = id;
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component);
			return component;
		}

		public static T CreateWithParentAndId<T, A>(XfsEntity parent, long id, A a) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Id = id;
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component, a);
			return component;
		}

		public static T CreateWithParentAndId<T, A, B>(XfsEntity parent, long id, A a, B b) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Id = id;
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component, a, b);
			return component;
		}

		public static T CreateWithParentAndId<T, A, B, C>(XfsEntity parent, long id, A a, B b, C c, bool fromPool = true) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Id = id;
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component, a, b, c);
			return component;
		}

		public static T CreateWithParentAndId<T, A, B, C, D>(XfsEntity parent, long id, A a, B b, C c, D d, bool fromPool = true) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Id = id;
			component.Parent = parent;

			XfsEventSystem.Instance.Awake(component, a, b, c, d);
			return component;
		}


		public static XfsEntity Create(XfsEntity domain, Type type)
		{
			XfsEntity component = XfsEntity.Create(type, true);
			component.Domain = domain;
			component.Id = XfsIdGeneraterHelper.GenerateId();

			XfsEventSystem.Instance.Awake(component);
			return component;
		}


		public static T Create<T>(XfsEntity domain) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Domain = domain;
			component.Id = XfsIdGeneraterHelper.GenerateId();
			XfsEventSystem.Instance.Awake(component);
			return component;
		}

		public static T Create<T, A>(XfsEntity domain, A a) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Domain = domain;
			component.Id = XfsIdGeneraterHelper.GenerateId();
			XfsEventSystem.Instance.Awake(component, a);
			return component;
		}

		public static T Create<T, A, B>(XfsEntity domain, A a, B b) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Domain = domain;
			component.Id = XfsIdGeneraterHelper.GenerateId();
			XfsEventSystem.Instance.Awake(component, a, b);
			return component;
		}

		public static T Create<T, A, B, C>(XfsEntity domain, A a, B b, C c) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Domain = domain;
			component.Id = XfsIdGeneraterHelper.GenerateId();
			XfsEventSystem.Instance.Awake(component, a, b, c);
			return component;
		}

		public static T CreateWithId<T>(XfsEntity domain, long id) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Domain = domain;
			component.Id = id;
			XfsEventSystem.Instance.Awake(component);
			return component;
		}

		public static T CreateWithId<T, A>(XfsEntity domain, long id, A a) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Domain = domain;
			component.Id = id;
			XfsEventSystem.Instance.Awake(component, a);
			return component;
		}

		public static T CreateWithId<T, A, B>(XfsEntity domain, long id, A a, B b) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Domain = domain;
			component.Id = id;
			XfsEventSystem.Instance.Awake(component, a, b);
			return component;
		}

		public static T CreateWithId<T, A, B, C>(XfsEntity domain, long id, A a, B b, C c) where T : XfsEntity
		{
			Type type = typeof(T);
			T component = (T)XfsEntity.Create(type, true);
			component.Domain = domain;
			component.Id = id;
			XfsEventSystem.Instance.Awake(component, a, b, c);
			return component;
		}

	}
}
