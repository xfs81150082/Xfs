namespace Xfs
{
	public class XfsMessageAttribute: XfsBaseAttribute
	{
		public int Opcode { get; }

		public XfsMessageAttribute(int opcode)
		{
			this.Opcode = opcode;
		}
	}
}