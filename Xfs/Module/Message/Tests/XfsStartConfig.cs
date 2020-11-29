namespace Xfs
{
	public class XfsStartConfig : XfsEntity
	{
		public long SenceId { get; set; }
		public XfsSceneType SenceType { get; set; }
		public string ServerIP { get; set; }
		public int Port { get; set; }
		public int MaxLiningCount { get; set; } = 10;
	}
}