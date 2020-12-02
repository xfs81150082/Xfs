namespace Xfs
{
    public class CoroutineLockSystem : XfsAwakeSystem<XfsCoroutineLock, XfsCoroutineLockType, long>
    {
        public override void Awake(XfsCoroutineLock self, XfsCoroutineLockType coroutineLockType, long key)
        {
            self.Awake(coroutineLockType, key);
        }
    }

    public class XfsCoroutineLock : XfsEntity
    {
        private XfsCoroutineLockType coroutineLockType;
        private long key;

        public void Awake(XfsCoroutineLockType type, long k)
        {
            this.coroutineLockType = type;
            this.key = k;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            XfsCoroutineLockComponent.Instance.Notify(coroutineLockType, this.key);
        }
    }
}