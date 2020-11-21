namespace Xfs
{
	public class XfsMailboxHandlerAttribute : XfsBaseAttribute
	{
		public XfsSenceType Type { get; }

		public string MailboxType { get; }

		public XfsMailboxHandlerAttribute(XfsSenceType appType, string mailboxType)
		{
			this.Type = appType;
			this.MailboxType = mailboxType;
		}
	}
}