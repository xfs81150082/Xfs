﻿using System;
namespace Xfs
{
	[Serializable]
	public struct XfsMessageInfo
	{
		public ushort Opcode { get; set; }
		public int RpcId { get; set; }
		public object Message { get; set; }	
		
	}
}