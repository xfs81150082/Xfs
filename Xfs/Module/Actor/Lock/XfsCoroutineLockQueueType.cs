using System.Collections.Generic;

namespace Xfs
{
    public class XfsCoroutineLockQueueType : XfsEntity
    {
        private readonly Dictionary<long, XfsCoroutineLockQueue> workQueues = new Dictionary<long, XfsCoroutineLockQueue>();

        public void Add(long key, XfsCoroutineLockQueue coroutineLockQueue)
        {
            this.workQueues.Add(key, coroutineLockQueue);
            coroutineLockQueue.Parent = this;
        }

        public void Remove(long key)
        {
            if (!this.workQueues.TryGetValue(key, out XfsCoroutineLockQueue queue))
            {
                return;
            }

            this.workQueues.Remove(key);
            queue.Dispose();
        }

        public bool ContainsKey(long key)
        {
            return this.workQueues.ContainsKey(key);
        }

        public bool TryGetValue(long key, out XfsCoroutineLockQueue coroutineLockQueue)
        {
            return this.workQueues.TryGetValue(key, out coroutineLockQueue);
        }
    }
}