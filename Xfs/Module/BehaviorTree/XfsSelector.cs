using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    public class XfsSelector : XfsBranch
    {
        public XfsSelector(bool shuffle)
        {
            if (shuffle)
            {
                var n = children.Count;
                while (n > 1)
                {
                    n--;
                    Random rd = new Random();
                    var k = rd.Next(0, n + 1);
                    //var k = Mathf.FloorToInt(Random.value * (n + 1));
                    var value = children[k];
                    children[k] = children[n];
                    children[n] = value;
                }
            }
        }

        public override XfsBtState Tick()
        {
            var childState = children[activeChild].Tick();
            switch (childState)
            {
                case XfsBtState.Success:
                    activeChild = 0;
                    return XfsBtState.Success;
                case XfsBtState.Failure:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return XfsBtState.Failure;
                    }
                    else
                    {
                        return XfsBtState.Continue;
                    }
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
