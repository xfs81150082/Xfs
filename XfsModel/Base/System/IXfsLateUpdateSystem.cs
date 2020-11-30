using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsLateUpdateSystem
    {
        Type Type();
        void Run(object o);
    }
	public abstract class XfsLateUpdateSystem<T> : IXfsLateUpdateSystem
	{
		public void Run(object o)
		{
			this.LateUpdate((T)o);
		}

		public Type Type()
		{
			return typeof(T);
		}

		public abstract void LateUpdate(T self);
	}

}
