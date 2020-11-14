using System;
using System.Collections;
namespace Xfs
{
    public class XfsAstar
    {
        private XfsPriorityQueue closedList, openList;
        public ArrayList FindPath(XfsGrid start, XfsGrid goal, XfsGrid[,] grids)
        {
            //if (grids[start.x, start.z].bObstacle && grids[goal.x, goal.z].bObstacle) return null;
            openList = new XfsPriorityQueue();
            closedList = new XfsPriorityQueue();
            openList.Add(start);
            start.G = 0.0;
            start.H = Math.Abs(goal.x - start.x) + Math.Abs(goal.z - start.z) + start.bH;
            start.F = start.G + start.H;
            XfsGrid grid = null;
            while (openList.Length != 0)
            {
                grid = openList.First();
                if (grid == null) return null;
                if (grid.x == goal.x && grid.z == goal.z)
                {
                    return CalculatePath(grid);
                }
                ArrayList neighbours = GetNeighbours(grid, grids);
                for(int i = 0; i < neighbours.Count; i++)
                {
                    XfsGrid neighGrid = (XfsGrid)neighbours[i];
                    if (!closedList.Contains(neighGrid))
                    {
                        double costG = GetCostG(neighGrid, grid);
                        //double costH = Math.Abs(goal.x - neighGrid.x) + Math.Abs(goal.z - neighGrid.z);
                        double costH = GetCostH(neighGrid, goal);
                        neighGrid.G = grid.G + costG;
                        neighGrid.H = costH;
                        neighGrid.F = neighGrid.G + neighGrid.H;
                        neighGrid.parent = grid;
                        if (!openList.Contains(neighGrid))
                        {
                            openList.Add(neighGrid);
                        }
                    }
                }
                closedList.Add(grid);
                openList.Remove(grid);
            }
            if (grid.x != goal.x && grid.z != goal.z)
            {
                Console.WriteLine("Goal Not Find.");
                return null;
            }
            return CalculatePath(grid);
        }
        private ArrayList CalculatePath(XfsGrid goal)
        {
            ArrayList list = new ArrayList();
            while (goal != null)
            {
                list.Add(goal);
                goal = goal.parent;
            }
            list.Reverse();
            return list;
        }
        private double GetCostG(XfsGrid neighour, XfsGrid grid)
        {
            double cost = 1.0;
            if (grid.x != neighour.x && grid.z != neighour.z)
            {
                cost = 1.4;
            }
            return cost;
        }
        private double GetCostH(XfsGrid neighour, XfsGrid goal)
        {
            double cost = 0.0;
            double xx = Math.Abs(goal.x - neighour.x);
            double yy = Math.Abs(goal.z - neighour.z);
            if (xx >= yy)
            {
                cost = xx - yy + yy * 1.4 + neighour.bH;
            }
            else
            {
                cost = yy - xx + xx * 1.4 + neighour.bH;
            }
            return cost;
        }
        private ArrayList GetNeighbours(XfsGrid grid, XfsGrid[,] grids)
        {
            ArrayList list = new ArrayList();
            int row = grid.z;
            int column = grid.x;
            AssignNeighbour(row - 1, column - 1, list, grids);    //左上
            AssignNeighbour(row - 1, column, list, grids);        //左上
            AssignNeighbour(row - 1, column + 1, list, grids);    //左上
            AssignNeighbour(row, column - 1, list, grids);        //左上
            AssignNeighbour(row, column + 1, list, grids);        //左上
            AssignNeighbour(row + 1, column - 1, list, grids);    //左上
            AssignNeighbour(row + 1, column, list, grids);        //左上
            AssignNeighbour(row + 1, column + 1, list, grids);    //左上
            return list;
        }
        private void AssignNeighbour(int row, int column, ArrayList neighbours , XfsGrid[,] grids)
        {
            if (row > -1 && column > -1 && row < grids.GetLength(0) && column < grids.GetLength(1))
            {
                XfsGrid grid = grids[row, column];
                if (!grid.bObstacle)
                {
                    neighbours.Add(grid);
                }
            }
        }          
    }
}