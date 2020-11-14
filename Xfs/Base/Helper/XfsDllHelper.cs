using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public static class XfsDllHelper
	{
		public static Assembly GetAssembly(string name)
		{
			Assembly assembly = Assembly.Load(name);
			return assembly;
		}
		public static Assembly GetXfsAssembly()
		{
			byte[] dllBytes = File.ReadAllBytes("./Xfs.dll");
			byte[] pdbBytes = File.ReadAllBytes("./Xfs.pdb");
			Assembly assembly = Assembly.Load(dllBytes, pdbBytes);
			return assembly;
		}
		public static Assembly GetXfsGateSeverAssembly()
		{
			byte[] dllBytes = File.ReadAllBytes("./XfsGateSever.dll");
			byte[] pdbBytes = File.ReadAllBytes("./XfsGateSever.pdb");
			Assembly assembly = Assembly.Load(dllBytes, pdbBytes);
			return assembly;
		}
		public static Assembly GetXfsConsoleClientAssembly()
		{
			byte[] dllBytes = File.ReadAllBytes("./XfsConsoleClient.dll");
			byte[] pdbBytes = File.ReadAllBytes("./XfsConsoleClient.pdb");
			Assembly assembly = Assembly.Load(dllBytes, pdbBytes);
			return assembly;
		}



	}
}
