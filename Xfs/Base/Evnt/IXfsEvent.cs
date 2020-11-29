using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public interface IXfsEvent
    {
		Type GetEventType();		
    }
	
	public abstract class XfsAEvent<A> : IXfsEvent where A : struct
	{
		public Type GetEventType()
		{
			return typeof(A);
		}
		public abstract XfsTask Run(A a);
	}	

}