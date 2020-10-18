using System;
namespace Xfs
{
    class XfsAstarSystem : XfsSystem
    {
        public override void BeginInit()
        {
            ValTime = 400;
        }
        public override void XfsAwake()
        {
            AddComponent(new XfsAstarComponent());
            //AddComponent(new TmSouler());
        }
        public override void XfsUpdate()
        {
            foreach (XfsEntity tem in GetTmEntities())
            {
                FindPaths(tem);
            }
        }
        XfsAstar Astar { get; set; } = new XfsAstar();
        void FindPaths(XfsEntity entity)
        {
            XfsAstarComponent path = entity.GetComponent<XfsAstarComponent>();
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