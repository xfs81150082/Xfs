using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xfs;

namespace XfsGateSever
{
    [XfsObjectSystem]
    public class XfsClass3UpdateSystem : XfsUpdateSystem<Class3>
    {
        public override void Update(Class3 self)
        {
            //Console.WriteLine(XfsTimerTool.CurrentTime() + " Class3 Update: " + self.test3);
        }
    }
}
