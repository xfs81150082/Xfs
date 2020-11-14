using System;

namespace Xfs
{
	public class XfsActorMessageAttribute : Attribute
	{
		public ushort Opcode { get; private set; }

		public XfsActorMessageAttribute(ushort opcode)
		{
			this.Opcode = opcode;
		}
	}
}