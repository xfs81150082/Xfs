using System;
using System.Collections;
using System.Diagnostics;
using Xfs;

namespace XfsNodeServer
{
    class ServerTest : XfsSystem
    {
        public ServerTest()
        {
        }

        public override void XfsAwake()
        {
            //GetGrids();
            //GetStartGoal();
        }
        public override void XfsUpdate()
        {
            //ResTimeGet();
            //TestPaths();
        }

        Stopwatch TmTime = new Stopwatch();
        //TmSoulerItem SoulerItem = new TmSoulerItem();
        ArrayList Paths { get; set; } = new ArrayList();
        bool iscan = true;
        void TestPaths()
        {
            ////Console.WriteLine(" grids:" + grids.Length);
            //Paths = SoulerItem.GetComponent<TmAstarComponent>().paths;
            //if (Paths != null && Paths.Count > 0 && iscan)
            //{
            //    Console.WriteLine(Paths.Count);
            //    for (int i = 0; i < 5; i++)
            //    {
            //        TmGrid grid = (TmGrid)Paths[i];
            //        Console.WriteLine("(x,z): " + grid.x + " , " + grid.z);
            //    }
            //    iscan = false;
            //}
        }

        #region
        XfsGrid[,] grids { get; set; }
        XfsGrid start { get; set; }
        XfsGrid goal { get; set; }
        int row = 100;
        int cilumn = 100;
        int time = 0;
        int resTime = 120;
        void ResTimeGet()
        {
            time += 1;
            if (time == resTime)
            {
                GetStartGoal2();
                //SoulerItem.GetComponent<TmAstarComponent>().isCan = true;
                //Console.WriteLine(TmTimerTool.CurrentTime() + " ServerTest53: " + SoulerItem.GetComponent<TmAstarComponent>().isCan);
            }

            if (time == resTime * 2)
            {
                GetStartGoal();
                //SoulerItem.GetComponent<TmAstarComponent>().isCan = true;
                //Console.WriteLine(TmTimerTool.CurrentTime() + " ServerTest59: " + SoulerItem.GetComponent<TmAstarComponent>().isCan);
                time = 0;
            }
        }
        void GetStartGoal()
        {
            start = new XfsGrid();
            start.x = 30;
            start.z = 30;
            goal = new XfsGrid();
            goal.x = 60;
            goal.z = 20;
            UpdateStartGoal();
            iscan = true;
        }
        void GetStartGoal2()
        {
            start = new XfsGrid();
            start.x = 20;
            start.z = 30;
            goal = new XfsGrid();
            goal.x = 60;
            goal.z = 30;
            UpdateStartGoal();
            iscan = true;
        }
        void UpdateStartGoal()
        {
            //SoulerItem.GetComponent<TmAstarComponent>().start = start;
            //SoulerItem.GetComponent<TmAstarComponent>().goal = goal;
            //SoulerItem.GetComponent<TmAstarComponent>().grids = grids;
            //SoulerItem.GetComponent<TmSouler>().RoleType = RoleType.Booker;
        }
        void GetGrids()
        {
            grids = new XfsGrid[100, 100];
            for (int i = 0; i < grids.GetLength(0); i++)
            {
                for (int j = 0; j < grids.GetLength(1); j++)
                {
                    grids[i, j] = new XfsGrid();
                    grids[i, j].x = j;
                    grids[i, j].z = i;
                    if (j == 40 && i < 50 && i > 10)
                    {
                        grids[i, j].bObstacle = true;
                    }
                }
            }
        }
        #endregion






    }
}