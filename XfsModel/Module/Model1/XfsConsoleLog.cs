using System;
using System.IO;
namespace Xfs
{
    public static class XfsConsoleLog
    {
        private static string consolePath = AppDomain.CurrentDomain.BaseDirectory + "log.txt";
        //private static string logpath = AppDomain.CurrentDomain.BaseDirectory + "TumoLog/log.txt";
        /// <summary>
        /// 用于控制台
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLine(string message)
        {
            //打开文件，如果文件不存在，则创建一个文件
            string path = consolePath;
            if (!File.Exists(path))
            {
                using (File.Create(path)) { }
            }
            //打开文件，如果文件大于2M，则修改文件名保存备份
            FileInfo fileinfo = new FileInfo(path);
            if (fileinfo.Length > 1024 * 1024 * 2)
            {
                File.Move(path, AppDomain.CurrentDomain.BaseDirectory + XfsTimeHelper.CurrentTime() + "log.txt");
                if (!File.Exists(path))
                {
                    using (File.Create(path)) { }
                }
            }
            //在文件上写入文本文字
            StreamWriter sw2 = File.AppendText(path);
            sw2.WriteLine(XfsTimeHelper.CurrentTime() + " " + message);
            sw2.Close();
        }    
    }
}
