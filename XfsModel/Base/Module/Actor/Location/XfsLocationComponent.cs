using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{ 
	//public class LockInfoAwakeSystem : XfsAwakeSystem<LockInfo, long, XfsCoroutineLock>
	//{
	//	public override void Awake(LockInfo self, long lockInstanceId, XfsCoroutineLock coroutineLock)
	//	{
	//		self.CoroutineLock = coroutineLock;
	//		self.LockInstanceId = lockInstanceId;
	//	}
	//}    

    // public class LockInfo : XfsEntity
	//{
	//	public long LockInstanceId;

	//	public XfsCoroutineLock CoroutineLock;

	//	public override void Dispose()
	//	{
	//		if (this.IsDisposed)
	//		{
	//			return;
	//		}

	//		base.Dispose();

	//		this.CoroutineLock.Dispose();
	//		this.CoroutineLock = null;
	//		LockInstanceId = 0;
	//	}
	//}

	public class XfsLocationComponent : XfsEntity
	{
		private readonly Dictionary<long, long> locations = new Dictionary<long, long>();

        public void Add(long key, long instanceId)
        {
            this.locations[key] = instanceId;
            Console.WriteLine($"location add key: {key} instanceId: {instanceId}");
        }

        public void Remove(long key)
        {
            this.locations.Remove(key);
            Console.WriteLine($"location remove key: {key}");
        }

        public  long Get(long key)
        {
            this.locations.TryGetValue(key, out long instanceId);
            Console.WriteLine($"location get key: {key} instanceId: {instanceId}");
            return instanceId;
        }


        //private readonly Dictionary<long, LockInfo> lockInfos = new Dictionary<long, LockInfo>();

        //public async XfsTask Add(long key, long instanceId)
        //{
        //	using (await XfsCoroutineLockComponent.Instance.Wait(XfsCoroutineLockType.Location, key))
        //	{
        //		this.locations[key] = instanceId;
        //		//Log.Info($"location add key: {key} instanceId: {instanceId}");
        //	}
        //}

        //public async XfsTask Remove(long key)
        //{
        //	using (await XfsCoroutineLockComponent.Instance.Wait(XfsCoroutineLockType.Location, key))
        //	{
        //		this.locations.Remove(key);
        //		//Log.Info($"location remove key: {key}");
        //	}
        //}

        //public async XfsVoid Lock(long key, long instanceId, int time = 0)
        //{
        //	XfsCoroutineLock coroutineLock = await XfsCoroutineLockComponent.Instance.Wait(XfsCoroutineLockType.Location, key);

        //	LockInfo lockInfo = XfsEntityFactory.Create<LockInfo, long, XfsCoroutineLock>(this.Domain, instanceId, coroutineLock);
        //	lockInfo.Parent = this;
        //	this.lockInfos.Add(key, lockInfo);

        //	//Log.Info($"location lock key: {key} instanceId: {instanceId}");

        //	if (time > 0)
        //	{
        //		long lockInfoInstanceId = lockInfo.InstanceId;
        //		await XfsTimerComponent.Instance.WaitAsync(time);
        //		if (lockInfo.InstanceId != lockInfoInstanceId)
        //		{
        //			return;
        //		}
        //		UnLock(key, instanceId, instanceId);
        //	}
        //}

        //public void UnLock(long key, long oldInstanceId, long newInstanceId)
        //{
        //	if (!this.lockInfos.TryGetValue(key, out LockInfo lockInfo))
        //	{
        //		//Log.Error($"location unlock not found key: {key} {oldInstanceId}");
        //		return;
        //	}

        //	if (oldInstanceId != lockInfo.LockInstanceId)
        //	{
        //		//Log.Error($"location unlock oldInstanceId is different: {key} {oldInstanceId}");
        //		return;
        //	}

        //	//Log.Info($"location unlock key: {key} instanceId: {oldInstanceId} newInstanceId: {newInstanceId}");

        //	this.locations[key] = newInstanceId;

        //	this.lockInfos.Remove(key);

        //	// 解锁
        //	lockInfo.Dispose();
        //}

        //public async XfsTask<long> Get(long key)
        //{
        //	using (await XfsCoroutineLockComponent.Instance.Wait(XfsCoroutineLockType.Location, key))
        //	{
        //		this.locations.TryGetValue(key, out long instanceId);
        //		//Log.Info($"location get key: {key} instanceId: {instanceId}");
        //		return instanceId;
        //	}
        //}

        public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();

			this.locations.Clear();
			//this.lockInfos.Clear();
		}
	}
}