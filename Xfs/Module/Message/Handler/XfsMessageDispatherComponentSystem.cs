using System;
using System.Collections.Generic;


namespace Xfs
{
	public class MessageDispatcherComponentAwakeSystem : XfsAwakeSystem<XfsMessageDispatcherComponent>
	{
		public override void Awake(XfsMessageDispatcherComponent self)
		{
			XfsMessageDispatcherComponent.Instace = self;
			self.Load();
		}
	}

	public class MessageDispatcherComponentLoadSystem : XfsLoadSystem<XfsMessageDispatcherComponent>
	{
		public override void Load(XfsMessageDispatcherComponent self)
		{
			self.Load();
		}
	}

	public class MessageDispatcherComponentDestroySystem : XfsDestroySystem<XfsMessageDispatcherComponent>
	{
		public override void Destroy(XfsMessageDispatcherComponent self)
		{
			XfsMessageDispatcherComponent.Instace = null;
			self.Handlers.Clear();
		}
	}

	/// <summary>
	/// 消息分发组件
	/// </summary>
	public static class XfsMessageDispatcherComponentHelper
	{
		public static void Load(this XfsMessageDispatcherComponent self)
		{
			self.Handlers.Clear();

			HashSet<Type> types = XfsEventSystem.Instance.GetTypes(typeof(XfsMessageHandlerAttribute));

			foreach (Type type in types)
			{
				IXfsMHandler iMHandler = Activator.CreateInstance(type) as IXfsMHandler;
				if (iMHandler == null)
				{
					//Log.Error($"message handle {type.Name} 需要继承 IMHandler");
					Console.WriteLine($"message handle {type.Name} 需要继承 IMHandler");
					continue;
				}

				Type messageType = iMHandler.GetMessageType();
				int opcode = XfsOpcodeTypeComponent.Instance.GetOpcode(messageType);
				if (opcode == 0)
				{
					//Log.Error($"消息opcode为0: {messageType.Name}");
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
				//Log.Error($"消息没有处理: {messageInfo.Opcode} {JsonHelper.ToJson(messageInfo.Message)}");
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
					//Log.Error(e);
					Console.WriteLine(e);
				}
			}
		}
	}
}