using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Xfs
{
	[XfsObjectSystem]
	public class SessionAwakeSystem : XfsAwakeSystem<XfsSession, XfsAChannel>
	{
		public override void Awake(XfsSession self, XfsAChannel b)
		{
			self.Awake(b);
		}
	}

	public sealed class XfsSession : XfsEntity
	{
		private static int RpcId { get; set; }
		private XfsAChannel channel;

		private readonly Dictionary<int, Action<IXfsResponse>> requestCallback = new Dictionary<int, Action<IXfsResponse>>();
		private readonly byte[] opcodeBytes = new byte[2];

        public XfsNetworkComponent Network
        {
            get
            {
                return this.GetParent<XfsNetworkComponent>();
            }
        }

        public int Error
		{
			get
			{
				return this.channel.Error;
			}
			set
			{
				this.channel.Error = value;
			}
		}

		public void Awake(XfsAChannel aChannel)
		{
			this.channel = aChannel;
			this.requestCallback.Clear();
			long id = this.Id;
			channel.ErrorCallback += (c, e) =>
			{
                this.Network.Remove(id);
            };
			channel.ReadCallback += this.OnRead;
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			//this.Network.Remove(this.Id);

			base.Dispose();

			foreach (Action<IXfsResponse> action in this.requestCallback.Values.ToArray())
			{
				//action.Invoke(new ResponseMessage { Error = this.Error });
			}

			//int error = this.channel.Error;
			//if (this.channel.Error != 0)
			//{
			//	Log.Trace($"session dispose: {this.Id} ErrorCode: {error}, please see ErrorCode.cs!");
			//}

			this.channel.Dispose();

			this.requestCallback.Clear();
		}

		public void Start()
		{
			this.channel.Start();
		}

		public IPEndPoint RemoteAddress
		{
			get
			{
				return this.channel.RemoteAddress;
			}
		}

		public XfsChannelType ChannelType
		{
			get
			{
				return this.channel.ChannelType;
			}
		}

		public MemoryStream Stream
		{
			get
			{
				return this.channel.Stream;
			}
		}

		public void OnRead(MemoryStream memoryStream)
		{
			try
			{
				this.Run(memoryStream);
			}
			catch (Exception e)
			{
				//Log.Error(e);
			}
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

            object message;
			try
			{
				XfsOpcodeTypeComponent opcodeTypeComponent = this.Network.Entity.GetComponent<XfsOpcodeTypeComponent>();
				object instance = opcodeTypeComponent.GetInstance(opcode);
				message = this.Network.MessagePacker.DeserializeFrom(instance, memoryStream);

				//if (XfsOpcodeHelper.IsNeedDebugLogMessage(opcode))
				//{
				//    Log.Msg(message);
				//}
			}
			catch (Exception e)
			{
				// 出现任何消息解析异常都要断开Session，防止客户端伪造消息
				//Log.Error($"opcode: {opcode} {this.Network.Count} {e} ");
				this.Error = XfsErrorCode.ERR_PacketParserError;
				this.Network.Remove(this.Id);
				return;
			}

            IXfsResponse response = message as IXfsResponse;
            if (response == null)
            {
                this.Network.MessageDispatcher.Dispatch(this, opcode, message);
                return;
            }

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

//			if (OpcodeHelper.IsNeedDebugLogMessage(opcode))
//			{
//#if !SERVER
//				if (OpcodeHelper.IsClientHotfixMessage(opcode))
//				{
//				}
//				else
//#endif
//				{
//					//Log.Msg(message);
//				}
//			}

			MemoryStream stream = this.Stream;

            //stream.Seek(Packet.MessageIndex, SeekOrigin.Begin);
            //stream.SetLength(Packet.MessageIndex);
            //this.Network.MessagePacker.SerializeTo(message, stream);
            //stream.Seek(0, SeekOrigin.Begin);

            //			opcodeBytes.WriteTo(0, opcode);
            //			Array.Copy(opcodeBytes, 0, stream.GetBuffer(), 0, opcodeBytes.Length);

            //#if SERVER
            //			// 如果是allserver，内部消息不走网络，直接转给session,方便调试时看到整体堆栈
            if (this.Network.SenceType == XfsSenceType.AllServer)
            {
                XfsSession session = this.Network.Entity.GetComponent<XfsNetInnerComponent>().Get(this.RemoteAddress);
                session.Run(stream);
                return;
            }
            //#endif

            this.Send(stream);
		}

		public void Send(MemoryStream stream)
		{
			channel.Send(stream);
		}
	}
}