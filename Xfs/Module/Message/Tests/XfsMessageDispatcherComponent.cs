using System.Collections.Generic;

namespace Xfs
{
	/// <summary>
	/// 消息分发组件
	/// </summary>
	public class XfsMessageDispatcherComponent : XfsComponent
	{
		public readonly Dictionary<ushort, List<IXfsMHandler>> Handlers = new Dictionary<ushort, List<IXfsMHandler>>();
	}
}