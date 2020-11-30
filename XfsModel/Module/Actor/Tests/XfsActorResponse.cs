namespace Xfs
{
	/// <summary>
	/// actor RPC消息响应
	/// </summary>
	[XfsMessage(XfsOpcode.ActorResponse)]
	public class XfsActorResponse : IXfsActorLocationResponse
	{
		public int RpcId { get; set; }

		public int Error { get; set; }

		public string Message { get; set; }
	}
}
