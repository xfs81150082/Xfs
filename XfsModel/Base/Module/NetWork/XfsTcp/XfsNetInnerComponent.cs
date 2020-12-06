using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Xfs
{
	[XfsObjectSystem]
	public class XfsNetInnerComponentAwakeSystem : XfsAwakeSystem<XfsNetInnerComponent>
	{
		public override void Awake(XfsNetInnerComponent self)
		{
            self.MessageDispatcher = new XfsInnerMessageDispatcher();
			self.Session = XfsEntityFactory.CreateWithParent<XfsSession>(self);
        }
	}
	public class XfsNetInnerComponent: XfsNetWorkComponent
	{
		//public readonly Dictionary<string, XfsSession> adressSessions = new Dictionary<string, XfsSession>();
		//public XfsSession Get(string addr)
		//{
		//	if (this.adressSessions.TryGetValue(addr, out XfsSession session))
		//	{
		//		return session;
		//	}
		//	session = XfsEntityFactory.CreateWithParent<XfsSession>(this);

		//	this.adressSessions.Add(addr, session);

		//	//// 内网connect连接，一分钟检查一次，10分钟没有收到发送消息则断开
		//	//session.AddComponent<SessionIdleCheckerComponent, int, int, int>(60 * 1000, int.MaxValue, 60 * 1000);

		//	return session;
		//}

		public XfsSession GetSession()
		{
			if (this.Session == null)
			{
				XfsSession session = XfsEntityFactory.CreateWithParent<XfsSession>(this);
				this.Session = session;
			}

			return this.Session;
		}


	}
}