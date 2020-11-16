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
		public XfsPacketParser tcpSession { get; set; }
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
		public Queue<XfsParameter> RecvParameters { get; set; } = new Queue<XfsParameter>();
		public Queue<XfsParameter> SendParameters { get; set; } = new Queue<XfsParameter>();

		private static int RpcId { get; set; }
		private readonly Dictionary<int, Action<XfsParameter>> parameterCallback = new Dictionary<int, Action<XfsParameter>>();

		public void Awake()
		{
			this.requestCallback.Clear();
			this.AddComponent<XfsHeartComponent>();
            this.tcpSession = XfsComponentFactory.CreateWithParent<XfsPacketParser>(this);
			this.tcpSession.ReadCallback += this.RecvBufferBytes;
		}	
		#region
		/// 接收Socket信息
		public void BeginReceiveMessage(Socket socket)
		{
			this.Socket = socket;
			tcpSession.BeginReceiveMessage(socket);
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
        public void RecvBufferBytes(object obj, byte[] HeadBytes, byte[] BodyBytes)
		{
			//base.RecvBufferBytes(obj, HeadBytes, BodyBytes);

			///一个包身BodyBytes消息包接收完毕，解析消息包
			string mvcString = Encoding.UTF8.GetString(BodyBytes, 0, BodyBytes.Length);

			Console.WriteLine(XfsTimeHelper.CurrentTime() + " Recv HeadBytes {0} Bytes, BodyBytes {1} Bytes. ThreadId:{2} .", HeadBytes.Length, BodyBytes.Length, Thread.CurrentThread.ManagedThreadId);

			HeadBytes = null;
			BodyBytes = null;

			XfsParameter parameter = XfsJsonHelper.ToObject<XfsParameter>(mvcString);
			///这个方法用来处理参数Mvc，并让结果给客户端响应（当客户端发起请求时调用）
			this.RecvBodyBytesParameter(this, parameter);
		}

		public void RecvBodyBytesParameter(object obj, XfsParameter parameter)
		{
			///将字符串string,用json反序列化转换成MvcParameter参数
			if (parameter.TenCode == TenCode.Zero)
			{
				this.GetComponent<XfsHeartComponent>().CdCount = 0;

				Console.WriteLine("{0} 服务端心跳包 {1} 接收成功", XfsTimeHelper.CurrentTime(), Socket.RemoteEndPoint);

				return;
			}
			///将MvcParameter参数分别列队并处理

			Action<XfsParameter> action;
			if (this.parameterCallback.TryGetValue(parameter.RpcId, out action))
			{
				this.parameterCallback.Remove(parameter.RpcId);
				action(parameter);
				return;
			}

			parameter.PeerIds.Add(this.InstanceId);

			this.Recv(parameter);
		}

        public void Recv(XfsParameter parameter)
		{
			this.RecvParameters.Enqueue(parameter);
			this.OnRecvParameters();
		}

		void OnRecvParameters()
		{
			try
			{
				while (this.RecvParameters.Count > 0)
				{
					XfsParameter parameter = this.RecvParameters.Dequeue();






					XfsHandler handler = null;
					XfsSockets.XfsHandlers.TryGetValue(this.SenceType, out handler);
					if (handler != null)
					{
						handler.Recv(this, parameter);
						Console.WriteLine(XfsTimeHelper.CurrentTime() + " RecvParameters: " + this.RecvParameters.Count);
					}
					else
					{
						Console.WriteLine(XfsTimeHelper.CurrentTime() + " XfsHandler is null.");
						continue;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(XfsTimeHelper.CurrentTime() + ex.Message);
			}
		}
		#endregion

		#region
		///发送信息，但有回复
		public XfsTask<XfsParameter> Call(XfsParameter parameter)
		{
			int rpcId = ++RpcId;
			var tcs = new XfsTaskCompletionSource<XfsParameter>();

			this.parameterCallback[rpcId] = (response) =>
			{
				try
				{
					//if (XfsErrorCode.IsRpcNeedThrowException(response.Error))
					//{
					//	//throw new RpcException(response.Error, response.Message);
					//}

					tcs.SetResult(response);
				}
				catch (Exception e)
				{
					tcs.SetException(new Exception($"Rpc Error: {parameterCallback.GetType().FullName}", e));
				}
			};

			parameter.RpcId = rpcId;

			this.Send(parameter);

			return tcs.Task;
		}
		public void Reply(XfsParameter  message)
		{
			if (this.IsDisposed)
			{
				throw new Exception("session已经被Dispose了");
			}

			this.Send(message);
		}
		public void Send(XfsParameter mvc)
		{
			this.SendParameters.Enqueue(mvc);
			OnSendMvcParameters();
		}

		///处理发送参数信息
		void OnSendMvcParameters()
		{
			try
			{
				while (this.SendParameters.Count > 0)
				{
					XfsParameter response = SendParameters.Dequeue();

					///用Json将参数（MvcParameter）,序列化转换成字符串（string）
					string mvcJsons = XfsJsonHelper.ToString<XfsParameter>(response);

					this.tcpSession.SendString(mvcJsons);

				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(XfsTimeHelper.CurrentTime() + " OnSendMvcParameters143: " + ex.Message);
			}
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
				//action.Invoke(new ResponseMessage { Error = this.Error });
			}

			this.tcpSession.ReadCallback -= this.RecvBufferBytes;

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
			//this.tcpSession.Dispose();

			Console.WriteLine(XfsTimeHelper.CurrentTime() + " 一个客户端:已经中断连接, TPeers: " + (this.Parent as XfsTcpServer).TPeers.Count);
		}
        #endregion

        #region
 		private readonly Dictionary<int, Action<IXfsResponse>> requestCallback = new Dictionary<int, Action<IXfsResponse>>();
		private readonly byte[] opcodeBytes = new byte[4];
		public void Start()
		{
			//this.channel.Start();
		}

		private void Run(MemoryStream memoryStream)
		{
			memoryStream.Seek(XfsPacket.MessageIndex, SeekOrigin.Begin);
			ushort opcode = BitConverter.ToUInt16(memoryStream.GetBuffer(), XfsPacket.OpcodeIndex);

			//#if !SERVER
			//if (OpcodeHelper.IsClientHotfixMessage(opcode))
			//{
			//    this.GetComponent<SessionCallbackComponent>().MessageCallback.Invoke(this, opcode, memoryStream);
			//    return;
			//}
			//#endif

			object message = null;
			try
			{
				XfsOpcodeTypeComponent opcodeTypeComponent = this.Network.Entity.GetComponent<XfsOpcodeTypeComponent>();
				object instance = opcodeTypeComponent.GetInstance(opcode);
				//message = this.Network.MessagePacker.DeserializeFrom(instance, memoryStream);

				//if (XfsOpcodeHelper.IsNeedDebugLogMessage(opcode))
				//{
				//    Log.Msg(message);
				//}
			}
			catch (Exception e)
			{
				// 出现任何消息解析异常都要断开Session，防止客户端伪造消息
				//Log.Error($"opcode: {opcode} {this.Network.Count} {e} ");
				//this.Error = XfsErrorCode.ERR_PacketParserError;
				//this.Network.Remove(this.Id);
				return;
			}

            IXfsResponse response = message as IXfsResponse;
            //if (response == null)
            //{
            //    this.Network.MessageDispatcher.Dispatch(this, opcode, message);
            //    return;
            //}

            Action<IXfsResponse> action;
            if (!this.requestCallback.TryGetValue(response.RpcId, out action))
            {
                throw new Exception($"not found rpc, response message: {/*StringHelper.MessageToStr(response)*/ 0}");
            }
            this.requestCallback.Remove(response.RpcId);

            action(response);
        }

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
						//throw new RpcException(response.Error, response.Message);
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
			//OpcodeTypeComponent opcodeTypeComponent = this.Network.Entity.GetComponent<OpcodeTypeComponent>();
			//ushort opcode = opcodeTypeComponent.GetOpcode(message.GetType());

			//Send(opcode, message);
		}

		public void Send(ushort opcode, object message)
		{
			if (this.IsDisposed)
			{
				throw new Exception("session已经被Dispose了");
			}

		

			//this.Send(stream);
		}

		public void Send(MemoryStream stream)
		{
			//channel.Send(stream);
		}
        #endregion



    }
}
