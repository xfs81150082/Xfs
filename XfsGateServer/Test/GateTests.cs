using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateServer
{
    public class GateTests : XfsEntity
    {
        public GateTests()
        {
            this.AddComponent<XfsGateTest>();
            this.AddComponent<Test1Entity>();
            this.AddComponent<Test2Entity>();



        }

    }
}
