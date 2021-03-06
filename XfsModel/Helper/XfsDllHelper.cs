﻿using System;
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
		public static Assembly GetXfsServerAssembly()
		{
			byte[] dllBytes = File.ReadAllBytes("./XfsServer.dll");
			byte[] pdbBytes = File.ReadAllBytes("./XfsServer.pdb");
			Assembly assembly = Assembly.Load(dllBytes, pdbBytes);
			return assembly;
		}
		public static Assembly GetXfsGateServerAssembly()
		{
			byte[] dllBytes = File.ReadAllBytes("./XfsGateServer.dll");
			byte[] pdbBytes = File.ReadAllBytes("./XfsGateServer.pdb");
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
