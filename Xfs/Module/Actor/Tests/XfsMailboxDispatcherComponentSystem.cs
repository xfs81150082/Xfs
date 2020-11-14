using System;
using System.Collections.Generic;
using ETModel;

namespace Xfs
{
	[XfsObjectSystem]
	public class XfsMailboxDispatcherComponentAwakeSystem: XfsAwakeSystem<XfsMailboxDispatcherComponent>
	{
		public override void Awake(XfsMailboxDispatcherComponent self)
		{
			self.Awake();
		}
	}

	[XfsObjectSystem]
	public class XfsMailboxDispatcherComponentLoadSystem: XfsLoadSystem<XfsMailboxDispatcherComponent>
	{
		public override void Load(XfsMailboxDispatcherComponent self)
		{
			self.Load();
		}
	}

	public static class MailboxDispatcherComponentHelper
	{
		public static void Awake(this XfsMailboxDispatcherComponent self)
		{
			self.Load();
		}

		public static void Load(this XfsMailboxDispatcherComponent self)
		{
			XfsSenceType appType = XfsStartConfigComponent.Instance.StartConfig.SenceType;

			self.MailboxHandlers.Clear();

			List<Type> types = XfsGame.EventSystem.GetTypes(typeof(XfsMailboxHandlerAttribute));

			foreach (Type type in types)
			{
				object[] attrs = type.GetCustomAttributes(typeof(XfsMailboxHandlerAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

				XfsMailboxHandlerAttribute mailboxHandlerAttribute = (XfsMailboxHandlerAttribute) attrs[0];
				if (!mailboxHandlerAttribute.Type.Is(appType))
				{
					continue;
				}

				object obj = Activator.CreateInstance(type);

				IXfsMailboxHandler iMailboxHandler = obj as IXfsMailboxHandler;
				if (iMailboxHandler == null)
				{
					throw new Exception($"actor handler not inherit IEntityActorHandler: {obj.GetType().FullName}");
				}

				self.MailboxHandlers.Add(mailboxHandlerAttribute.MailboxType, iMailboxHandler);
			}
		}

		/// <summary>
		/// 根据mailbox类型做不同的处理
		/// </summary>
		//public static async XfsTask Handle(
		//		this XfsMailboxDispatcherComponent self, XfsMailboxComponent mailBoxComponent, XfsActorMessageInfo actorMessageInfo)
		//{
		//	IXfsMailboxHandler iMailboxHandler;
		//	if (self.MailboxHandlers.TryGetValue(mailBoxComponent.MailboxType, out iMailboxHandler))
		//	{
		//		await iMailboxHandler.Handle(actorMessageInfo.Session, mailBoxComponent.Entity, actorMessageInfo.Message);
  //          }
           
		//}
	}
}
