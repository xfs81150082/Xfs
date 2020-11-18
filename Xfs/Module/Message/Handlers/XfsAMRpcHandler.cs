using System;

namespace Xfs
{
	public abstract class XfsAMRpcHandler<Request, Response>: IXfsMHandler where Request : class, IXfsRequest where Response : class, IXfsResponse 
	{
		protected static void ReplyError(Response response, Exception e, Action<Response> reply)
		{
			Console.WriteLine(XfsTimeHelper.CurrentTime()+" : "+e);
			//Log.Error(e);
			//response.Error = ErrorCode.ERR_RpcFail;
			response.Message = e.ToString();
			reply(response);
		}

		protected abstract void Run(XfsSession session, Request message, Action<Response> reply);

		public void Handle(XfsSession session, object message)
		{
			try
			{
				Request request = message as Request;
				if (request == null)
				{
					//Log.Error($"消息类型转换错误: {message.GetType().Name} to {typeof(Request).Name}");
					Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + $"消息类型转换错误: {message.GetType().Name} to {typeof(Request).Name}");
				}

				int rpcId = request.RpcId;

				long instanceId = session.InstanceId;
				
				this.Run(session, request, response =>
				{
					// 等回调回来,session可以已经断开了,所以需要判断session InstanceId是否一样
					if (session.InstanceId != instanceId)
					{
						return;
					}

					response.RpcId = rpcId;
					session.Reply(response);
				});
			}
			catch (Exception e)
			{
				throw new Exception($"解释消息失败: {message.GetType().FullName}", e);
			}
		}

		public Type GetMessageType()
		{
			return typeof (Request);
		}
	}
}