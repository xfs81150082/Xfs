using System;
using System.Collections.Generic;
using System.Threading;

namespace Xfs
{
    public interface IXfsTimer
    {
        void Run(bool isTimeout);
    }

    public class OnceWaitTimerAwakeSystem : XfsAwakeSystem<OnceWaitTimer, XfsTaskCompletionSource<bool>>
    {
        public override void Awake(OnceWaitTimer self, XfsTaskCompletionSource<bool> callback)
        {
            self.Callback = callback;
        }
    }

    public class OnceWaitTimer : XfsEntity, IXfsTimer
    {
        public XfsTaskCompletionSource<bool> Callback { get; set; }

        public void Run(bool isTimeout)
        {
            XfsTaskCompletionSource<bool> tcs = this.Callback;
            this.GetParent<XfsTimerComponent>().Remove(this.Id);
            tcs.SetResult(isTimeout);
        }
    }

    public class OnceTimerAwakeSystem : XfsAwakeSystem<XfsOnceTimer, Action<bool>>
    {
        public override void Awake(XfsOnceTimer self, Action<bool> callback)
        {
            self.Callback = callback;
        }
    }

    public class XfsOnceTimer : XfsEntity, IXfsTimer
    {
        public Action<bool> Callback { get; set; }

        public void Run(bool isTimeout)
        {
            try
            {
                this.Callback.Invoke(isTimeout);
            }
            catch (Exception e)
            {
                //Log.Error(e);
                Console.WriteLine(e);
            }
        }
    }
  
    public class RepeatedTimerAwakeSystem : XfsAwakeSystem<XfsRepeatedTimer, long, Action<bool>>
    {
        public override void Awake(XfsRepeatedTimer self, long repeatedTime, Action<bool> callback)
        {
            self.Awake(repeatedTime, callback);
        }
    }

    public class XfsRepeatedTimer : XfsEntity, IXfsTimer
    {
        public void Awake(long repeatedTime, Action<bool> callback)
        {
            this.StartTime = XfsTimeHelper.Now();
            this.RepeatedTime = repeatedTime;
            this.Callback = callback;
            this.Count = 1;
        }

        private long StartTime { get; set; }

        private long RepeatedTime { get; set; }

        // 下次一是第几次触发
        private int Count { get; set; }

        public Action<bool> Callback { private get; set; }

        public void Run(bool isTimeout)
        {
            ++this.Count;
            XfsTimerComponent timerComponent = this.GetParent<XfsTimerComponent>();
            long tillTime = this.StartTime + this.RepeatedTime * this.Count;
            timerComponent.AddToTimeId(tillTime, this.Id);

            try
            {
                this.Callback?.Invoke(isTimeout);
            }
            catch (Exception e)
            {
                //Log.Error(e);
                Console.WriteLine(e);
            }
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            long id = this.Id;

            if (id == 0)
            {
                //Log.Error($"RepeatedTimer可能多次释放了");
                Console.WriteLine($"RepeatedTimer可能多次释放了");
                return;
            }

            base.Dispose();

            this.StartTime = 0;
            this.RepeatedTime = 0;
            this.Callback = null;
            this.Count = 0;
        }
    }


    public class TimerComponentAwakeSystem : XfsAwakeSystem<XfsTimerComponent>
    {
        public override void Awake(XfsTimerComponent self)
        {
            XfsTimerComponent.Instance = self;
        }
    }


    public class TimerComponentUpdateSystem : XfsUpdateSystem<XfsTimerComponent>
    {
        public override void Update(XfsTimerComponent self)
        {
            self.Update();
        }
    }

    public class XfsTimerComponent : XfsEntity
    {
        public static XfsTimerComponent Instance { get; set; }

        private readonly Dictionary<long, IXfsTimer> timers = new Dictionary<long, IXfsTimer>();

        /// <summary>
        /// key: time, value: timer id
        /// </summary>
        public readonly XfsMultiMap<long, long> TimeId = new XfsMultiMap<long, long>();

        private readonly Queue<long> timeOutTime = new Queue<long>();

        private readonly Queue<long> timeOutTimerIds = new Queue<long>();

        // 记录最小时间，不用每次都去MultiMap取第一个值
        private long minTime;

        public void Update()
        {
            if (this.TimeId.Count == 0)
            {
                return;
            }

            long timeNow = XfsTimeHelper.Now();

            if (timeNow < this.minTime)
            {
                return;
            }

            foreach (KeyValuePair<long, List<long>> kv in this.TimeId.GetDictionary())
            {
                long k = kv.Key;
                if (k > timeNow)
                {
                    minTime = k;
                    break;
                }
                this.timeOutTime.Enqueue(k);
            }

            while (this.timeOutTime.Count > 0)
            {
                long time = this.timeOutTime.Dequeue();
                foreach (long timerId in this.TimeId[time])
                {
                    this.timeOutTimerIds.Enqueue(timerId);
                }
                this.TimeId.Remove(time);
            }

            while (this.timeOutTimerIds.Count > 0)
            {
                long timerId = this.timeOutTimerIds.Dequeue();
                IXfsTimer timer;
                if (!this.timers.TryGetValue(timerId, out timer))
                {
                    continue;
                }

                timer.Run(true);
            }
        }

