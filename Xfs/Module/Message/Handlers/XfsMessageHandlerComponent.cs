using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xfs
{
	public class XfsMessageHandlerComponent : XfsComponent
	{
		public readonly Dictionary<ushort, List<IXfsMHandler>> Handlers = new Dictionary<ushort, List<IXfsMHandler>>();
	}
}