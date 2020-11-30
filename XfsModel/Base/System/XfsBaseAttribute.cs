using System;

namespace Xfs
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class XfsBaseAttribute : Attribute
    {
        public Type AttributeType { get; }
        public XfsBaseAttribute()
        {
            this.AttributeType = this.GetType();
        }
    }

}