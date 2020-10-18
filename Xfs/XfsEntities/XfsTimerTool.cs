using System;
namespace Xfs
{
    public static class XfsTimerTool
    {
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
        }///获得服务器当前时间
        public static string IdCurrentTime()
        {
            string cuurentTime = "";
            cuurentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            return cuurentTime;
        }
    }
}