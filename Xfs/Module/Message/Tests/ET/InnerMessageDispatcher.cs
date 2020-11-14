using ETModel;

namespace Xfs
{
	public class InnerMessageDispatcher : IXfsMessageDispatcher
	{
		public void Dispatch(XfsSession session, ushort opcode, object message)
		{
			// 收到actor消息,放入actor队列
			switch (message)
			{
				case IXfsActorRequest iActorRequest:
					{
						XfsEntity entity = (XfsEntity)XfsGame.EventSystem.Get(iActorRequest.ActorId);
						if (entity == null)
						{
							//Log.Warning($"not found actor: {message}");
							XfsActorResponse response = new XfsActorResponse
							{
								Error = XfsErrorCode.ERR_NotFoundActor,
								RpcId = iActorRequest.RpcId
							};
							session.Reply(response);
							return;
						}

						XfsMailboxComponent mailBoxComponent = entity.GetComponent<XfsMailboxComponent>();
						if (mailBoxComponent == null)
						{
							XfsActorResponse response = new XfsActorResponse
							{
								Error = XfsErrorCode.ERR_ActorNoMailBoxComponent,
								RpcId = iActorRequest.RpcId
							};
							session.Reply(response);
							//Log.Error($"actor not add MailBoxComponent: {entity.GetType().Name} {message}");
							return;
						}

						mailBoxComponent.Add(new XfsActorMessageInfo() { Session = session, Message = iActorRequest });
						return;
					}
				case IXfsActorMessage iactorMessage:
					{
						XfsEntity entity = (XfsEntity)XfsGame.EventSystem.Get(iactorMessage.ActorId);
						if (entity == null)
						{
							//Log.Error($"not found actor: {message}");
							return;
						}

						XfsMailboxComponent mailBoxComponent = entity.GetComponent<XfsMailboxComponent>();
						if (mailBoxComponent == null)
						{
							//Log.Error($"actor not add MailBoxComponent: {entity.GetType().Name} {message}");
							return;
						}

						mailBoxComponent.Add(new XfsActorMessageInfo() { Session = session, Message = iactorMessage });
						return;
					}
				default:
					{
						//XfsGame.XfsSence.GetComponent<XfsMessageDispatcherComponent>().Handle(session, new XfsMessageInfo(opcode, message));
						break;
					}
			}
		}
	}
}
