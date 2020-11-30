using System.Collections.Generic;

namespace Xfs
{
	/// <summary>
	/// 消息分发组件
	/// </summary>
	public class XfsMessageDispatcherComponent : XfsEntity
	{	
		public static XfsMessageDispatcherComponent Instace { get; set; }
		public readonly Dictionary<int, List<IXfsMHandler>> Handlers = new Dictionary<int, List<IXfsMHandler>>();
	}
}