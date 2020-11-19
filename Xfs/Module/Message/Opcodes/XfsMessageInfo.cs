using System;
namespace Xfs
{
	[Serializable]
	public struct XfsMessageInfo
	{
		public int Opcode { get; set; }
		public object Message { get; set; }	
		
	}
}