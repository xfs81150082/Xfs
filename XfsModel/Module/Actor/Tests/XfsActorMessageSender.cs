using System;

namespace Xfs
{
	// 知道对方的instanceId，使用这个类发actor消息
	public struct XfsActorMessageSender
	{
		// 最近接收或者发送消息的时间
		public long CreateTime { get; }
		// actor的地址
		public Action<IXfsActorResponse> Callback { get; }

		public XfsActorMessageSender(Action<IXfsActorResponse> callback)
		{
			this.CreateTime = XfsTimeHelper.Now();
			this.Callback = callback;
		}
	}
}