using System;
using System.Collections.Generic;
using ETModel;

namespace Xfs
{
	[XfsObjectSystem]
	public class MessageDispatcherComponentAwakeSystem : XfsAwakeSystem<XfsMessageDispatcherComponent>
	{
		public override void Awake(XfsMessageDispatcherComponent self)
		{
			self.Load();
		}
	}

	[XfsObjectSystem]
	public class MessageDispatcherComponentLoadSystem : XfsLoadSystem<XfsMessageDispatcherComponent>
	{
		public override void Load(XfsMessageDispatcherComponent self)
		{
			self.Load();
		}
	}

	/// <summary>
	/// 消息分发组件
	/// </summary>
	public static class MessageDispatcherComponentHelper
	{
		public static void Load(this XfsMessageDispatcherComponent self)
		{
			self.Handlers.Clear();

			XfsSenceType appType = XfsGame.XfsSence.Type;
			List<Type> types = XfsGame.EventSystem.GetTypes(typeof(XfsMessageHandlerAttribute));

			foreach (Type type in types)
			{
				object[] attrs = type.GetCustomAttributes(typeof(XfsMessageHandlerAttribute), false);
				if (attrs.Length == 0)
				{
					continue;
				}

				XfsMessageHandlerAttribute messageHandlerAttribute = attrs[0] as XfsMessageHandlerAttribute;
				if (!messageHandlerAttribute.Type.Is(appType))
				{
					continue;
				}

				IXfsMHandler iMHandler = Activator.CreateInstance(type) as IXfsMHandler;
				if (iMHandler == null)
				{
					Console.WriteLine($"message handle {type.Name} 需要继承 IMHandler");
					continue;
				}

				Type messageType = iMHandler.GetMessageType();
				int opcode = XfsGame.XfsSence.GetComponent<XfsOpcodeTypeComponent>().GetOpcode(messageType);
				if (opcode == 0)
				{
					Console.WriteLine($"消息opcode为0: {messageType.Name}");
					continue;
				}
				self.RegisterHandler(opcode, iMHandler);
			}
		}

		public static void RegisterHandler(this XfsMessageDispatcherComponent self, int opcode, IXfsMHandler handler)
		{
			if (!self.Handlers.ContainsKey(opcode))
			{
				self.Handlers.Add(opcode, new List<IXfsMHandler>());
			}
			self.Handlers[opcode].Add(handler);
		}

		public static void Handle(this XfsMessageDispatcherComponent self, XfsSession session, XfsMessageInfo messageInfo)
		{
			List<IXfsMHandler> actions;
			if (!self.Handlers.TryGetValue(messageInfo.Opcode, out actions))
			{
				Console.WriteLine($"消息没有处理: {messageInfo.Opcode} {XfsJsonHelper.ToJson(messageInfo.Message)}");
				return;
			}
			
			foreach (IXfsMHandler ev in actions)
			{
				try
				{
					ev.Handle(session, messageInfo.Message);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
	}

}