using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public static class XfsTimeHelper
    {
		private static readonly long epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
		/// <summary>
		/// 客户端时间
		/// </summary>
		/// <returns></returns>
		public static long ClientNow()
		{
			return (DateTime.UtcNow.Ticks - epoch) / 10000;
		}
		public static long ClientNowSeconds()
		{
			return (DateTime.UtcNow.Ticks - epoch) / 10000000;
		}
		public static long Now()
		{
			return ClientNow();
		}
        ///获得服务器当前时间
        public static string CurrentMoveTime()
        {
            string cuurentTime = "";
            cuurentTime = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            return cuurentTime;
        }
        ///获得服务器当前时间
        public static string CurrentTime()
        {
            string cuurentTime = "";
            cuurentTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            return cuurentTime;
        }
        ///获得服务器当前时间
        public static string IdCurrentTime()
        {
            string cuurentTime = "";
            cuurentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            return cuurentTime;
        }
    }
}