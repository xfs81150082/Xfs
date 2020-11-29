using System;
using System.Collections.Generic;

namespace Xfs
{
	/// Actor消息分发组件
	public class XfsActorMessageDispatcherComponent : XfsEntity
	{
		public static XfsActorMessageDispatcherComponent Instance;
		public readonly Dictionary<Type, IXfsMActorHandler> ActorMessageHandlers = new Dictionary<Type, IXfsMActorHandler>();	
	}
}