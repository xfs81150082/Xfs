namespace Xfs
{
    public class XfsCoolDownComponent : XfsComponent
    { 
        public int CdCount { get; set; } = 0;
        public int MaxCdCount { get; set; } = 4000;
        public bool Counting { get; set; } = true;
  
        public double CdTime { get; set; } = 0.0;
        public double MaxCdTime { get; set; } = 4000;
        public bool Timing { get; set; } = true;
    }
}
