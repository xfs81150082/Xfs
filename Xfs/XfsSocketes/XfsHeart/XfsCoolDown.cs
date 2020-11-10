namespace Xfs
{
    public class XfsCoolDown : XfsComponent
    {
        public long Key { get; set; }
        public int State { get; set; } = 0;
        public int CdCount { get; set; } = 0;
        public int MaxCdCount { get; set; } = 4000;
        public double CdTime { get; set; } = 0.0;
        public double MaxCdTime { get; set; } = 14.0;
        public bool Counting { get; set; } = true;
        public bool Timing { get; set; } = true;
        public XfsCoolDown(long key) { this.Key = key; }
        public XfsCoolDown() {  }
    }
}
