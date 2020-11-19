using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xfs
{
	public class XfsMessageHandlerComponent : XfsComponent
	{
		public readonly Dictionary<int, List<IXfsMHandler>> Handlers = new Dictionary<int, List<IXfsMHandler>>();
	}
}