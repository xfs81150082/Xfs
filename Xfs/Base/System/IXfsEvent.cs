using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsEvent
    {
        void Handle();
        void Handle(object a);
        void Handle(object a, object b);
        void Handle(object a, object b, object c);
    }
	public abstract class XfsAEvent : IXfsEvent
	{
		public void Handle()
		{
			this.Run();
		}
		public void Handle(object a)
		{
			throw new NotImplementedException();
		}
		public void Handle(object a, object b)
		{
			throw new NotImplementedException();
		}
		public void Handle(object a, object b, object c)
		{
			throw new NotImplementedException();
		}
		public abstract void Run();
	}
	public abstract class XfsAEvent<A> : IXfsEvent
	{
		public void Handle()
		{
			throw new NotImplementedException();
		}

		public void Handle(object a)
		{
			this.Run((A)a);
		}

		public void Handle(object a, object b)
		{
			throw new NotImplementedException();
		}

		public void Handle(object a, object b, object c)
		{
			throw new NotImplementedException();
		}

		public abstract void Run(A a);
	}

	public abstract class XfsAEvent<A, B> : IXfsEvent
	{
		public void Handle()
		{
			throw new NotImplementedException();
		}

		public void Handle(object a)
		{
			throw new NotImplementedException();
		}

		public void Handle(object a, object b)
		{
			this.Run((A)a, (B)b);
		}

		public void Handle(object a, object b, object c)
		{
			throw new NotImplementedException();
		}

		public abstract void Run(A a, B b);
	}

	public abstract class XfsAEvent<A, B, C> : IXfsEvent
	{
		public void Handle()
		{
			throw new NotImplementedException();
		}

		public void Handle(object a)
		{
			throw new NotImplementedException();
		}

		public void Handle(object a, object b)
		{
			throw new NotImplementedException();
		}

		public void Handle(object a, object b, object c)
		{
			this.Run((A)a, (B)b, (C)c);
		}

		public abstract void Run(A a, B b, C c);
	}


}