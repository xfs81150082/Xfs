using System.Net;

namespace Xfs
{
	// 知道对方的instanceId，使用这个类发actor消息
	public struct XfsActorMessageSender
	{
		// actor的地址
		public IPEndPoint Address { get; }

		public long ActorId { get; }

		public XfsActorMessageSender(long actorId, IPEndPoint address)
		{
			this.ActorId = actorId;
			this.Address = address;
		}
	}
}