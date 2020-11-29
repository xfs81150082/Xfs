using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
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