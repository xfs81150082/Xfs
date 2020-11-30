using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace Xfs
{
    public class XfsTaskCompletionSource : ICriticalNotifyCompletion
    {
        private XfsAwaiterStatus state;
        private ExceptionDispatchInfo exception;
        private Action continuation; // action or list

        [DebuggerHidden]
        public XfsTask Task => new XfsTask(this);

        [DebuggerHidden]
        public XfsAwaiterStatus Status => state;

        [DebuggerHidden]
        public bool IsCompleted => state != XfsAwaiterStatus.Pending;

        [DebuggerHidden]
        public void UnsafeOnCompleted(Action action)
        {
            this.continuation = action;
            if (state != XfsAwaiterStatus.Pending)
            {
                TryInvokeContinuation();
            }
        }

        [DebuggerHidden]
        public void OnCompleted(Action action)
        {
            this.UnsafeOnCompleted(action);
        }

        [DebuggerHidden]
        public void GetResult()
        {
            switch (this.state)
            {
                case XfsAwaiterStatus.Succeeded:
                    return;
                case XfsAwaiterStatus.Faulted:
                    this.exception?.Throw();
                    this.exception = null;
                    return;
                default:
                    throw new NotSupportedException("ETTask does not allow call GetResult directly when task not completed. Please use 'await'.");
            }
        }

        [DebuggerHidden]
        public void SetResult()
        {
            if (this.TrySetResult())
            {
                return;
            }

            throw new InvalidOperationException("TaskT_TransitionToFinal_AlreadyCompleted");
        }

        [DebuggerHidden]
        public void SetException(Exception e)
        {
            if (this.TrySetException(e))
            {
                return;
            }

            throw new InvalidOperationException("TaskT_TransitionToFinal_AlreadyCompleted");
        }

        [DebuggerHidden]
        private void TryInvokeContinuation()
        {
            this.continuation?.Invoke();
            this.continuation = null;
        }

        [DebuggerHidden]
        private bool TrySetResult()
        {
            if (this.state != XfsAwaiterStatus.Pending)
            {
                return false;
            }

            this.state = XfsAwaiterStatus.Succeeded;

            this.TryInvokeContinuation();
            return true;
        }

        [DebuggerHidden]
        private bool TrySetException(Exception e)
        {
            if (this.state != XfsAwaiterStatus.Pending)
            {
                return false;
            }

            this.state = XfsAwaiterStatus.Faulted;

            this.exception = ExceptionDispatchInfo.Capture(e);
            this.TryInvokeContinuation();
            return true;
        }
    }

    public class XfsTaskCompletionSource<T> : ICriticalNotifyCompletion
    {
        private XfsAwaiterStatus state;
        private T value;
        private ExceptionDispatchInfo exception;
        private Action continuation; // action or list

        [DebuggerHidden]
        public XfsTask<T> Task => new XfsTask<T>(this);

        [DebuggerHidden]
        public XfsTaskCompletionSource<T> GetAwaiter()
        {
            return this;
        }

        [DebuggerHidden]
        public T GetResult()
        {
            switch (this.state)
            {
                case XfsAwaiterStatus.Succeeded:
                    return this.value;
                case XfsAwaiterStatus.Faulted:
                    this.exception?.Throw();
                    this.exception = null;
                    return default;
                default:
                    throw new NotSupportedException("ETask does not allow call GetResult directly when task not completed. Please use 'await'.");
            }
        }

        [DebuggerHidden]
        public bool IsCompleted => state != XfsAwaiterStatus.Pending;

        [DebuggerHidden]
        public XfsAwaiterStatus Status => state;

        [DebuggerHidden]
        public void UnsafeOnCompleted(Action action)
        {
            this.continuation = action;
            if (state != XfsAwaiterStatus.Pending)
            {
                TryInvokeContinuation();
            }
        }

        [DebuggerHidden]
        public void OnCompleted(Action action)
        {
            this.UnsafeOnCompleted(action);
        }

        [DebuggerHidden]
        public void SetResult(T result)
        {
            if (this.TrySetResult(result))
            {
                return;
            }

            throw new InvalidOperationException("TaskT_TransitionToFinal_AlreadyCompleted");
        }

        [DebuggerHidden]
        public void SetException(Exception e)
        {
            if (this.TrySetException(e))
            {
                return;
            }

            throw new InvalidOperationException("TaskT_TransitionToFinal_AlreadyCompleted");
        }

        [DebuggerHidden]
        private void TryInvokeContinuation()
        {
            this.continuation?.Invoke();
            this.continuation = null;
        }

        [DebuggerHidden]
        private bool TrySetResult(T result)
        {
            if (this.state != XfsAwaiterStatus.Pending)
            {
                return false;
            }

            this.state = XfsAwaiterStatus.Succeeded;

            this.value = result;
            this.TryInvokeContinuation();
            return true;
        }

        [DebuggerHidden]
        private bool TrySetException(Exception e)
        {
            if (this.state != XfsAwaiterStatus.Pending)
            {
                return false;
            }

            this.state = XfsAwaiterStatus.Faulted;

            this.exception = ExceptionDispatchInfo.Capture(e);
            this.TryInvokeContinuation();
            return true;
        }
    }

}