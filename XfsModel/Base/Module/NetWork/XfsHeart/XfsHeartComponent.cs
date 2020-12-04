using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsHeartComponent : XfsEntity
    {
        public int CdCount { get; set; } = 0;
        public int MaxCdCount { get; set; } = 4;
        public bool Counting { get; set; } = true;

        public int Time = 0;
        public int RecTimer = 4000;
    }
}