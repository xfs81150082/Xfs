using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    public class XfsMysqlHandler : XfsEntity
    {
        private static XfsMysqlHandler _instance;
        public static XfsMysqlHandler Instance { get => _instance;  }
       

    }
}
