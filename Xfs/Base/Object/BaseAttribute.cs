using System;

namespace Xfs
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class BaseAttribute: Attribute
	{
		public Type AttributeType { get; }

		public BaseAttribute()
		{
			this.AttributeType = this.GetType();
		}
	}
}