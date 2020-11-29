using System;

namespace Xfs
{
	public class XfsActorMessageAttribute : Attribute
	{
		public int Opcode { get; private set; }

		public XfsActorMessageAttribute(int opcode)
		{
			this.Opcode = opcode;
		}
	}
}