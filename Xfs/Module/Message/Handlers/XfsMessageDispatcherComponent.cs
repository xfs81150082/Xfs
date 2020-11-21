using System.Collections.Generic;

namespace Xfs
{
	/// <summary>
	/// 消息分发组件
	/// </summary>
	public class XfsMessageDispatcherComponent : XfsComponent
	{
		public readonly Dictionary<int, List<IXfsMHandler>> Handlers = new Dictionary<int, List<IXfsMHandler>>();
	}
}