namespace Xfs
{
	/// <summary>
	/// 挂上这个组件表示该Entity是一个Actor,接收的消息将会队列处理
	/// </summary>
	public class XfsMailBoxComponent : XfsEntity
	{
		// Mailbox的类型
		public XfsMailboxType MailboxType;
	}
}