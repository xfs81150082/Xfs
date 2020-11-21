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
        #region 自定义属性
        public XfsPacketParser packetParser { get; private set; }	
		public XfsNetWorkComponent Network
		{
			get
			{
				return this.GetParent<XfsNetWorkComponent>();
			}
		}
		public Socket Socket { get; set; }                ///创建一个套接字，用于储藏代理服务端套接字，与客户端通信///客户端Socket 
		public IPEndPoint RemoteAddress
		{
			get
			{
				if(Socket == null || this.IsRunning == false)
                {
					return null;
                }
				return (IPEndPoint)this.Socket.RemoteEndPoint;
			}
		}
		public bool IsRunning { get; set; }
		public bool IsServer { get; set; }
		public XfsSenceType SenceType { get; set; }
		private static int RpcId { get; set; }
 		public readonly Dictionary<int, Action<IXfsResponse>> requestCallback = new Dictionary<int, Action<IXfsResponse>>();
		public void Awake()
		{
			this.requestCallback.Clear();
			this.AddComponent<XfsHeartComponent>();
            this.packetParser = XfsComponentFactory.CreateWithParent<XfsPacketParser>(this);
			this.packetParser.ReadCallback += this.OnRead;
		}
        #endregion

        #region 接收Socket信息        
        public void BeginReceiveMessage(Socket socket)
		{
			this.Socket = socket;
			packetParser.BeginReceiveMessage(socket);
			this.OnConnect();


        }
		public void OnConnect()
		{
			///显示与客户端连接			
			Console.WriteLine(XfsTimeHelper.CurrentTime() + " 客户端连接成功 Is Peer: " + this.IsServer + " : " + this.Socket.RemoteEndPoint);
		}
		#endregion

		#region 接收包裹信息
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
				if (this.Network != null)
				{
					if (this.Network.Sessions.TryGetValue(this.InstanceId, out XfsSession session))
					{
						this.Network.Sessions.Remove(this.InstanceId);
					}
				}
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
                throw new Exception($"not found rpc, response message: {response}");
            }
            this.requestCallback.Remove(response.RpcId);
            action(response);
		}
		#endregion

		#region Call Send
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
						Console.WriteLine(XfsTimeHelper.CurrentTime() + " : " + response.Message);
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

		#region Dispose
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

			if (this.Network != null)
			{
				if (this.Network.Sessions.Count > 0)
				{
					if (this.Network.Sessions.TryGetValue(this.InstanceId, out XfsSession peer))
					{
						this.Network.Sessions.Remove(this.InstanceId);
					}
				}
			}

			this.Socket.Close();
			this.IsRunning = false;
            this.packetParser.Dispose();

            Console.WriteLine(XfsTimeHelper.CurrentTime() + " 一个Session : 已经中断连接, Sessions: " + this.Network.Sessions.Count);
		}
        #endregion

    }
}
