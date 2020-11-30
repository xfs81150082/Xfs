using System;
namespace Xfs
{
	[Serializable]
	public struct XfsMessageInfo
	{
		public int Opcode { get; }
		public object Message { get; }		
		public XfsMessageInfo(int opcode, object message)
		{
			this.Opcode = opcode;
			this.Message = message;
		}

	}
}