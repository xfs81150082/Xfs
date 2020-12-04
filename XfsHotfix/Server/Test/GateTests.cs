using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace Xfs
{
    public class GateTests : XfsEntity
    {
        public List<long> ids = new List<long>();
        public GateTests()
        {
            this.AddComponent<XfsGateTest>();
            this.AddComponent<Test1Entity>();
            this.AddComponent<Test2Entity>();

        }

    }
}
