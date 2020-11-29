using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsDeserializeSystem
    {
        Type Type();
        void Run(object o);
    }
	 /// 反序列化后执行的System
	 /// 要小心使用这个System，因为对象假如要保存到数据库，到dbserver也会进行反序列化，那么也会执行该System
	public abstract class XfsDeserializeSystem<T> : IXfsDeserializeSystem
	{
		public void Run(object o)
		{
			this.Deserialize((T)o);
		}

		public Type Type()
		{
			return typeof(T);
		}

		public abstract void Deserialize(T self);
	}
}
