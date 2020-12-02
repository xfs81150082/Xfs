

//namespace Xfs
//{
//	[XfsObjectSystem]
//	public class NetOuterComponentAwakeSystem : XfsAwakeSystem<XfsNetOuterComponent>
//	{
//		public override void Awake(XfsNetOuterComponent self)
//		{
//			self.Awake(self.Protocol);
//			//self.MessagePacker = new XfsProtobufPacker();
//			self.MessageDispatcher = new XfsOuterMessageDispatcher();
//		}
//	}

//	[XfsObjectSystem]
//	public class NetOuterComponentAwake1System : XfsAwakeSystem<XfsNetOuterComponent, string>
//	{
//		public override void Awake(XfsNetOuterComponent self, string address)
//		{
//			self.Awake(self.Protocol, address);
//			//self.MessagePacker = new XfsProtobufPacker();
//			self.MessageDispatcher = new XfsOuterMessageDispatcher();
//		}
//	}
	
//	[XfsObjectSystem]
//	public class NetOuterComponentLoadSystem : XfsLoadSystem<XfsNetOuterComponent>
//	{
//		public override void Load(XfsNetOuterComponent self)
//		{
//			//self.MessagePacker = new XfsProtobufPacker();
//			self.MessageDispatcher = new XfsOuterMessageDispatcher();
//		}
//	}
	
//	[XfsObjectSystem]
//	public class NetOuterComponentUpdateSystem : XfsUpdateSystem<XfsNetOuterComponent>
//	{
//		public override void Update(XfsNetOuterComponent self)
//		{
//			self.Update();
//		}
//	}
//}