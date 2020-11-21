using System;
using System.Collections.Generic;

namespace Xfs
{
	/// <summary>
	/// Actor消息分发组件
	/// </summary>
	public class XfsActorMessageDispatcherComponent : XfsComponent
	{
		public readonly Dictionary<Type, IXfsMActorHandler> ActorMessageHandlers = new Dictionary<Type, IXfsMActorHandler>();

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();

			this.ActorMessageHandlers.Clear();
		}
	}
}