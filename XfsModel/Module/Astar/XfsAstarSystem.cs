using System;
namespace Xfs
{
    [XfsObjectSystem]
    class XfsAstarSystem : XfsUpdateSystem<XfsAstarComponent>
    {
        public override void Update(XfsAstarComponent self)
        {
            FindPaths(self);
        }
      
        XfsAstar Astar { get; set; } = new XfsAstar();
        void FindPaths(XfsAstarComponent path)
        {
            if (!path.isCan) return;
            //if (entity.GetComponent<TmSouler>().RoleType == RoleType.Engineer || path.IsKey) return;
            if (path.start != null && path.goal != null && path.grids != null && path.grids.Length > 0)
            {               
                path.paths = Astar.FindPath(path.start, path.goal, path.grids);
                path.start = null;

                path.lastGoal = new XfsGrid(path.goal);
                path.isPath = true;
            }
        }

     
    }
}