using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsDestroySystem
    {
        Type Type();
        void Run(object o);
    }
	public abstract class XfsDestroySystem<T> : IXfsDestroySystem
	{
		public void Run(object o)
		{
			this.Destroy((T)o);
		}

		public Type Type()
		{
			return typeof(T);
		}

		public abstract void Destroy(T self);
	}

}