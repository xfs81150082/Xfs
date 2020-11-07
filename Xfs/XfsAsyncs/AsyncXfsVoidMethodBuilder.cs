using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
     public struct AsyncXfsVoidMethodBuilder
    {
        private Action moveNext;

        // 1. Static Create method.
        //[DebuggerHidden]
        public static AsyncXfsVoidMethodBuilder Create()
        {
            AsyncXfsVoidMethodBuilder builder = new AsyncXfsVoidMethodBuilder();
            return builder;
        }

        // 2. TaskLike Task property(void)
        public XfsVoid Task => default;

        // 3. SetException
        //[DebuggerHidden]
        public void SetException(Exception exception)
        {
            //Log.Error(exception);
        }

        // 4. SetResult
        //[DebuggerHidden]
        public void SetResult()
        {
            // do nothing
        }

        // 5. AwaitOnCompleted
        //[DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                var runner = new XfsMoveNextRunner<TStateMachine>();
                moveNext = runner.Run;
                runner.StateMachine = stateMachine; // set after create delegate.
            }

            awaiter.OnCompleted(moveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        //[DebuggerHidden]
        //[SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                var runner = new XfsMoveNextRunner<TStateMachine>();
                moveNext = runner.Run;
                runner.StateMachine = stateMachine; // set after create delegate.
            }

            awaiter.UnsafeOnCompleted(moveNext);
        }

        // 7. Start
        //[DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        //[DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }

    }
}
