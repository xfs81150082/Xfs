using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    public abstract class XfsBtNode : XfsComponent
    {
        public abstract XfsBtState Tick();
    }
}
