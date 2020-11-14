using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    //[ObjectSystem]
    //public class OperationAwakeSystem : AwakeSystem<Operation, Action>
    //{
    //    public override void Awake(Operation self, Action a)
    //    {
    //        self.Awake(a);
    //    }
    //}

    public class XfsOperation : XfsBtNode
    {
        Action fn;
        Func<IEnumerator<XfsBtState>> coroutineFactory;
        IEnumerator<XfsBtState> coroutine;

        public void Awake(Action fn)
        {
            this.fn = fn;
        }

        public XfsOperation(Action fn)
        {
            this.fn = fn;
        }

        public XfsOperation(Func<IEnumerator<XfsBtState>> coroutineFactory)
        {
            this.coroutineFactory = coroutineFactory;
        }

        public override XfsBtState Tick()
        {
            if (fn != null)
            {
                fn();
                return XfsBtState.Success;
            }
            else
            {
                if (coroutine == null)
                {
                    coroutine = coroutineFactory();
                }
                if (!coroutine.MoveNext())
                {
                    coroutine = null;
                    return XfsBtState.Success;
                }
                var result = coroutine.Current;
                if (result == XfsBtState.Continue)
                {
                    return XfsBtState.Continue;
                }
                else
                {
                    coroutine = null;
                    return result;
                }
            }
        }

        public override string ToString()
        {
            return "Operation : " + fn.Method.ToString();
        }
    }

}
