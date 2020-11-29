using System;

namespace Xfs
{
	public abstract class XfsAMActorRpcHandler<E, Request, Response> : IXfsMActorHandler where E : XfsEntity where Request : class, IXfsActorRequest where Response : class, IXfsActorResponse
	{
		protected abstract XfsTask Run(E unit, Request request, Response response, Action reply);

		public async XfsTask Handle(XfsSession session, XfsEntity entity, object actorMessage)
		{
			try
			{
				Request request = actorMessage as Request;
				if (request == null)
				{
					//Log.Error($"消息类型转换错误: {actorMessage.GetType().FullName} to {typeof(Request).Name}");
					Console.WriteLine($"消息类型转换错误: {actorMessage.GetType().FullName} to {typeof(Request).Name}");
					return;
				}
				E e = entity as E;
				if (e == null)
				{
					//Log.Error($"Actor类型转换错误: {entity.GetType().Name} to {typeof(E).Name}");
					Console.WriteLine($"Actor类型转换错误: {entity.GetType().Name} to {typeof(E).Name}");
					return;
				}

				int rpcId = request.RpcId;
				long instanceId = session.InstanceId;
				Response response = Activator.CreateInstance<Response>();

				void Reply()
				{
					// 等回调回来,session可以已经断开了,所以需要判断session InstanceId是否一样
					if (session.InstanceId != instanceId)
					{
						return;
					}

					response.RpcId = rpcId;
					session.Reply(response);
				}

				try
				{
					await this.Run(e, request, response, Reply);
				}
				catch (Exception exception)
				{
					//Log.Error(exception);
					Console.WriteLine(exception);
					response.Error = XfsErrorCode.ERR_RpcFail;
					response.Message = e.ToString();
					Reply();
				}
			}
			catch (Exception e)
			{
				//Log.Error($"解释消息失败: {actorMessage.GetType().FullName}\n{e}");
				Console.WriteLine($"解释消息失败: {actorMessage.GetType().FullName}\n{e}");
			}
		}

		public Type GetMessageType()
		{
			return typeof(Request);
		}
	}
}