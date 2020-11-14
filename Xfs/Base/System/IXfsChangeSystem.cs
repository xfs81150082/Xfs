using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsChangeSystem
    {
        Type Type();
        void Run(object o);
	}
	public abstract class XfsChangeSystem<T> : IXfsChangeSystem
	{
		public void Run(object o)
		{
			this.Change((T)o);
		}

		public Type Type()
		{
			return typeof(T);
		}

		public abstract void Change(T self);
	}

}
