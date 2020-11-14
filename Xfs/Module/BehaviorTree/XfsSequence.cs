using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    public class XfsSequence : XfsBranch
    {
        public override XfsBtState Tick()
        {
            var childState = children[activeChild].Tick();
            switch (childState)
            {
                case XfsBtState.Success:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return XfsBtState.Success;
                    }
                    else
                    {
                        return XfsBtState.Continue;
                    }
                case XfsBtState.Failure:
                    activeChild = 0;
                    return XfsBtState.Failure;
                case XfsBtState.Continue:
                    return XfsBtState.Continue;
                case XfsBtState.Abort:
                    activeChild = 0;
                    return XfsBtState.Abort;
            }
            throw new System.Exception("This should never happen, but clearly it has.");
        }
    }

}
