using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public enum XfsAwaiterStatus
    {
        Pending = 0,
        Succeeded = 1,
        Faulted = 2,
        Canceled = 3
    }
    public interface IXfsAwaiter : ICriticalNotifyCompletion
    {
        XfsAwaiterStatus Status { get; }
        bool IsCompleted { get; }
        void GetResult();
    }
    public interface IXfsAwaiter<out T> : IXfsAwaiter
    {
        new T GetResult();
    }
    public static class AwaiterStatusExtensions
    {
        public static bool IsCompleted(this XfsAwaiterStatus status)
        {
            return status != XfsAwaiterStatus.Pending;
        }
        public static bool IsCompletedSuccessfully(this XfsAwaiterStatus status)
        {
            return status == XfsAwaiterStatus.Succeeded;
        }
        public static bool IsCanceled(this XfsAwaiterStatus status)
        {
            return status == XfsAwaiterStatus.Canceled;
        }
        public static bool IsFaulted(this XfsAwaiterStatus status)
        {
            return status == XfsAwaiterStatus.Faulted;
        }
    }

}