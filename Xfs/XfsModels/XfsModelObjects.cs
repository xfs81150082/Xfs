using System;
using System.Collections;
using System.Collections.Generic;
namespace Xfs
{
    public static class XfsModelObjects
    {
        public static List<string> Tests { get; set; } = new List<string>();
        public static Dictionary<string, XfsUser> Users { get; set; } = new Dictionary<string, XfsUser>();
        public static Dictionary<int, TmSouler> Soulers { get; set; } = new Dictionary<int, TmSouler>();
        public static Dictionary<int, TmSoulerDB> Bookers { get; set; } = new Dictionary<int, TmSoulerDB>();
        public static Dictionary<int, TmSoulerDB> Teachers { get; set; } = new Dictionary<int, TmSoulerDB>();
        public static Dictionary<int, TmSoulerDB> Engineers { get; set; } = new Dictionary<int, TmSoulerDB>();
        public static TmSoulerDB Engineer { get; set; }
        public static XfsGrid[,] Grids { get; set; }
        public static Dictionary<int, XfsGridMap> GridMaps { get; set; } = new Dictionary<int, XfsGridMap>();


    }
}