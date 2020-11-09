namespace Xfs
{
    public static class XfsGame
    {
		private static XfsSence xfsSence;
		public static XfsSence XfsSence
		{
			get
			{
				if (xfsSence != null)
				{
					return xfsSence;
				}
				xfsSence = new XfsSence();
				return xfsSence;
			}
		}
	
		private static XfsEventSystem eventSystem;
		public static XfsEventSystem EventSystem
		{
			get
			{
				return eventSystem ?? (eventSystem = new XfsEventSystem());
			}
		}

		private static XfsObjectPool objectPool;
		public static XfsObjectPool ObjectPool
		{
			get
			{
				return objectPool ?? (objectPool = new XfsObjectPool());
			}
		}
		public static void Close()
		{
			xfsSence.Dispose();
			xfsSence = null;
			objectPool = null;
			eventSystem = null;
		}
	
	}
}