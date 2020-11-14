using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public abstract class XfsComponentWithId : XfsComponent
	{
		public long Id { get; set; }

		protected XfsComponentWithId()
		{
			this.Id = this.InstanceId;
		}

		protected XfsComponentWithId(long id)
		{
			this.Id = id;
		}
		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();
		}

	}
}
