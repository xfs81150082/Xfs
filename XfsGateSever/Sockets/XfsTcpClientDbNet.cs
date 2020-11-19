using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    public class XfsTcpClientDbNet : XfsNetWorkComponent
    {
        public override XfsSenceType SenceType => XfsSenceType.Db;
        public override bool IsServer => false;


    }
}