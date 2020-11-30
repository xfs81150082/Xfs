using System;
using System.ComponentModel;

namespace Xfs
{
	public abstract class XfsObject : ISupportInitialize, IDisposable
	{
		public XfsObject()
		{
		}

		public virtual void BeginInit()
		{
		}

		public virtual void EndInit()
		{
		}

		public virtual void Dispose()
		{
		}

	}
}