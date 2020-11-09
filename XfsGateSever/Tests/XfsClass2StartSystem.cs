using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    [XfsObjectSystem]
    public class XfsClass2StartSystem : XfsStartSystem<Class2>
    {
        public override void Start(Class2 self)
        {
            Console.WriteLine(XfsTimerTool.CurrentTime() + " Class2 Start 运行成功: " + self.test2);
        }
    }
}
