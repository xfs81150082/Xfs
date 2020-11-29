using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    [XfsObjectSystem]
    public class ConditionAwakeSystem : XfsAwakeSystem<XfsCondition, Func<bool>>
    {
        public override void Awake(XfsCondition self, Func<bool> a)
        {
            self.Awake(a);
        }
    }

    public class XfsCondition : XfsBtNode
    {
        public Func<bool> fn;

        public void Awake(Func<bool> fn)
        {
            this.fn = fn;
        }

        public XfsCondition(Func<bool> fn)
        {
            this.fn = fn;
        }
        public override XfsBtState Tick()
        {
            return fn() ? XfsBtState.Success : XfsBtState.Failure;
        }

        public override string ToString()
        {
            return "Condition : " + fn.Method.ToString();
        }
    }
}
