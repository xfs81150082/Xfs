using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    //根节点，循环执行
    public class XfsRoot : XfsBranch
    {
        public bool isTerminated = false;

        public override XfsBtState Tick()
        {
            if (isTerminated) { return XfsBtState.Abort; }

            while (true)
            {
                switch (children[activeChild].Tick())
                {
                    case XfsBtState.Continue:
                        return XfsBtState.Continue;
                    case XfsBtState.Abort:
                        isTerminated = true;
                        return XfsBtState.Abort;
                    default:
                        activeChild++;
                        if (activeChild == children.Count)
                        {
                            activeChild = 0;
                            return XfsBtState.Success;
                        }
                        continue;
                }
            }
        }


    }
}