using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace Xfs
{
    public struct XfsAsyncTaskMethodBuilder
    {
        public XfsTaskCompletionSource Tcs;

        // 1. Static Create method.
        [DebuggerHidden]
        public static XfsAsyncTaskMethodBuilder Create()
        {
            XfsAsyncTaskMethodBuilder builder = new XfsAsyncTaskMethodBuilder() { Tcs = new XfsTaskCompletionSource() };
            return builder;
        }

        // 2. TaskLike Task property.
        [DebuggerHidden]
        public XfsTask Task => this.Tcs.Task;

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            this.Tcs.SetException(exception);
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult()
        {
            this.Tcs.SetResult();
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }

    public struct XfsAsyncTaskMethodBuilder<T>
    {
        public XfsTaskCompletionSource<T> Tcs;

        // 1. Static Create method.
        [DebuggerHidden]
        public static XfsAsyncTaskMethodBuilder<T> Create()
        {
            XfsAsyncTaskMethodBuilder<T> builder = new XfsAsyncTaskMethodBuilder<T>() { Tcs = new XfsTaskCompletionSource<T>() };
            return builder;
        }

        // 2. TaskLike Task property.
        [DebuggerHidden]
        public XfsTask<T> Task => this.Tcs.Task;

        // 3. SetException
        [DebuggerHidden]
        public void SetException(Exception exception)
        {
            this.Tcs.SetException(exception);
        }

        // 4. SetResult
        [DebuggerHidden]
        public void SetResult(T ret)
        {
            this.Tcs.SetResult(ret);
        }

        // 5. AwaitOnCompleted
        [DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        [DebuggerHidden]
        [SecuritySafeCritical]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine) where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        // 7. Start
        [DebuggerHidden]
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        [DebuggerHidden]
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }
}