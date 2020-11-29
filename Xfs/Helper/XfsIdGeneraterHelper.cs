using System;

namespace Xfs
{
    public static class XfsIdGeneraterHelper
    {
        static int idCount = 1400;
        public static string GetId()
        {
            string tmId = "";
            idCount += 1;
            if (idCount > 4000)
            {
                idCount = 1400;
            }
            tmId = XfsTimeHelper.IdCurrentTime() + idCount.ToString();
            return tmId;
        }
		
		private static long instanceIdGenerator;
		private static long appId;
		public static long AppId
		{
			set
			{
				appId = value;
				instanceIdGenerator = appId << 48;
			}
		}
		private static ushort value;
		public static long GenerateId()
		{
			long time = XfsTimeHelper.ClientNowSeconds();

			return (appId << 48) + (time << 16) + ++value;
		}
		public static long GenerateInstanceId()
		{
			return ++instanceIdGenerator;
		}
		public static int GetAppId(long v)
		{
			return (int)(v >> 48);
		}

	}
}