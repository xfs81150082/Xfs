using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsAwakeSystem
    {
        Type Type();
    }
	public interface IXfsAwake
	{
		void Run(object o);
	}

	public interface IXfsAwake<A>
	{
		void Run(object o, A a);
	}

	public interface IXfsAwake<A, B>
	{
		void Run(object o, A a, B b);
	}

	public interface IXfsAwake<A, B, C>
	{
		void Run(object o, A a, B b, C c);
	}

	public abstract class XfsAwakeSystem<T> : IXfsAwakeSystem, IXfsAwake
	{
		public Type Type()
		{
			return typeof(T);
		}

		public void Run(object o)
		{
			this.Awake((T)o);
		}

		public abstract void Awake(T self);
	}

	public abstract class XfsAwakeSystem<T, A> : IXfsAwakeSystem, IXfsAwake<A>
	{
		public Type Type()
		{
			return typeof(T);
		}

		public void Run(object o, A a)
		{
			this.Awake((T)o, a);
		}

		public abstract void Awake(T self, A a);
	}

	public abstract class XfsAwakeSystem<T, A, B> : IXfsAwakeSystem, IXfsAwake<A, B>
	{
		public Type Type()
		{
			return typeof(T);
		}

		public void Run(object o, A a, B b)
		{
			this.Awake((T)o, a, b);
		}

		public abstract void Awake(T self, A a, B b);
	}

	public abstract class XfsAwakeSystem<T, A, B, C> : IXfsAwakeSystem, IXfsAwake<A, B, C>
	{
		public Type Type()
		{
			return typeof(T);
		}

		public void Run(object o, A a, B b, C c)
		{
			this.Awake((T)o, a, b, c);
		}

		public abstract void Awake(T self, A a, B b, C c);
	}


}