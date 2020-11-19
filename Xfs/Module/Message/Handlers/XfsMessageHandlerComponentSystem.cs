using System;
using System.Collections.Generic;
using ETModel;

namespace Xfs
{
	[XfsObjectSystem]
	public class XfsMessageHandlerComponentAwakeSystem : XfsAwakeSystem<XfsMessageHandlerComponent>
	{
		public override void Awake(XfsMessageHandlerComponent self)
		{
			self.Load();
		}
	}

	[XfsObjectSystem]
	public class XfsMessageHandlerComponentLoadSystem : XfsLoadSystem<XfsMessageHandlerComponent>
	{
		public override void Load(XfsMessageHandlerComponent self)
		{
			self.Load();
		}
	}

	/// <summary>
	/// Handler消息分发组件
	/// </summary>
	public static class XfsMessageHandlerComponentHelper
	{
		public static void Load(this XfsMessageHandlerComponent self)
		{
			self.Handlers.Clear();
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsMessageHandlerComponent-33.");

			//XfsSenceType appType = XfsStartConfigComponent.Instance.StartConfig.SenceType;
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
					//Log.Error($"message handle {type.Name} 需要继承 IMHandler");
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

		public static void RegisterHandler(this XfsMessageHandlerComponent self, int opcode, IXfsMHandler handler)
		{
			if (!self.Handlers.ContainsKey(opcode))
			{
				self.Handlers.Add(opcode, new List<IXfsMHandler>());
			}
			self.Handlers[opcode].Add(handler);
		}

		public static void Handle(this XfsMessageHandlerComponent self, XfsSession session, XfsMessageInfo messageInfo)
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