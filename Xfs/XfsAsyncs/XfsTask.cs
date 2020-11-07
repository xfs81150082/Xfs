using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public partial struct XfsTask : IEquatable<XfsTask>
    {
        private readonly IXfsAwaiter awaiter;

        public XfsTask(IXfsAwaiter awaiter)
        {
            this.awaiter = awaiter;
        }
        public XfsAwaiterStatus Status => awaiter?.Status ?? XfsAwaiterStatus.Succeeded;  
        public bool IsCompleted => awaiter?.IsCompleted ?? true;
        public void GetResult()
        {
            if (awaiter != null)
            {
                awaiter.GetResult();
            }
        }
        public void Coroutine()
        {
        }
        public XfsAwaiter GetIXfsAwaiter()
        {
            return new XfsAwaiter(this);
        }
        public bool Equals(XfsTask other)
        {
            if (this.awaiter == null && other.awaiter == null)
            {
                return true;
            }
            if (this.awaiter != null && other.awaiter != null)
            {
                return this.awaiter == other.awaiter;
            }
            return false;
        }
        public override int GetHashCode()
        {
            if (this.awaiter == null)
            {
                return 0;
            }
            return this.awaiter.GetHashCode();
        }
        public override string ToString()
        {
            return this.awaiter == null ? "()"
                    : this.awaiter.Status == XfsAwaiterStatus.Succeeded ? "()"
                    : "(" + this.awaiter.Status + ")";
        }
        public struct XfsAwaiter : IXfsAwaiter
        {
            private readonly XfsTask task;
            public XfsAwaiter(XfsTask task)
            {
                this.task = task;
            }
            public bool IsCompleted => task.IsCompleted;
            public XfsAwaiterStatus Status => task.Status;
            public void GetResult()
            {
                task.GetResult();
            }
            public void OnCompleted(Action continuation)
            {
                if (task.awaiter != null)
                {
                    task.awaiter.OnCompleted(continuation);
                }
                else
                {
                    continuation();
                }
            }                    
            public void UnsafeOnCompleted(Action continuation)
            {
                if (task.awaiter != null)
                {
                    task.awaiter.UnsafeOnCompleted(continuation);
                }
                else
                {
                    continuation();
                }
            }
        }
    }

    public struct XfsTask<T> : IEquatable<XfsTask<T>>
    {
        private readonly T result;
        private readonly IXfsAwaiter<T> awaiter;
        public XfsTask(T result)
        {
            this.result = result;
            this.awaiter = null;
        }
        public XfsTask(IXfsAwaiter<T> awaiter)
        {
            this.result = default;
            this.awaiter = awaiter;
        }
        public XfsAwaiterStatus Status => awaiter?.Status ?? XfsAwaiterStatus.Succeeded;
        public bool IsCompleted => awaiter?.IsCompleted ?? true;
        public T Result
        {
            get
            {
                if (awaiter == null)
                {
                    return result;
                }

                return this.awaiter.GetResult();
            }
        }

        public void Coroutine()
        {
        }
        public XfsAwaiter GetAwaiter()
        {
            return new XfsAwaiter(this);
        }

        public bool Equals(XfsTask<T> other)
        {
            if (this.awaiter == null && other.awaiter == null)
            {
                return EqualityComparer<T>.Default.Equals(this.result, other.result);
            }

            if (this.awaiter != null && other.awaiter != null)
            {
                return this.awaiter == other.awaiter;
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (this.awaiter == null)
            {
                if (result == null)
                {
                    return 0;
                }

                return result.GetHashCode();
            }

            return this.awaiter.GetHashCode();
        }

        public override string ToString()
        {
            return this.awaiter == null ? result.ToString()
                    : this.awaiter.Status == XfsAwaiterStatus.Succeeded ? this.awaiter.GetResult().ToString()
                    : "(" + this.awaiter.Status + ")";
        }

        public static implicit operator XfsTask(XfsTask<T> task)
        {
            if (task.awaiter != null)
            {
                return new XfsTask(task.awaiter);
            }

            return new XfsTask();
        }

        public struct XfsAwaiter : IXfsAwaiter<T>
        {
            private readonly XfsTask<T> task;

            public XfsAwaiter(XfsTask<T> task)
            {
                this.task = task;
            }

            public bool IsCompleted => task.IsCompleted;

            public XfsAwaiterStatus Status => task.Status;

            void IXfsAwaiter.GetResult()
            {
                GetResult();
            }

            public T GetResult()
            {
                return task.Result;
            }
            public void OnCompleted(Action continuation)
            {
                if (task.awaiter != null)
                {
                    task.awaiter.OnCompleted(continuation);
                }
                else
                {
                    continuation();
                }
            }
            public void UnsafeOnCompleted(Action continuation)
            {
                if (task.awaiter != null)
                {
                    task.awaiter.UnsafeOnCompleted(continuation);
                }
                else
                {
                    continuation();
                }
            }
        }
    }

}