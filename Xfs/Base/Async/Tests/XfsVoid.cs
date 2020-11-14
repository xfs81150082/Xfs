using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public struct XfsVoid
    {
        public void Coroutine()
        {
        }
        public XfsAwaiter GetXfsAwaiter()
        {
            return new XfsAwaiter();
        }
        public struct XfsAwaiter : ICriticalNotifyCompletion
        {
            public bool IsCompleted => true;
            public void GetResult()
            {
                throw new InvalidOperationException("ETAvoid can not await, use Coroutine method instead!");
            }
            public void OnCompleted(Action continuation)
            {
            }
            public void UnsafeOnCompleted(Action continuation)
            {
            }
        }
    }
}