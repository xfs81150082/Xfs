using System;
using System.Collections.Generic;

namespace Xfs
{
    public class XfsCancellationToken
    {
        private Action action;

        public void Register(Action callback)
        {
            this.action = callback;
        }

        public void Cancel()
        {
            action.Invoke();
        }
    }
}