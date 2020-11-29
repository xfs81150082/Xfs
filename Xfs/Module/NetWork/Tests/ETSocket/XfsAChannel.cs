using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
	public enum XfsChannelType
	{
		Connect,
		Accept,
	}
	public abstract class XfsAChannel : XfsEntity
	{
		public XfsChannelType ChannelType { get; }

		public XfsAService Service { get; }

		public abstract MemoryStream Stream { get; }

		public int Error { get; set; }

		public IPEndPoint RemoteAddress { get; protected set; }

		private Action<XfsAChannel, int> errorCallback;

		public event Action<XfsAChannel, int> ErrorCallback
		{
			add
			{
				this.errorCallback += value;
			}
			remove
			{
				this.errorCallback -= value;
			}
		}

		private Action<MemoryStream> readCallback;

		public event Action<MemoryStream> ReadCallback
		{
			add
			{
				this.readCallback += value;
			}
			remove
			{
				this.readCallback -= value;
			}
		}

		protected void OnRead(MemoryStream memoryStream)
		{
			this.readCallback.Invoke(memoryStream);
		}

		protected void OnError(int e)
		{
			this.Error = e;
			this.errorCallback?.Invoke(this, e);
		}

		protected XfsAChannel(XfsAService service, XfsChannelType channelType)
		{
			this.Id = XfsIdGeneraterHelper.GenerateId();
			this.ChannelType = channelType;
			this.Service = service;
		}

		public abstract void Start();

		public abstract void Send(MemoryStream stream);

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();

			this.Service.Remove(this.Id);
		}
	}


}
