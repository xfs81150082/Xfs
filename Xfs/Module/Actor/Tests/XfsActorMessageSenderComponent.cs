using System;
using System.Net;

namespace Xfs
{
	public class XfsActorMessageSenderComponent : XfsComponent
	{
		public XfsActorMessageSender Get(long actorId)
		{
			if (actorId == 0)
			{
				throw new Exception($"actor id is 0");
			}
			IPEndPoint ipEndPoint = XfsStartConfigComponent.Instance.GetInnerAddress(XfsIdGeneraterHelper.GetAppId(actorId));
			XfsActorMessageSender actorMessageSender = new XfsActorMessageSender(actorId, ipEndPoint);
			return actorMessageSender;
		}
	}
}
