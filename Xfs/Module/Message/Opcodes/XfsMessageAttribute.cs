namespace Xfs
{
	public class XfsMessageAttribute: XfsBaseAttribute
	{
		public ushort Opcode { get; }

		public XfsMessageAttribute(ushort opcode)
		{
			this.Opcode = opcode;
		}
	}
}