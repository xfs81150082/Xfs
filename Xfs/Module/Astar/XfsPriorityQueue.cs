using System.Collections;
namespace Xfs
{
    public class XfsPriorityQueue
    {
        private ArrayList grids = new ArrayList();
        public int Length
        {
            get { return this.grids.Count; }
        }
        public bool Contains(object grid)
        {
            return this.grids.Contains(grid);
        }
        public XfsGrid First()
        {
            if (this.grids.Count > 0)
            {
                return (XfsGrid)this.grids[0];
            }
            return null;
        }
        public void Add(XfsGrid grid)
        {
            this.grids.Add(grid);
            this.grids.Sort();
        }
        public void Remove(XfsGrid grid)
        {
            this.grids.Remove(grid);
            this.grids.Sort();
        }
    }
}