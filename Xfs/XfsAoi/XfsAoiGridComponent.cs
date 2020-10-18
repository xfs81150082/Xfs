using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xfs
{ 
    /// <summary>
    /// 挂在 Sense场景上，Update更新AOI格子和单元内容
    /// </summary>
    public class XfsAoiGridComponent : XfsComponent
    {
        private readonly Dictionary<long, XfsAoiGrid> grids = new Dictionary<long, XfsAoiGrid>();

        public int rcCount = 5;

        public int gridWide = 20;

        public int mapWide = 100;

        #region
        public void Add(XfsAoiGrid aoiGrid)
        {
            grids.Add(aoiGrid.gridId, aoiGrid);
        }

        public XfsAoiGrid Get(long gridId)
        {
            return grids.TryGetValue(gridId, out var aoiGrid) ? aoiGrid : null;
        }

        public XfsAoiGrid[] GetAll()
        {
            return this.grids.Values.ToArray();
        }

        public int Count
        {
            get
            {
                return this.grids.Count;
            }
        }

        public override void Dispose()
        {           
            base.Dispose();

            foreach (XfsAoiGrid aoiGrid in this.grids.Values)
            {
                aoiGrid.Dispose();
            }
            this.grids.Clear();
        }
        #endregion
        
    }
}