using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    public static class XfsBt
    {
        public static XfsRoot Root() { return new XfsRoot(); }
        public static XfsSequence Sequence() { return new XfsSequence(); }
        public static XfsSelector Selector(bool shuffle = false) { return new XfsSelector(shuffle); }

        public static XfsCondition Call(Func<bool> fn) { return new XfsCondition(fn); }
        public static XfsOperation Send(Action fn) { return new XfsOperation(fn); }

        public static XfsTrigger Trigger(string name, bool set = true) { return new XfsTrigger(name, set); }     
    }

}
