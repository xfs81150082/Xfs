

namespace Xfs
{
	[XfsObjectSystem]
	public class XfsNetOuterComponentAwakeSystem : XfsAwakeSystem<XfsNetOuterComponent>
	{
		public override void Awake(XfsNetOuterComponent self)
		{
			self.Awake(self.Protocol);
			//self.MessagePacker = new XfsProtobufPacker();
			self.MessageDispatcher = new OuterMessageDispatcher();
		}
	}

	[XfsObjectSystem]
	public class XfsNetOuterComponentAwake1System : XfsAwakeSystem<XfsNetOuterComponent, string>
	{
		public override void Awake(XfsNetOuterComponent self, string address)
		{
			self.Awake(self.Protocol, address);
			//self.MessagePacker = new XfsProtobufPacker();
			self.MessageDispatcher = new OuterMessageDispatcher();
		}
	}
	
	[XfsObjectSystem]
	public class XfsNetOuterComponentLoadSystem : XfsLoadSystem<XfsNetOuterComponent>
	{
		public override void Load(XfsNetOuterComponent self)
		{
			//self.MessagePacker = new XfsProtobufPacker();
			self.MessageDispatcher = new OuterMessageDispatcher();
		}
	}
	
	[XfsObjectSystem]
	public class XfsNetOuterComponentUpdateSystem : XfsUpdateSystem<XfsNetOuterComponent>
	{
		public override void Update(XfsNetOuterComponent self)
		{
			self.Update();
		}
	}
}