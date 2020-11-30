using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
	public enum XfsNetworkProtocol
	{
		KCP,
		TCP,
		WebSocket,
	}
	public abstract class XfsAService : XfsEntity
	{
		public abstract XfsAChannel GetChannel(long id);

		private Action<XfsAChannel> acceptCallback;

		public event Action<XfsAChannel> AcceptCallback
		{
			add
			{
				this.acceptCallback += value;
			}
			remove
			{
				this.acceptCallback -= value;
			}
		}

		protected void OnAccept(XfsAChannel channel)
		{
			this.acceptCallback.Invoke(channel);
		}

		public abstract XfsAChannel ConnectChannel(IPEndPoint ipEndPoint);

		public abstract XfsAChannel ConnectChannel(string address);

		public abstract void Remove(long channelId);

		public abstract void Update();


	}
}
