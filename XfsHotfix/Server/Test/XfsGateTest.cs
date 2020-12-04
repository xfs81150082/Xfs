using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace Xfs
{
    public class XfsGateTest : XfsEntity
    {
        public string longin = "2020-10-18";
        public string par { get; set; }

        public bool IsUserLogin = false;

        public float time = 0;

        public float restime = 6000;

        public string call = "发送测试回调请求-20201106";

    }
}
