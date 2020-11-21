using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsStartSystem
    {
        Type Type();
        void Run(object o);
    }
	public abstract class XfsStartSystem<T> : IXfsStartSystem
	{
		public void Run(object o)
		{
			this.Start((T)o);
		}

		public Type Type()
		{
			return typeof(T);
		}

		public abstract void Start(T self);
	}
}
