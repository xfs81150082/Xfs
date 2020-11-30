namespace Xfs
{
    public class XfsNumericWatcherAttribute : XfsBaseAttribute
    {
        public XfsNumericType NumericType { get; }

        public XfsNumericWatcherAttribute(XfsNumericType type)
        {
            this.NumericType = type;
        }
    }
}