using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    public abstract class XfsBtNode : XfsEntity
    {
        public abstract XfsBtState Tick();
    }
}
