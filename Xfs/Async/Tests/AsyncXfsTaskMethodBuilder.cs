using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public struct AsyncXfsTaskMethodBuilder
    {
        private XfsTaskCompletionSource tcs;
        private Action moveNext;

        // 1. Static Create method.
        public static AsyncXfsTaskMethodBuilder Create()
        {
            AsyncXfsTaskMethodBuilder builder = new AsyncXfsTaskMethodBuilder();
            return builder;
        }

        // 2. TaskLike Task property.
        public XfsTask Task
        {
            get
            {
                if (this.tcs != null)
                {
                    return this.tcs.Task;
                }

                if (moveNext == null)
                {
                    return XfsTask.CompletedTask;
                }

                this.tcs = new XfsTaskCompletionSource();
                return this.tcs.Task;
            }
        }

        // 3. SetException
        public void SetException(Exception exception)
        {
            if (this.tcs == null)
            {
                this.tcs = new XfsTaskCompletionSource();
            }

            if (exception is OperationCanceledException ex)
            {
                this.tcs.TrySetCanceled(ex);
            }
            else
            {
                this.tcs.TrySetException(exception);
            }
        }

        // 4. SetResult
        public void SetResult()
        {
            if (moveNext == null)
            {
            }
            else
            {
                if (this.tcs == null)
                {
                    this.tcs = new XfsTaskCompletionSource();
                }

                this.tcs.TrySetResult();
            }
        }

        // 5. AwaitOnCompleted
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
                where TAwaiter : INotifyCompletion
                where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (this.tcs == null)
                {
                    this.tcs = new XfsTaskCompletionSource(); // built future.
                }

                var runner = new XfsMoveNextRunner<TStateMachine>();
                moveNext = runner.Run;
                runner.StateMachine = stateMachine; // set after create delegate.
            }

            awaiter.OnCompleted(moveNext);
        }

        // 6. AwaitUnsafeOnCompleted
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
                where TAwaiter : ICriticalNotifyCompletion
                where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (this.tcs == null)
                {
                    this.tcs = new XfsTaskCompletionSource(); // built future.
                }

                var runner = new XfsMoveNextRunner<TStateMachine>();
                moveNext = runner.Run;
                runner.StateMachine = stateMachine; // set after create delegate.
            }

            awaiter.UnsafeOnCompleted(moveNext);
        }
        // 7. Start
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }
        // 8. SetStateMachine
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }

    public struct XfsAsyncTaskMethodBuilder<T>
    {
        private T result;
        private XfsTaskCompletionSource<T> tcs;
        private Action moveNext;

        // 1. Static Create method.
        //[DebuggerHidden]
        public static XfsAsyncTaskMethodBuilder<T> Create()
        {
            var builder = new XfsAsyncTaskMethodBuilder<T>();
            return builder;
        }

        // 2. TaskLike Task property.
        //[DebuggerHidden]
        public XfsTask<T> Task
        {
            get
            {
                if (this.tcs != null)
                {
                    return new XfsTask<T>(this.tcs);
                }

                if (moveNext == null)
                {
                    return new XfsTask<T>(result);
                }

                this.tcs = new XfsTaskCompletionSource<T>();
                return this.tcs.Task;
            }
        }

        // 3. SetException
        //[DebuggerHidden]
        public void SetException(Exception exception)
        {
            if (this.tcs == null)
            {
                this.tcs = new XfsTaskCompletionSource<T>();
            }

            if (exception is OperationCanceledException ex)
            {
                this.tcs.TrySetCanceled(ex);
            }
            else
            {
                this.tcs.TrySetException(exception);
            }
        }

        // 4. SetResult
        //[DebuggerHidden]
        public void SetResult(T ret)
        {
            if (moveNext == null)
            {
                this.result = ret;
            }
            else
            {
                if (this.tcs == null)
                {
                    this.tcs = new XfsTaskCompletionSource<T>();
                }

                this.tcs.TrySetResult(ret);
            }
        }

        // 5. AwaitOnCompleted
        //[DebuggerHidden]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
                where TAwaiter : INotifyCompletion
                where TStateMachine : IAsyncStateMachine
        {
            if (moveNext == null)
            {
                if (this.tcs == null)
                {
                    this.tcs = new XfsTaskCompletionSource<T>(); // built future.
                }

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
                if (this.tcs == null)
                {
                    this.tcs = new XfsTaskCompletionSource<T>(); // built future.
                }

                var runner = new XfsMoveNextRunner<TStateMachine>();
                moveNext = runner.Run;
                runner.StateMachine = stateMachine; // set after create delegate.
            }

            awaiter.UnsafeOnCompleted(moveNext);
        }

        // 7. Start
        public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        // 8. SetStateMachine
        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }
    }


}
