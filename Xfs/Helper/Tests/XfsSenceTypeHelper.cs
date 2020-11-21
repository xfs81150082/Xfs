using System;
using System.Collections.Generic;

namespace Xfs
{
	//[Flags]
	//public enum XfsAppType
	//{
	//	None = 0,
	//	Manager = 1,
	//	Realm = 1 << 1,
	//	Gate = 1 << 2,
	//	Http = 1 << 3,
	//	DB = 1 << 4,
	//	Location = 1 << 5,
	//	Map = 1 << 6,

	//	BenchmarkWebsocketServer = 1 << 26,
	//	BenchmarkWebsocketClient = 1 << 27,
	//	Robot = 1 << 28,
	//	Benchmark = 1 << 29,
	//	// 客户端Hotfix层
	//	ClientH = 1 << 30,
	//	// 客户端Model层
	//	ClientM = 1 << 31,
	
	//	Client = 1 << 51,

	//	// 7
	//	AllServer = Manager | Realm | Gate | Http | DB | Location | Map | BenchmarkWebsocketServer
	//}

	//public static class XfsAppTypeHelper
	//{
	//	public static List<XfsAppType> GetServerTypes()
	//	{
	//		List<XfsAppType> appTypes = new List<XfsAppType> { XfsAppType.Manager, XfsAppType.Realm, XfsAppType.Gate };
	//		return appTypes;
	//	}

	//	public static bool Is(this XfsAppType a, XfsAppType b)
	//	{
	//		if ((a & b) != 0)
	//		{
	//			return true;
	//		}
	//		return false;
	//	}
	//}

	public static class XfsSenceTypeHelper
	{
		public static List<XfsSenceType> GetServerTypes()
		{
			List<XfsSenceType> appTypes = new List<XfsSenceType> { XfsSenceType.Manager, XfsSenceType.Realm, XfsSenceType.Gate };
			return appTypes;
		}

		public static bool Is(this XfsSenceType a, XfsSenceType b)
		{
			if ((a & b) != 0)
			{
				return true;
			}
			return false;
		}
	}

}