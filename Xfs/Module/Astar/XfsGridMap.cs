using System;
using System.Collections.Generic;
namespace Xfs
{
    public class XfsGridMap : XfsEntity
    {
        public int MapId { get; set; }
        public string senceName { get; set; }
        public double withSize { get; set; }
        public double lentgSize { get; set; }
        public double gridSize { get; set; }
        public int raw { get; set; }
        public int column { get; set; }
        public XfsGrid[,] grids { get; set; }
        public List<XfsGrid> gridList { get; set; }
        public List<XfsObstacle> obstacles { get; set; }
    }
}
