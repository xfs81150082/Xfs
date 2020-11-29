using System;


namespace Xfs
{
	public class MailBoxComponentAwakeSystem : XfsAwakeSystem<XfsMailBoxComponent>
	{
		public override void Awake(XfsMailBoxComponent self)
		{
			self.MailboxType = XfsMailboxType.MessageDispatcher;
		}
	}

	public class MailBoxComponentAwake1System : XfsAwakeSystem<XfsMailBoxComponent, XfsMailboxType>
	{
		public override void Awake(XfsMailBoxComponent self, XfsMailboxType mailboxType)
		{
			self.MailboxType = mailboxType;
		}
	}

	public static class MailBoxComponentSystem
	{
		//public static async XfsTask Handle(this XfsMailBoxComponent self, XfsSession session, IXfsActorMessage message)
		//{
		//	using (await XfsCoroutineLockComponent.Instance.Wait(XfsCoroutineLockType.Mailbox, message.ActorId))
		//	{
		//		switch (self.MailboxType)
		//		{
		//			case XfsMailboxType.GateSession:
		//				IXfsActorMessage iActorMessage = message as IXfsActorMessage;
		//				// 发送给客户端
		//				XfsSession clientSession = self.Parent as XfsSession;
		//				iActorMessage.ActorId = 0;
		//				clientSession.Send(iActorMessage);
		//				break;
		//			case XfsMailboxType.MessageDispatcher:
		//				await XfsActorMessageDispatcherComponent.Instance.Handle(self.Parent, session, message);
		//				break;
		//			case XfsMailboxType.UnOrderMessageDispatcher:
		//				self.HandleInner(session, message).Coroutine();
		//				break;
		//		}
		//	}
		//}

		private static async XfsVoid HandleInner(this XfsMailBoxComponent self, XfsSession session, IXfsActorMessage message)
		{
			await XfsActorMessageDispatcherComponent.Instance.Handle(self.Parent, session, message);
		}
	}
}