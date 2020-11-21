using System.Collections.Generic;

namespace Xfs
{
	/// <summary>
	/// mailbox分发组件,不同类型的mailbox交给不同的MailboxHandle处理
	/// </summary>
	public class XfsMailboxDispatcherComponent : XfsComponent
	{
		public readonly Dictionary<string, IXfsMailboxHandler> MailboxHandlers = new Dictionary<string, IXfsMailboxHandler>();

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();

			this.MailboxHandlers.Clear();
		}
	}
}