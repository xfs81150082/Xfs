using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsUpdateSystem
    {
        Type Type();
        void Run(object o);
    }
	public abstract class XfsUpdateSystem<T> : IXfsUpdateSystem
	{
		public void Run(object o)
		{
			this.Update((T)o);
		}

		public Type Type()
		{
			return typeof(T);
		}

		public abstract void Update(T self);
	}

}