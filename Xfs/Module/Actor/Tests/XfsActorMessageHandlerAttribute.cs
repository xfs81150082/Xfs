namespace Xfs
{
	public class XfsActorMessageHandlerAttribute : XfsBaseAttribute
	{
		public XfsSceneType Type { get; }

		public XfsActorMessageHandlerAttribute(XfsSceneType appType)
		{
			this.Type = appType;
		}
	}
}