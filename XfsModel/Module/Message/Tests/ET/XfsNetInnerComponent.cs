using System.Collections.Generic;
using System.Net;

namespace Xfs
{
	public class XfsNetInnerComponent: XfsNetworkComponent
	{
		public readonly Dictionary<IPEndPoint, XfsSession> adressSessions = new Dictionary<IPEndPoint, XfsSession>();

		public override void Remove(long id)
		{
			XfsSession session = this.Get(id);
			if (session == null)
			{
				return;
			}
			this.adressSessions.Remove(session.RemoteAddress);

			base.Remove(id);
		}

		/// <summary>
		/// 从地址缓存中取Session,如果没有则创建一个新的Session,并且保存到地址缓存中
		/// </summary>
		public XfsSession Get(IPEndPoint ipEndPoint)
		{
			if (this.adressSessions.TryGetValue(ipEndPoint, out XfsSession session))
			{
				return session;
			}
			
			session = this.Create(ipEndPoint);

			this.adressSessions.Add(ipEndPoint, session);
			return session;
		}
	}

}