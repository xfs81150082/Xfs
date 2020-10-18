using System;
namespace Xfs
{
    public class XfsCoolDownSystem : XfsSystem
    {
        public override void XfsAwake()
        {
            ValTime = 4000;
            AddComponent(new XfsCoolDown());
        }
        public override void XfsUpdate()
        {
            foreach (XfsEntity entity in GetTmEntities())
            {
                UpdateCoolDown(entity);
            }
        }
        void UpdateCoolDown(XfsEntity entity)
        {
            XfsCoolDown cd = entity.GetComponent<XfsCoolDown>();
            if (cd == null) return;
            if (cd.Counting)
            {
                cd.CdCount += 1;
                if (cd.CdCount >= cd.MaxCdCount)
                {
                    cd.CdCount = 0;
                    cd.Counting = false;
                }
            }
            if (cd.Timing)
            {
                cd.CdTime += 4;
                if (cd.CdTime >= cd.MaxCdTime)
                {
                    cd.CdTime = 0;
                    cd.Timing = false;
                }
            }
        }
    }
}