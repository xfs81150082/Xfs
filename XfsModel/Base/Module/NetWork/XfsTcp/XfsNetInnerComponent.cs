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
        }
	}
	public class XfsNetInnerComponent: XfsNetWorkComponent
	{
		
	}
}