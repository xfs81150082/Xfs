using System;
namespace Xfs
{
    [XfsObjectSystem]
    public class XfsCoolDownComponentUpdateSystem : XfsUpdateSystem<XfsCoolDownComponent>
    {       
        public override void Update(XfsCoolDownComponent self)
        {
                UpdateCoolDown(self);
        }
        void UpdateCoolDown(XfsCoolDownComponent self)
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
            if (self.Timing)
            {
                self.CdTime += 4;
                if (self.CdTime >= self.MaxCdTime)
                {
                    self.CdTime = 0;
                    self.Timing = false;
                }
            }
        }


    }
}