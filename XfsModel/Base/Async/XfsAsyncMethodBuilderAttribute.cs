using System;
using System.Runtime.CompilerServices;

namespace Xfs
{
    public sealed class XfsAsyncMethodBuilderAttribute : Attribute
    {
        public Type BuilderType
        {
            get;
        }

        public XfsAsyncMethodBuilderAttribute(Type builderType)
        {
            this.BuilderType = builderType;
        }
    }
}