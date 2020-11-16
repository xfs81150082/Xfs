namespace Xfs
{
	public class XfsMessageHandlerAttribute : XfsBaseAttribute
	{
		public XfsSenceType Type { get; }
		public XfsMessageHandlerAttribute(){  }
		public XfsMessageHandlerAttribute(XfsSenceType appType)
		{
			this.Type = appType;
		}

	}
}