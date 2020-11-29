using System.Collections.Generic;

namespace Xfs
{
	public class XfsActorMessageSenderComponent : XfsEntity
	{
		public static long TIMEOUT_TIME = 30 * 1000;

		public static XfsActorMessageSenderComponent Instance { get; set; }

		public int RpcId;

		public readonly Dictionary<int, XfsActorMessageSender> requestCallback = new Dictionary<int, XfsActorMessageSender>();

		public long TimeoutCheckTimer;

		public List<int> TimeoutActorMessageSenders = new List<int>();

	}
}
