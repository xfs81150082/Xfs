using System;
using System.Collections.Generic;

namespace Xfs
{
    public class CoroutineLockComponentSystem : XfsAwakeSystem<XfsCoroutineLockComponent>
    {
        public override void Awake(XfsCoroutineLockComponent self)
        {
            self.Awake();
        }
    }

    public class XfsCoroutineLockComponent : XfsEntity
    {
        public static XfsCoroutineLockComponent Instance { get; private set; }

        private readonly List<XfsCoroutineLockQueueType> list = new List<XfsCoroutineLockQueueType>((int)XfsCoroutineLockType.Max);

        public void Awake()
        {
            Instance = this;
            for (int i = 0; i < this.list.Capacity; ++i)
            {
                XfsCoroutineLockQueueType coroutineLockQueueType = XfsEntityFactory.Create<XfsCoroutineLockQueueType>(this.Domain);
                this.list.Add(coroutineLockQueueType);
                coroutineLockQueueType.Parent = this;
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            base.Dispose();

            this.list.Clear();
        }

        //public async XfsTask<XfsCoroutineLock> Wait(XfsCoroutineLockType coroutineLockType, long key)
        //{
        //    XfsCoroutineLockQueueType coroutineLockQueueType = this.list[(int)coroutineLockType];
        //    if (!coroutineLockQueueType.TryGetValue(key, out XfsCoroutineLockQueue queue))
        //    {
        //        queue = XfsEntityFactory.Create<XfsCoroutineLockQueue>(this.Domain);
        //        coroutineLockQueueType.Add(key, queue);

        //        return XfsEntityFactory.CreateWithParent<XfsCoroutineLock, XfsCoroutineLockType, long>(this, coroutineLockType, key);
        //    }

        //    XfsTaskCompletionSource<XfsCoroutineLock> tcs = new XfsTaskCompletionSource<XfsCoroutineLock>();
        //    queue.Enqueue(tcs);
        //    return await tcs.Task;
        //}

        public void Notify(XfsCoroutineLockType coroutineLockType, long key)
        {
            XfsCoroutineLockQueueType coroutineLockQueueType = this.list[(int)coroutineLockType];
            if (!coroutineLockQueueType.TryGetValue(key, out XfsCoroutineLockQueue queue))
            {
                throw new Exception($"first work notify not find queue");
            }
            if (queue.Count == 0)
            {
                coroutineLockQueueType.Remove(key);
                queue.Dispose();
                return;
            }

            XfsTaskCompletionSource<XfsCoroutineLock> tcs = queue.Dequeue();
            tcs.SetResult(XfsEntityFactory.CreateWithParent<XfsCoroutineLock, XfsCoroutineLockType, long>(this, coroutineLockType, key));
        }
    }
}