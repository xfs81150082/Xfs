using System.Collections.Generic;
using System.Threading;

namespace Xfs
{
	public struct XfsTimer
	{
		public long Id { get; set; }
		public long Time { get; set; }
		public XfsTaskCompletionSource tcs;
	}

	[XfsObjectSystem]
	public class XfsTimerComponentUpdateSystem : XfsUpdateSystem<XfsTimerComponent>
	{
		public override void Update(XfsTimerComponent self)
		{
			self.Update();
		}
	}

	public class XfsTimerComponent : XfsComponent
	{
		private readonly Dictionary<long, XfsTimer> timers = new Dictionary<long, XfsTimer>();

		/// <summary>
		/// key: time, value: timer id
		/// </summary>
		private readonly XfsMultiMap<long, long> timeId = new XfsMultiMap<long, long>();

		private readonly Queue<long> timeOutTime = new Queue<long>();
		
		private readonly Queue<long> timeOutTimerIds = new Queue<long>();

		// 记录最小时间，不用每次都去MultiMap取第一个值
		private long minTime;

		public void Update()
		{
			if (this.timeId.Count == 0)
			{
				return;
			}

			long timeNow = XfsTimeHelper.Now();

			if (timeNow < this.minTime)
			{
				return;
			}
			
			foreach (KeyValuePair<long, List<long>> kv in this.timeId.GetDictionary())
			{
				long k = kv.Key;
				if (k > timeNow)
				{
					minTime = k;
					break;
				}
				this.timeOutTime.Enqueue(k);
			}

			while(this.timeOutTime.Count > 0)
			{
				long time = this.timeOutTime.Dequeue();
				foreach(long timerId in this.timeId[time])
				{
					this.timeOutTimerIds.Enqueue(timerId);	
				}
				this.timeId.Remove(time);
			}

			while(this.timeOutTimerIds.Count > 0)
			{
				long timerId = this.timeOutTimerIds.Dequeue();
				XfsTimer timer;
				if (!this.timers.TryGetValue(timerId, out timer))
				{
					continue;
				}
				this.timers.Remove(timerId);
				timer.tcs.SetResult();
			}
		}

		private void Remove(long id)
		{
			this.timers.Remove(id);
		}

		public XfsTask WaitTillAsync(long tillTime, CancellationToken cancellationToken)
		{
			XfsTaskCompletionSource tcs = new XfsTaskCompletionSource();
			XfsTimer timer = new XfsTimer { Id = XfsIdGeneraterHelper.GenerateId(), Time = tillTime, tcs = tcs };
			this.timers[timer.Id] = timer;
			this.timeId.Add(timer.Time, timer.Id);
			if (timer.Time < this.minTime)
			{
				this.minTime = timer.Time;
			}
			cancellationToken.Register(() => { this.Remove(timer.Id); });
			return tcs.Task;
		}

		public XfsTask WaitTillAsync(long tillTime)
		{
			XfsTaskCompletionSource tcs = new XfsTaskCompletionSource();
			XfsTimer timer = new XfsTimer { Id = XfsIdGeneraterHelper.GenerateId(), Time = tillTime, tcs = tcs };
			this.timers[timer.Id] = timer;
			this.timeId.Add(timer.Time, timer.Id);
			if (timer.Time < this.minTime)
			{
				this.minTime = timer.Time;
			}
			return tcs.Task;
		}

		public XfsTask WaitAsync(long time, CancellationToken cancellationToken)
		{
			XfsTaskCompletionSource tcs = new XfsTaskCompletionSource();
			XfsTimer timer = new XfsTimer { Id = XfsIdGeneraterHelper.GenerateId(), Time = XfsTimeHelper.Now() + time, tcs = tcs };
			this.timers[timer.Id] = timer;
			this.timeId.Add(timer.Time, timer.Id);
			if (timer.Time < this.minTime)
			{
				this.minTime = timer.Time;
			}
			cancellationToken.Register(() => { this.Remove(timer.Id); });
			return tcs.Task;
		}

		public XfsTask WaitAsync(long time)
		{
			XfsTaskCompletionSource tcs = new XfsTaskCompletionSource();
			XfsTimer timer = new XfsTimer { Id = XfsIdGeneraterHelper.GenerateId(), Time = XfsTimeHelper.Now() + time, tcs = tcs };
			this.timers[timer.Id] = timer;
			this.timeId.Add(timer.Time, timer.Id);
			if (timer.Time < this.minTime)
			{
				this.minTime = timer.Time;
			}
			return tcs.Task;
		}

	}
}