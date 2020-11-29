namespace Xfs
{
	public class XfsMailboxHandlerAttribute : XfsBaseAttribute
	{
		public XfsSceneType Type { get; }

		public string MailboxType { get; }

		public XfsMailboxHandlerAttribute(XfsSceneType appType, string mailboxType)
		{
			this.Type = appType;
			this.MailboxType = mailboxType;
		}
	}
}