using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsServer
{
    public class XfsMysqlHandler : XfsEntity
    {
        private static XfsMysqlHandler _instance;
        public static XfsMysqlHandler Instance { get => _instance;  }
        public override void XfsAwake()
        {
            base.XfsAwake();
            _instance = this;
            this.AddComponent(new XfsUserMysql());
            this.AddComponent(new XfsBookerMysql());

        }
    }
}
