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
		//public static XfsSence XfsSence { get; set; } = new XfsSence();
        //public static XfsSystemMananger XfsSystemMananger { get; set; } = new XfsSystemMananger();

		private static XfsSystemMananger xfsSystemMananger;
		public static XfsSystemMananger XfsSystemMananger
		{
			get
			{
				if (xfsSystemMananger != null)
				{
					return xfsSystemMananger;
				}
				xfsSystemMananger = new XfsSystemMananger();
				return xfsSystemMananger;
			}
		}


	}
}