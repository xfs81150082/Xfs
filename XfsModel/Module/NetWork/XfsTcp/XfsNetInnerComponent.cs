using System;
using System.Collections.Generic;
using System.Net;

namespace Xfs
{
	public class XfsNetInnerComponent: XfsNetWorkComponent
	{
		public readonly Dictionary<IPEndPoint, XfsSession> adressSessions = new Dictionary<IPEndPoint, XfsSession>();
		public readonly List<XfsSession> sessionList = new List<XfsSession>();


		//public XfsSession Get(IPEndPoint ipEndPoint)
		//{
		//	if (this.adressSessions.TryGetValue(ipEndPoint, out XfsSession session))
		//	{
		//		return session;
		//	}
			
		//	session = XfsEntityFactory.CreateWithParent<XfsSession>(this);


		//	this.adressSessions.Add(ipEndPoint, session);
		//	return session;
	

		/// <summary>
		/// 从地址缓存中取Session,如果没有则创建一个新的Session,并且保存到地址缓存中
		/// </summary>
		public XfsSession InnerGet()
		{
			if (this.sessionList.Count>0)
			{
				return sessionList[0];
			}

			XfsSession session = XfsEntityFactory.CreateWithParent<XfsSession>(this);
			this.sessionList.Add(session);

			return session;
		}

	}

}