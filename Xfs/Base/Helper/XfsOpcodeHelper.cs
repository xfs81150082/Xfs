using System.Collections.Generic;

namespace Xfs
{
	public static class XfsOpcodeHelper
	{
		private static readonly HashSet<ushort> ignoreDebugLogMessageSet = new HashSet<ushort>
		{
			XfsOuterOpcode.C4G_Ping,
			XfsOuterOpcode.G4C_Ping,
		};

		public static bool IsNeedDebugLogMessage(ushort opcode)
		{
			if (ignoreDebugLogMessageSet.Contains(opcode))
			{
				return false;
			}

			return true;
		}

		public static bool IsClientHotfixMessage(ushort opcode)
		{
			return opcode > 10000;
		}
	}
}