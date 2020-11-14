using System.Collections;
namespace Xfs
{
    public class XfsAstarComponent : XfsComponent
    {
        public XfsGrid start { get; set; }
        public XfsGrid goal { get; set; }
        public XfsGrid lastGoal { get; set; }
        public XfsGrid[,] grids { get; set; }
        public ArrayList paths { get; set; }
        public int time = 1;
        public bool isCan { get; set; } = true;
        public bool isPath { get; set; } = false;
        public bool IsKey { get; set; } = false;
    }
}
