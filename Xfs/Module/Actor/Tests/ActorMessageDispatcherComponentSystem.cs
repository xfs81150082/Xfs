using System;
using System.Collections.Generic;
using ETModel;

namespace Xfs
{
	[XfsObjectSystem]
	public class XfsActorMessageDispatcherComponentStartSystem : XfsAwakeSystem<XfsActorMessageDispatcherComponent>
	{
		public override void Awake(XfsActorMessageDispatcherComponent self)
		{
			//self.Awake();
		}
	}

	[XfsObjectSystem]
	public class XfsActorMessageDispatcherComponentLoadSystem : XfsLoadSystem<XfsActorMessageDispatcherComponent>
	{
		public override void Load(XfsActorMessageDispatcherComponent self)
		{
			//self.Load();
		}
	}

	/// <summary>
	/// Actor消息分发组件
	/// </summary>
	public static class ActorMessageDispatcherComponentHelper
	{
		public static void Awake(this XfsActorMessageDispatcherComponent self)
		{
			self.Load();
		}

		public static void Load(this XfsActorMessageDispatcherComponent self)
		{
			XfsSenceType appType = XfsStartConfigComponent.Instance.StartConfig.SenceType;

			self.ActorMessageHandlers.Clear();

			List<Type> types = XfsGame.EventSystem.GetTypes(typeof(XfsActorMessageHandlerAttribute));

			types = XfsGame.EventSystem.GetTypes(typeof (XfsActorMessageHandlerAttribute));
			foreach (Type type in types)
			{
				object[] attrs = type.GetCustomAttributes(typeof(XfsActorMessageHandlerAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

				XfsActorMessageHandlerAttribute messageHandlerAttribute = (XfsActorMessageHandlerAttribute) attrs[0];
				if (!messageHandlerAttribute.Type.Is(appType))
				{
					continue;
				}

				object obj = Activator.CreateInstance(type);

				IXfsMActorHandler imHandler = obj as IXfsMActorHandler;
				if (imHandler == null)
				{
					throw new Exception($"message handler not inherit IMActorHandler abstract class: {obj.GetType().FullName}");
				}

				Type messageType = imHandler.GetMessageType();
				self.ActorMessageHandlers.Add(messageType, imHandler);
			}
		}

		/// <summary>
		/// 分发actor消息
		/// </summary>
		//public static async XfsTask Handle(
		//		this XfsActorMessageDispatcherComponent self, XfsEntity entity, XfsActorMessageInfo actorMessageInfo)
		//{
		//	if (!self.ActorMessageHandlers.TryGetValue(actorMessageInfo.Message.GetType(), out IXfsMActorHandler handler))
		//	{
		//		//throw new Exception($"not found message handler: {XfsMongoHelper.ToJson(actorMessageInfo.Message)}");
		//	}

		//	await handler.Handle(actorMessageInfo.Session, entity, actorMessageInfo.Message);
		//}
	}
}
