using System;
using System.Collections.Generic;
using System.Linq;
using Xfs;
using System.Threading.Tasks;
using XfsGateSever;

namespace XfsGateSever
{
    [XfsEvent(XfsEventIdType.NumbericChange)]
    public class XfsClass4EventSystem : XfsAEvent<Class4>
    {      
        public override void Run(Class4 a)
        {
            throw new NotImplementedException();
        }
    }
}
