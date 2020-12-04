using System;

namespace Xfs
{
	public abstract class XfsAMHandler<Message> : IXfsMHandler where Message: class
	{
		protected abstract void Run(XfsSession session, Message message);

		public void Handle(XfsSession session, object msg)
		{
			Message message = msg as Message;
			if (message == null)
			{
				//Log.Error($"消息类型转换错误: {msg.GetType().Name} to {typeof(Message).Name}");
				return;
			}
			if (session.IsDisposed)
			{
				//Log.Error($"session disconnect {msg}");
				return;
			}
			this.Run(session, message);
		}

		public Type GetMessageType()
		{
			return typeof(Message);
		}
	}
}