        //public async XfsTask<bool> WaitTillAsync(long tillTime, XfsCancellationToken cancellationToken)
        //{
        //    if (XfsTimeHelper.Now() > tillTime)
        //    {
        //        return true;
        //    }
        //    XfsTaskCompletionSource<bool> tcs = new XfsTaskCompletionSource<bool>();
        //    OnceWaitTimer timer = XfsEntityFactory.CreateWithParent<OnceWaitTimer, XfsTaskCompletionSource<bool>>(this, tcs);
        //    this.timers[timer.Id] = timer;
        //    AddToTimeId(tillTime, timer.Id);

        //    long instanceId = timer.InstanceId;
        //    cancellationToken.Register(() =>
        //    {
        //        if (instanceId != timer.InstanceId)
        //        {
        //            return;
        //        }

        //        timer.Run(false);

        //        this.Remove(timer.Id);
        //    });
        //    return await tcs.Task;
        //}

        //public async XfsTask<bool> WaitTillAsync(long tillTime)
        //{
        //    if (XfsTimeHelper.Now() > tillTime)
        //    {
        //        return true;
        //    }
        //    XfsTaskCompletionSource<bool> tcs = new XfsTaskCompletionSource<bool>();
        //    OnceWaitTimer timer = XfsEntityFactory.CreateWithParent<OnceWaitTimer, XfsTaskCompletionSource<bool>>(this, tcs);
        //    this.timers[timer.Id] = timer;
        //    AddToTimeId(tillTime, timer.Id);
        //    return await tcs.Task;
        //}

        //public async XfsTask<bool> WaitAsync(long time, XfsCancellationToken cancellationToken)
        //{
        //    long tillTime = XfsTimeHelper.Now() + time;

        //    if (XfsTimeHelper.Now() > tillTime)
        //    {
        //        return true;
        //    }

        //    XfsTaskCompletionSource<bool> tcs = new XfsTaskCompletionSource<bool>();
        //    OnceWaitTimer timer = XfsEntityFactory.CreateWithParent<OnceWaitTimer, XfsTaskCompletionSource<bool>>(this, tcs);
        //    this.timers[timer.Id] = timer;
        //    AddToTimeId(tillTime, timer.Id);
        //    long instanceId = timer.InstanceId;
        //    cancellationToken.Register(() =>
        //    {
        //        if (instanceId != timer.InstanceId)
        //        {
        //            return;
        //        }

        //        timer.Run(false);

        //        this.Remove(timer.Id);
        //    });
        //    return await tcs.Task;
        //}

        //public async XfsTask<bool> WaitAsync(long time)
        //{
        //    long tillTime = XfsTimeHelper.Now() + time;
        //    XfsTaskCompletionSource<bool> tcs = new XfsTaskCompletionSource<bool>();
        //    OnceWaitTimer timer = XfsEntityFactory.CreateWithParent<OnceWaitTimer, XfsTaskCompletionSource<bool>>(this, tcs);
        //    this.timers[timer.Id] = timer;
        //    AddToTimeId(tillTime, timer.Id);
        //    return await tcs.Task;
        //}

        /// <summary>
        /// 创建一个RepeatedTimer
        /// </summary>
        /// <param name="time"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public long NewRepeatedTimer(long time, Action<bool> action)
        {
            if (time < 30)
            {
                throw new Exception($"repeated time < 30");
            }
            long tillTime = XfsTimeHelper.Now() + time;
            XfsRepeatedTimer timer = XfsEntityFactory.CreateWithParent<XfsRepeatedTimer, long, Action<bool>>(this, time, action);
            this.timers[timer.Id] = timer;
            AddToTimeId(tillTime, timer.Id);
            return timer.Id;
        }

        public XfsRepeatedTimer GetRepeatedTimer(long id)
        {
            if (!this.timers.TryGetValue(id, out IXfsTimer timer))
            {
                return null;
            }
            return timer as XfsRepeatedTimer;
        }

        public void Remove(long id)
        {
            if (id == 0)
            {
                return;
            }
            IXfsTimer timer;
            if (!this.timers.TryGetValue(id, out timer))
            {
                return;
            }
            this.timers.Remove(id);

            (timer as IDisposable)?.Dispose();
        }

        public long NewOnceTimer(long tillTime, Action action)
        {
            XfsOnceTimer timer = XfsEntityFactory.CreateWithParent<XfsOnceTimer, Action>(this, action);
            this.timers[timer.Id] = timer;
            AddToTimeId(tillTime, timer.Id);
            return timer.Id;
        }

        public XfsOnceTimer GetOnceTimer(long id)
        {
            if (!this.timers.TryGetValue(id, out IXfsTimer timer))
            {
                return null;
            }
            return timer as XfsOnceTimer;
        }

        public void AddToTimeId(long tillTime, long id)
        {
            this.TimeId.Add(tillTime, id);
            if (tillTime < this.minTime)
            {
                this.minTime = tillTime;
            }
        }
    }

}