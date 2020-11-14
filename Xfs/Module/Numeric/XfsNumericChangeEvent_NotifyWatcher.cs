namespace Xfs
{
    // 分发数值监听
    [XfsEvent(XfsEventIdType.NumbericChange)]
    public class NumericChangeEvent_NotifyWatcher : XfsAEvent<long, XfsNumericType, int>
    {
        public override void Run(long id, XfsNumericType numericType, int value)
        {
            XfsGame.XfsSence.GetComponent<XfsNumericWatcherComponent>().Run(numericType, id, value);
        }
    }
}
