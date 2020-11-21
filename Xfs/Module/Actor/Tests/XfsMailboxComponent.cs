using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
	/// <summary>
	/// 用于存储邮件信息的数据结构体
	/// </summary>
	public struct XfsActorMessageInfo
	{
		public XfsSession Session;
		public object Message;
	}
	/// <summary>
	/// 挂上这个组件表示该Entity是一个Actor,接收的消息将会队列处理
	/// </summary>
	public class XfsMailboxComponent : XfsComponent
	{
		/// Mailbox的类型
		public string MailboxType;
		/// 队列处理消息
		public Queue<XfsActorMessageInfo> Queue = new Queue<XfsActorMessageInfo>();
		public XfsTaskCompletionSource<XfsActorMessageInfo> Tcs;
		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();

			var t = this.Tcs;
			this.Tcs = null;
			t?.SetResult(new XfsActorMessageInfo());
		}
	}

}