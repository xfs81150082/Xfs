
namespace Xfs
{

	public class XfsStartConfig : XfsEntity
	{
		public long SenceId { get; set; }
		public XfsSenceType SenceType { get; set; }
		public string ServerIP { get; set; }

		public string IP = "127.0.0.1";
		public int Port = 2001;
		public int MaxLiningCount = 10;

	}
}