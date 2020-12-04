using System;
using System.Collections.Generic;
using System.Linq;

namespace Xfs
{
	public class XfsActorMessageDispatcherComponentAwakeSystem : XfsAwakeSystem<XfsActorMessageDispatcherComponent>
	{
		public override void Awake(XfsActorMessageDispatcherComponent self)
		{
			XfsActorMessageDispatcherComponent.Instance = self;
			self.Awake();

			//Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsActorMessageDispatcherComponent-13: " + self.ActorMessageHandlers.Count);
			//Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsActorMessageDispatcherComponent-14: " + self.ActorMessageHandlers.Keys.ToList()[0] +" : "+ self.ActorMessageHandlers.Values.ToList()[0]);
		}
	}

	public class XfsActorMessageDispatcherComponentLoadSystem : XfsLoadSystem<XfsActorMessageDispatcherComponent>
	{
		public override void Load(XfsActorMessageDispatcherComponent self)
		{
			self.Load();
		}
	}

	public class XfsActorMessageDispatcherComponentDestroySystem : XfsDestroySystem<XfsActorMessageDispatcherComponent>
	{
		public override void Destroy(XfsActorMessageDispatcherComponent self)
		{
			self.ActorMessageHandlers.Clear();
			XfsActorMessageDispatcherComponent.Instance = null;
		}
	}

	/// <summary>
	/// Actor消息分发组件
	/// </summary>
	public static class XfsActorMessageDispatcherComponentHelper
	{
		public static void Awake(this XfsActorMessageDispatcherComponent self)
		{
			self.Load();
		}

		public static void Load(this XfsActorMessageDispatcherComponent self)
		{
			self.ActorMessageHandlers.Clear();

			HashSet<Type> types = XfsGame.EventSystem.GetTypes(typeof(XfsActorMessageHandlerAttribute));
			foreach (Type type in types)
			{
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
		public static async XfsTask Handle(
				this XfsActorMessageDispatcherComponent self, XfsEntity entity, XfsSession session, object message)
		{
			if (!self.ActorMessageHandlers.TryGetValue(message.GetType(), out IXfsMActorHandler handler))
			{
				throw new Exception($"not found message handler: {message}");
			}

			await handler.Handle(session, entity, message);
		}
	}
}
