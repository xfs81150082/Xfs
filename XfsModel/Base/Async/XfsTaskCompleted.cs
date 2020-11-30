using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Xfs
{
    [AsyncMethodBuilder(typeof(XfsAsyncTaskCompletedMethodBuilder))]
    public struct XfsTaskCompleted : ICriticalNotifyCompletion
    {
        [DebuggerHidden]
        public XfsTaskCompleted GetAwaiter()
        {
            return this;
        }

        [DebuggerHidden]
        public bool IsCompleted => true;

        [DebuggerHidden]
        public void GetResult()
        {
        }

        [DebuggerHidden]
        public void OnCompleted(Action continuation)
        {
        }

        [DebuggerHidden]
        public void UnsafeOnCompleted(Action continuation)
        {
        }
    }
}