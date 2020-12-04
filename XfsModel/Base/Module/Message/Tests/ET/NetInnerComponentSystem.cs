//using System.Net;
//using ETModel;

//namespace Xfs
//{
//	[XfsObjectSystem]
//	public class XfsNetInnerComponentAwakeSystem : XfsAwakeSystem<XfsNetInnerComponent>
//	{
//		public override void Awake(XfsNetInnerComponent self)
//		{
//			self.Awake();
//		}
//	}

//	[XfsObjectSystem]
//	public class XfsNetInnerComponentAwake1System : XfsAwakeSystem<XfsNetInnerComponent, string>
//	{
//		public override void Awake(XfsNetInnerComponent self, string a)
//		{
//			self.Awake(a);
//		}
//	}
	
//	[XfsObjectSystem]
//	public class XfsNetInnerComponentLoadSystem : XfsLoadSystem<XfsNetInnerComponent>
//	{
//		public override void Load(XfsNetInnerComponent self)
//		{
//			//self.MessagePacker = new XfsMongoPacker();
//			self.MessageDispatcher = new InnerMessageDispatcher();
//		}
//	}

//	[XfsObjectSystem]
//	public class XfsNetInnerComponentUpdateSystem : XfsUpdateSystem<XfsNetInnerComponent>
//	{
//		public override void Update(XfsNetInnerComponent self)
//		{
//			self.Update();
//		}
//	}

//	public static class XfsNetInnerComponentHelper
//	{
//		public static void Awake(this XfsNetInnerComponent self)
//		{
//			self.Awake(XfsNetworkProtocol.TCP, XfsPacket.PacketSizeLength4);
//			//self.MessagePacker = new XfsXfsMongoPacker();
//			self.MessageDispatcher = new InnerMessageDispatcher();
//			self.SenceType = XfsStartConfigComponent.Instance.StartConfig.SenceType;
//		}

//		public static void Awake(this XfsNetInnerComponent self, string address)
//		{
//			self.Awake(XfsNetworkProtocol.TCP, address, XfsPacket.PacketSizeLength4);
//			//self.MessagePacker = new XfsMongoPacker();
//			self.MessageDispatcher = new InnerMessageDispatcher();
//			self.SenceType = XfsStartConfigComponent.Instance.StartConfig.SenceType;
//		}

//		public static void Update(this XfsNetInnerComponent self)
//		{
//			self.Update();
//		}
//	}
//}