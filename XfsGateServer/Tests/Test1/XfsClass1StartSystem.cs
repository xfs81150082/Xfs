using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    [XfsObjectSystem]
    public class XfsClass1StartSystem : XfsStartSystem<Class1>
    {
        public override void Start(Class1 self)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " Class1 Start 运行成功: " + self.test1);
        }
    }
}
