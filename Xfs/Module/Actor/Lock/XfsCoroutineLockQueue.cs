using System.Collections.Generic;

namespace Xfs
{
    public class XfsCoroutineLockQueue : XfsEntity
    {
        private readonly Queue<XfsTaskCompletionSource<XfsCoroutineLock>> queue = new Queue<XfsTaskCompletionSource<XfsCoroutineLock>>();

        public void Enqueue(XfsTaskCompletionSource<XfsCoroutineLock> tcs)
        {
            this.queue.Enqueue(tcs);
        }

        public XfsTaskCompletionSource<XfsCoroutineLock> Dequeue()
        {
            return this.queue.Dequeue();
        }

        public int Count
        {
            get
            {
                return this.queue.Count;
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.queue.Clear();
        }
    }
}