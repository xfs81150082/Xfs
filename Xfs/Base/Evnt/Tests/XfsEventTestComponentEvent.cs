using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    [XfsEvent(XfsEventIdType.EventTest)]
    public class XfsEventTestComponentEvent : XfsAEvent<XfsEventTestComponent>
    { 
        public override void Run(XfsEventTestComponent a)
        {
            Console.WriteLine(XfsTimeHelper.CurrentTime() + " EventTest: " + a.EventTest);
        }
    }
}
