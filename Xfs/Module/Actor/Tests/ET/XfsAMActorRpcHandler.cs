using System;

namespace Xfs
{
	public abstract class XfsAMActorRpcHandler<E, Request, Response>: IXfsMActorHandler where E: XfsEntity where Request: class, IXfsActorRequest where Response : class, IXfsActorResponse
	{
		protected static void ReplyError(Response response, Exception e, Action<Response> reply)
		{
			//Log.Error(e);
			response.Error = XfsErrorCode.ERR_RpcFail;
			response.Message = e.ToString();
			reply(response);
		}
		protected abstract XfsTask Run(E unit, Request message, Action<Response> reply);

        public XfsTask Handle(XfsSession session, XfsEntity entity, object actorMessage)
        {
            try
            {
                Request request = actorMessage as Request;
                if (request == null)
                {
                    //Log.Error($"消息类型转换错误: {actorMessage.GetType().FullName} to {typeof (Request).Name}");
                    return XfsTask.CompletedTask;
                }
                E e = entity as E;
                if (e == null)
                {
					//Log.Error($"Actor类型转换错误: {entity.GetType().Name} to {typeof(E).Name}");
					return XfsTask.CompletedTask;
				}

				int rpcId = request.RpcId;

                long instanceId = session.InstanceId;

                this.Run(e, request, response =>
                {
                        // 等回调回来,session可以已经断开了,所以需要判断session InstanceId是否一样
                        if (session.InstanceId != instanceId)
                    {
                        return;
                    }
                    response.RpcId = rpcId;

                    session.Reply(response);

				});
				return XfsTask.CompletedTask;
			}
			catch (Exception e)
            {
                throw new Exception($"解释消息失败: {actorMessage.GetType().FullName}", e);
            }
        }

		//public async XfsTask Handle(XfsSession session, XfsEntity entity, object actorMessage)
		//{
		//	try
		//	{
		//		Request request = actorMessage as Request;
		//		if (request == null)
		//		{
		//			//Log.Error($"消息类型转换错误: {actorMessage.GetType().FullName} to {typeof (Request).Name}");
		//			return;
		//		}
		//		E e = entity as E;
		//		if (e == null)
		//		{
		//			//Log.Error($"Actor类型转换错误: {entity.GetType().Name} to {typeof(E).Name}");
		//			return;
		//		}

		//		int rpcId = request.RpcId;
				
		//		long instanceId = session.InstanceId;
				
		//		await this.Run(e, request, response =>
		//		{
		//			// 等回调回来,session可以已经断开了,所以需要判断session InstanceId是否一样
		//			if (session.InstanceId != instanceId)
		//			{
		//				return;
		//			}
		//			response.RpcId = rpcId;
					
		//			session.Reply(response);
		//		});
		//	}
		//	catch (Exception e)
		//	{
		//		throw new Exception($"解释消息失败: {actorMessage.GetType().FullName}", e);
		//	}
		//}

		public Type GetMessageType()
		{
			return typeof (Request);
		}

    }
}