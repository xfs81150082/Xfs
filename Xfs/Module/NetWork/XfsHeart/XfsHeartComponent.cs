using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsHeartComponent : XfsComponent
    {
        public bool IsPeer { get; set; } = true;
        public XfsSenceType SenceType { get; set; }
        public int CdCount { get; set; } = 0;
        public int MaxCdCount { get; set; } = 4000;
        public bool Counting { get; set; } = true;
    }
}