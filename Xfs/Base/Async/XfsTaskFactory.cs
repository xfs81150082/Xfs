using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Xfs
{
    public partial struct XfsTask
    {
        public static XfsTask CompletedTask => new XfsTask();
        public static XfsTask FromException(Exception ex)
        {
            XfsTaskCompletionSource tcs = new XfsTaskCompletionSource();
            tcs.TrySetException(ex);
            return tcs.Task;
        }
        public static XfsTask<T> FromException<T>(Exception ex)
        {
            var tcs = new XfsTaskCompletionSource<T>();
            tcs.TrySetException(ex);
            return tcs.Task;
        }
        public static XfsTask<T> FromResult<T>(T value)
        {
            return new XfsTask<T>(value);
        }
        public static XfsTask FromCanceled()
        {
            return CanceledETTaskCache.Task;
        }
        public static XfsTask<T> FromCanceled<T>()
        {
            return CanceledETTaskCache<T>.Task;
        }
        public static XfsTask FromCanceled(CancellationToken token)
        {
            XfsTaskCompletionSource tcs = new XfsTaskCompletionSource();
            tcs.TrySetException(new OperationCanceledException(token));
            return tcs.Task;
        }
        public static XfsTask<T> FromCanceled<T>(CancellationToken token)
        {
            var tcs = new XfsTaskCompletionSource<T>();
            tcs.TrySetException(new OperationCanceledException(token));
            return tcs.Task;
        }
        private static class CanceledETTaskCache
        {
            public static readonly XfsTask Task;

            static CanceledETTaskCache()
            {
                XfsTaskCompletionSource tcs = new XfsTaskCompletionSource();
                tcs.TrySetCanceled();
                Task = tcs.Task;
            }
        }
        private static class CanceledETTaskCache<T>
        {
            public static readonly XfsTask<T> Task;

            static CanceledETTaskCache()
            {
                var taskCompletionSource = new XfsTaskCompletionSource<T>();
                taskCompletionSource.TrySetCanceled();
                Task = taskCompletionSource.Task;
            }
        }
    }

    internal static class CompletedTasks
    {
        public static readonly XfsTask<bool> True = XfsTask.FromResult(true);
        public static readonly XfsTask<bool> False = XfsTask.FromResult(false);
        public static readonly XfsTask<int> Zero = XfsTask.FromResult(0);
        public static readonly XfsTask<int> MinusOne = XfsTask.FromResult(-1);
        public static readonly XfsTask<int> One = XfsTask.FromResult(1);
    }
}