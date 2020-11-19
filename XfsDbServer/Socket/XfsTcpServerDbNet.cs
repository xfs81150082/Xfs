using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsDbServer
{
    public class XfsTcpServerDbNet : XfsNetWorkComponent
    {
        public override XfsSenceType SenceType => XfsSenceType.Db;
        public override bool IsServer => true;
        public XfsTcpServerDbNet() { }
     
     
    }
}
