using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsConsoleClient
{
    public class XfsTcpClientDbNet : XfsTcpClient
    {
        public override XfsSenceType SenceType => XfsSenceType.Db;
        

    }
}