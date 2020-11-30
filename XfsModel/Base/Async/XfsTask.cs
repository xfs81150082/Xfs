using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Xfs
{
    [AsyncMethodBuilder(typeof(XfsAsyncTaskMethodBuilder))]
    public struct XfsTask
    {
        public static XfsTaskCompleted CompletedTask => new XfsTaskCompleted();

        private readonly XfsTaskCompletionSource awaiter;

        [DebuggerHidden]
        public XfsTask(XfsTaskCompletionSource awaiter)
        {
            this.awaiter = awaiter;
        }

        [DebuggerHidden]
        public XfsTaskCompletionSource GetAwaiter()
        {
            return this.awaiter;
        }

        [DebuggerHidden]
        public void Coroutine()
        {
            InnerCoroutine().Coroutine();
        }

        [DebuggerHidden]
        private async XfsVoid InnerCoroutine()
        {
            await this;
        }
    }

    [AsyncMethodBuilder(typeof(XfsAsyncTaskMethodBuilder))]
    public struct XfsTask<T>
    {
        private readonly XfsTaskCompletionSource<T> awaiter;

        [DebuggerHidden]
        public XfsTask(XfsTaskCompletionSource<T> awaiter)
        {
            this.awaiter = awaiter;
        }

        [DebuggerHidden]
        public XfsTaskCompletionSource<T> GetAwaiter()
        {
            return this.awaiter;
        }

        [DebuggerHidden]
        public void Coroutine()
        {
            InnerCoroutine().Coroutine();
        }

        private async XfsVoid InnerCoroutine()
        {
            await this;
        }
    }
}