using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xfs
{
    /// <summary>
    /// 挂在 Unit 个体上
    /// </summary>
    public class XfsAoiUnitComponent : XfsComponent
    {
        public long gridId { get; set; } = -1;

        public long changerGridId { get; set; } = -1;

        public XfsAoiUintInfo playerIds;

        public XfsAoiUintInfo enemyIds;

        public XfsAoiUintInfo npcerIds;

        public XfsAoiUnitComponent()
        {
            playerIds = new XfsAoiUintInfo { MovesSet = new HashSet<long>(), OldMovesSet = new HashSet<long>(), Enters = new HashSet<long>(), Leaves = new HashSet<long>() };
            enemyIds = new XfsAoiUintInfo { MovesSet = new HashSet<long>(), OldMovesSet = new HashSet<long>(), Enters = new HashSet<long>(), Leaves = new HashSet<long>() };
            npcerIds = new XfsAoiUintInfo { MovesSet = new HashSet<long>(), OldMovesSet = new HashSet<long>(), Enters = new HashSet<long>(), Leaves = new HashSet<long>() };
        }      

        public override void Dispose()
        {
            playerIds.Dispose();
            enemyIds.Dispose();
            npcerIds.Dispose();

            gridId = 0;

            base.Dispose();
        }

    }
}
