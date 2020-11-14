using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Xfs
{
	public abstract class XfsNetworkComponent : XfsComponent
	{
		public XfsSenceType SenceType;
		
		protected XfsAService Service;

		private readonly Dictionary<long, XfsSession> sessions = new Dictionary<long, XfsSession>();

		public IXfsMessagePacker MessagePacker { get; set; }

		public IXfsMessageDispatcher MessageDispatcher { get; set; }

		public void Awake(XfsNetworkProtocol protocol, int packetSize = XfsPacket.PacketSizeLength2)
		{
			switch (protocol)
			{
				case XfsNetworkProtocol.KCP:
					//this.Service = new XfsKService() { Parent = this };
					break;
				case XfsNetworkProtocol.TCP:
					this.Service = new XfsTService() { Parent = this };
					break;
				case XfsNetworkProtocol.WebSocket:
					//this.Service = new XfsWService() { Parent = this };
					break;
			}
		}

		public void Awake(XfsNetworkProtocol protocol, string address, int packetSize = XfsPacket.PacketSizeLength2)
		{
			try
			{
				IPEndPoint ipEndPoint;
				switch (protocol)
				{
					case XfsNetworkProtocol.KCP:
						//ipEndPoint = XfsNetworkHelper.ToIPEndPoint(address);
						//this.Service = new XfsKService(ipEndPoint, this.OnAccept) { Parent = this };
						break;
					case XfsNetworkProtocol.TCP:
						//ipEndPoint = XfsNetworkHelper.ToIPEndPoint(address);
						//this.Service = new XfsTService(packetSize, ipEndPoint, this.OnAccept) { Parent = this };
						break;
					case XfsNetworkProtocol.WebSocket:
						string[] prefixs = address.Split(';');
						//this.Service = new XfsWService(prefixs, this.OnAccept) { Parent = this };
						break;
				}
			}
			catch (Exception e)
			{
				throw new Exception($"NetworkComponent Awake Error {address}", e);
			}
		}

		public int Count
		{
			get { return this.sessions.Count; }
		}

		public void OnAccept(XfsAChannel channel)
		{
			XfsSession session = XfsComponentFactory.CreateWithParent<XfsSession, XfsAChannel>(this, channel);
			this.sessions.Add(session.Id, session);
			session.Start();
		}

		public virtual void Remove(long id)
		{
			XfsSession session;
			if (!this.sessions.TryGetValue(id, out session))
			{
				return;
			}
			this.sessions.Remove(id);
			session.Dispose();
		}

		public XfsSession Get(long id)
		{
			XfsSession session;
			this.sessions.TryGetValue(id, out session);
			return session;
		}

		/// <summary>
		/// 创建一个新Session
		/// </summary>
		public XfsSession Create(IPEndPoint ipEndPoint)
		{
			XfsAChannel channel = this.Service.ConnectChannel(ipEndPoint);
			XfsSession session = XfsComponentFactory.CreateWithParent<XfsSession, XfsAChannel>(this, channel);
			this.sessions.Add(session.Id, session);
			session.Start();
			return session;
		}
		
		/// <summary>
		/// 创建一个新Session
		/// </summary>
		public XfsSession Create(string address)
		{
			XfsAChannel channel = this.Service.ConnectChannel(address);
			XfsSession session = XfsComponentFactory.CreateWithParent<XfsSession, XfsAChannel>(this, channel);
			this.sessions.Add(session.Id, session);
			session.Start();
			return session;
		}

		public void Update()
		{
			if (this.Service == null)
			{
				return;
			}
			this.Service.Update();
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();

			foreach (XfsSession session in this.sessions.Values.ToArray())
			{
				session.Dispose();
			}

			this.Service.Dispose();
		}
	}
}