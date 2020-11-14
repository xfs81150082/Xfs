using System;
namespace Xfs
{
    [XfsObjectSystem]
    public class XfsCoolDownUpdateSystem : XfsUpdateSystem<XfsCoolDown>
    {       
        public override void Update(XfsCoolDown self)
        {
                UpdateCoolDown(self);
        }
        void UpdateCoolDown(XfsCoolDown self)
        {
            if (self.Counting)
            {
                self.CdCount += 1;
                if (self.CdCount >= self.MaxCdCount)
                {
                    self.CdCount = 0;
                    self.Counting = false;
                }
            }
            //if (cd.Timing)
            //{
            //    cd.CdTime += 4;
            //    if (cd.CdTime >= cd.MaxCdTime)
            //    {
            //        cd.CdTime = 0;
            //        cd.Timing = false;
            //    }
            //}
        }
    }
}