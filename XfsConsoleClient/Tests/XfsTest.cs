using System;
using System.Threading;
using Xfs;
namespace XfsConsoleClient
{
    public class XfsTest : XfsEntity
    {
        public string longin = "2020-10-18";
        public string par { get; set; }

        public bool IsUserLogin = false;

        public float time = 0;

        public float restime = 3000;

        public string call = "发送测试回调请求-20201106";
       
    }
}