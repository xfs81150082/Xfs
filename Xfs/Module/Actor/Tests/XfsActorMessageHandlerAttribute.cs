namespace Xfs
{
	public class XfsActorMessageHandlerAttribute : XfsBaseAttribute
	{
		public XfsSenceType Type { get; }

		public XfsActorMessageHandlerAttribute(XfsSenceType appType)
		{
			this.Type = appType;
		}
	}
}