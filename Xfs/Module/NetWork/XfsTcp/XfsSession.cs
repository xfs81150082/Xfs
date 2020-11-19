using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Xfs
{
	[XfsObjectSystem]
	public class XfsSessionAwakeSystem : XfsAwakeSystem<XfsSession>
	{
		public override void Awake(XfsSession self)
		{
			self.Awake();
		}
	}

	public sealed class XfsSession : XfsEntity
	{
		public XfsPacketParser packetParser { get; private set; }	
		public XfsNetWorkComponent Network
		{
			get
			{
				return this.GetParent<XfsNetWorkComponent>();
			}
		}
		public Socket Socket { get; set; }                ///创建一个套接字，用于储藏代理服务端套接字，与客户端通信///客户端Socket 
		public IPEndPoint RemoteAddress { get; set; }
		public bool IsRunning { get; set; }
		public bool IsPeer { get; set; }
		public XfsSenceType SenceType { get; set; }

		//public Queue<XfsParameter> RecvParameters { get; set; } = new Queue<XfsParameter>();
		//public Queue<XfsParameter> SendParameters { get; set; } = new Queue<XfsParameter>();

		private static int RpcId { get; set; }
 		public readonly Dictionary<int, Action<IXfsResponse>> requestCallback = new Dictionary<int, Action<IXfsResponse>>();
	
		//private readonly Dictionary<int, Action<XfsParameter>> parameterCallback = new Dictionary<int, Action<XfsParameter>>();

		public void Awake()
		{
			this.requestCallback.Clear();
			this.AddComponent<XfsHeartComponent>();
            this.packetParser = XfsComponentFactory.CreateWithParent<XfsPacketParser>(this);
			this.packetParser.ReadCallback += this.OnRead;
		}	
		#region
		/// 接收Socket信息
		public void BeginReceiveMessage(Socket socket)
		{
			this.Socket = socket;
			packetParser.BeginReceiveMessage(socket);
			this.OnConnect();


        }
		public void OnConnect()
		{
            this.RemoteAddress = (IPEndPoint)this.Socket.RemoteEndPoint;

			///显示与客户端连接			
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " 客户端连接成功 Is Peer: " + this.IsPeer + " : " + this.Socket.RemoteEndPoint);
		}
		#endregion

		#region  ///接收参数信息
		public void OnRead(object obj, byte[] HeadBytes, byte[] BodyBytes)
		{
			try
			{
				this.XfsRun(obj, HeadBytes, BodyBytes);
			}
			catch (Exception e)
			{
				Console.WriteLine(XfsTimeHelper.CurrentTime() + e);
			}
		}
		public void XfsRun(object obj, byte[] HeadBytes, byte[] BodyBytes)
		{
			///拿出包头中的操代码Opcode，位置4-7字节
			int length = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(HeadBytes, 0));
			int opcode = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(HeadBytes, 4));
			
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " Session-98: " + opcode + " + " + length + " ThreadId: " + Thread.CurrentThread.ManagedThreadId);

			object message = null;
			try
			{
                string jsonStr = Encoding.UTF8.GetString(BodyBytes, 0, BodyBytes.Length);
                XfsOpcodeTypeComponent opcodeTypeComponent = XfsGame.XfsSence.GetComponent<XfsOpcodeTypeComponent>();
				object instance = opcodeTypeComponent.GetInstance(opcode);
                message = JsonConvert.DeserializeObject(jsonStr, instance.GetType());   ////反序列化成功
            }
			catch (Exception e)
			{
				// 出现任何消息解析异常都要断开Session，防止客户端伪造消息				
				Console.WriteLine(XfsTimeHelper.CurrentTime() + e);
				if ((this.Parent as XfsTcpServer) != null)
				{
					if ((this.Parent as XfsTcpServer).TPeers.TryGetValue(this.InstanceId, out XfsSession session))
					{
						(this.Parent as XfsTcpServer).TPeers.Remove(this.InstanceId);
					}
				}
				return;
			}

            IXfsResponse response = message as IXfsResponse;
            if (response == null)
            {                
				XfsMessageInfo messageInfo = new XfsMessageInfo();
                messageInfo.Opcode = opcode;
                messageInfo.Message = message;

                XfsGame.XfsSence.GetComponent<XfsMessageHandlerComponent>().Handle(this, messageInfo);
				return;
            }

			Action<IXfsResponse> action;
            if (!this.requestCallback.TryGetValue(response.RpcId, out action))
            {
                throw new Exception($"not found rpc, response message: {response}");
            }
            this.requestCallback.Remove(response.RpcId);
            action(response);
		}

		//public void RecvBodyBytesParameter(object obj, XfsParameter parameter)
		//{
		//	///将字符串string,用json反序列化转换成MvcParameter参数
		//	if (parameter.TenCode == TenCode.Zero)
		//	{
		//		this.GetComponent<XfsHeartComponent>().CdCount = 0;

		//		Console.WriteLine("{0} 服务端心跳包 {1} 接收成功", XfsTimeHelper.CurrentTime(), Socket.RemoteEndPoint);

		//		return;
		//	}
		//	///将MvcParameter参数分别列队并处理

		//	Action<XfsParameter> action;
		//	if (this.parameterCallback.TryGetValue(parameter.RpcId, out action))
		//	{
		//		this.parameterCallback.Remove(parameter.RpcId);
		//		action(parameter);
		//		return;
		//	}

		//	this.Recv(parameter);
		//}

  //      public void Recv(XfsParameter parameter)
		//{
		//	this.RecvParameters.Enqueue(parameter);
		//	this.OnRecvParameters();
		//}

		//void OnRecvParameters()
		//{
		//	try
		//	{
		//		while (this.RecvParameters.Count > 0)
		//		{
		//			XfsParameter parameter = this.RecvParameters.Dequeue();

  //                  XfsMessageInfo messageInfo = new XfsMessageInfo();
		//			messageInfo.Opcode = parameter.Opcode;
		//			messageInfo.Message = parameter;
		//			XfsGame.XfsSence.GetComponent<XfsMessageHandlerComponent>().Handle(this, messageInfo);

										
		//			//continue;

		//			//XfsHandler handler = null;
		//			//XfsSockets.XfsHandlers.TryGetValue(this.SenceType, out handler);
		//			//if (handler != null)
		//			//{
		//			//	handler.Recv(this, parameter);
		//			//	Console.WriteLine(XfsTimeHelper.CurrentTime() + " RecvParameters: " + this.RecvParameters.Count);
		//			//}
		//			//else
		//			//{
		//			//	Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsHandler is null.");
		//			//	continue;
		//			//}

		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine(XfsTimeHelper.CurrentTime() + ex.Message);
		//	}
		//}
		#endregion

		#region
		/////发送信息，但有回复
		//public XfsTask<XfsParameter> Call(XfsParameter parameter)
		//{
		//	int rpcId = ++RpcId;
		//	var tcs = new XfsTaskCompletionSource<XfsParameter>();

		//	this.parameterCallback[rpcId] = (response) =>
		//	{
		//		try
		//		{
		//			//if (XfsErrorCode.IsRpcNeedThrowException(response.Error))
		//			//{
		//			//	//throw new RpcException(response.Error, response.Message);
		//			//}

		//			tcs.SetResult(response);
		//		}
		//		catch (Exception e)
		//		{
		//			tcs.SetException(new Exception($"Rpc Error: {parameterCallback.GetType().FullName}", e));
		//		}
		//	};

		//	parameter.RpcId = rpcId;

		//	this.Send(parameter);

		//	return tcs.Task;
		//}
		//public void Reply(XfsParameter  message)
		//{
		//	if (this.IsDisposed)
		//	{
		//		throw new Exception("session已经被Dispose了");
		//	}

		//	this.Send(message);
		//}
		//public void Send(XfsParameter mvc)
		//{
		//	this.SendParameters.Enqueue(mvc);
		//	OnSendMvcParameters();
		//}

		/////处理发送参数信息
		//void OnSendMvcParameters()
		//{
		//	try
		//	{
		//		while (this.SendParameters.Count > 0)
		//		{
		//			XfsParameter response = SendParameters.Dequeue();

		//			///用Json将参数（MvcParameter）,序列化转换成字符串（string）
		//			string mvcJsons = XfsJsonHelper.ToJson<XfsParameter>(response);

  //                  this.packetParser.SendString(mvcJsons);

		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine(XfsTimeHelper.CurrentTime() + " OnSendMvcParameters143: " + ex.Message);
		//	}
		//}
		#endregion
			

        #region	
		public XfsTask<IXfsResponse> Call(IXfsRequest request)
		{			
			int rpcId = ++RpcId;
			var tcs = new XfsTaskCompletionSource<IXfsResponse>();

			this.requestCallback[rpcId] = (response) =>
			{
				try
				{
					if (XfsErrorCode.IsRpcNeedThrowException(response.Error))
					{
						Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + response.Message);
					}

					tcs.SetResult(response);
				}
				catch (Exception e)
				{
					tcs.SetException(new Exception($"Rpc Error: {request.GetType().FullName}", e));
				}
			};

			request.RpcId = rpcId;
			this.Send(request);
			return tcs.Task;
		}

		public XfsTask<IXfsResponse> Call(IXfsRequest request, CancellationToken cancellationToken)
		{
			int rpcId = ++RpcId;
			var tcs = new XfsTaskCompletionSource<IXfsResponse>();

			this.requestCallback[rpcId] = (response) =>
			{
				try
				{
					if (XfsErrorCode.IsRpcNeedThrowException(response.Error))
					{
						//throw new RpcException(response.Error, response.Message);
					}

					tcs.SetResult(response);
				}
				catch (Exception e)
				{
					tcs.SetException(new Exception($"Rpc Error: {request.GetType().FullName}", e));
				}
			};

			cancellationToken.Register(() => this.requestCallback.Remove(rpcId));

			request.RpcId = rpcId;
			this.Send(request);
			return tcs.Task;
		}

		public void Reply(IXfsResponse message)
		{
			if (this.IsDisposed)
			{
				throw new Exception("session已经被Dispose了");
			}

			this.Send(message);
		}
		public void Send(IXfsMessage message)
		{
			int opcode = XfsGame.XfsSence.GetComponent<XfsOpcodeTypeComponent>().GetOpcode(message.GetType());
			this.Send(opcode, message);
		}
		public void Send(int opcode, object message)
		{
			if (this.IsDisposed)
			{
				throw new Exception("session已经被Dispose了");
			}

			///用Json将参数（MvcParameter）,序列化转换成字符串（string）
			string msgJsons = XfsJsonHelper.ToJson(message);

			///将字符串(string)转换成字节(byte)
			byte[] packetBody = Encoding.UTF8.GetBytes(msgJsons);

			int msgLength = 8 + packetBody.Length;
			byte[] packetBytes = new byte[msgLength];

			///包体长度（不含包头的8个字节的长度），存在包头，占用4个字节(0,1,2,3).
			BitConverter.GetBytes(IPAddress.HostToNetworkOrder(packetBody.Length)).CopyTo(packetBytes, 0);

			///信息类型，存在包头，占用4个字节(4,5,6,7).
			BitConverter.GetBytes(IPAddress.HostToNetworkOrder(opcode)).CopyTo(packetBytes, 4);

			///包体存入消息包，占用位置（8...）,从8开始向后存到底
			packetBody.CopyTo(packetBytes, 8);

			this.packetParser.SendBytes(packetBytes);
		}
        #endregion

        #region
        public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();

			foreach (Action<IXfsResponse> action in this.requestCallback.Values.ToArray())
			{
                action.Invoke(new XfsResponseMessage());
            }

			this.packetParser.ReadCallback -= this.OnRead;

			this.requestCallback.Clear();

			if ((this.Parent as XfsTcpServer) != null)
			{
				if ((this.Parent as XfsTcpServer).TPeers.Count > 0)
				{
					if ((this.Parent as XfsTcpServer).TPeers.TryGetValue(this.InstanceId, out XfsSession peer))
					{
						(this.Parent as XfsTcpServer).TPeers.Remove(this.InstanceId);
					}
				}
			}

			this.Socket.Close();
			this.IsRunning = false;
            this.packetParser.Dispose();

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " 一个客户端:已经中断连接, TPeers: " + (this.Parent as XfsTcpServer).TPeers.Count);
		}
        #endregion

    }
}
