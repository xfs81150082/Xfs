using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsLoadSystem
    {
        Type Type();
        void Run(object o);
    }
	public abstract class XfsLoadSystem<T> : IXfsLoadSystem
	{
		public void Run(object o)
		{
			this.Load((T)o);
		}

		public Type Type()
		{
			return typeof(T);
		}

		public abstract void Load(T self);
	}

}
