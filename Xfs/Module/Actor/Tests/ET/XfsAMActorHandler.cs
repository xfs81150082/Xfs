using System;

namespace Xfs
{
	public abstract class XfsAMActorHandler<E, Message>: IXfsMActorHandler where E: XfsEntity where Message : class, IXfsActorMessage
	{
		protected abstract void Run(E entity, Message message);

        public async XfsTask Handle(XfsSession session, XfsEntity entity, object actorMessage)
        {
            Message msg = actorMessage as Message;
            if (msg == null)
            {
                //Log.Error($"消息类型转换错误: {actorMessage.GetType().FullName} to {typeof(Message).Name}");
                Console.WriteLine($"消息类型转换错误: {actorMessage.GetType().FullName} to {typeof(Message).Name}");
                return;
            }
            E e = entity as E;
            if (e == null)
            {
                //Log.Error($"Actor类型转换错误: {entity.GetType().Name} to {typeof(E).Name}");
                Console.WriteLine($"Actor类型转换错误: {entity.GetType().Name} to {typeof(E).Name}");
                return;
            }

            this.Run(e, msg);

            await XfsTask.CompletedTask;
        }

        public Type GetMessageType()
		{
			return typeof (Message);
		}

    }
}