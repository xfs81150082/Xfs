using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    public class TestEntity1 : XfsEntity
    {
        public TestEntity1()
        {
            this.AddComponent<Class1>();
            this.AddComponent<Class2>();
            this.AddComponent<Class3>();
        }


    }
}
