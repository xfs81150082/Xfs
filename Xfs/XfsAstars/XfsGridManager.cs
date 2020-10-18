using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Xfs
{
    public class XfsGridManager : XfsComponent
    {
        private Dictionary<string, XfsGridMap> GridMaps = new Dictionary<string, XfsGridMap>();
        void Add(string key, XfsGridMap map)
        {
            XfsGridMap tem;
            GridMaps.TryGetValue(key, out tem);
            if (tem == null)
            {
                GridMaps.Add(key, map);
            }
        }
        XfsGridMap Get(string key)
        {
            XfsGridMap map = null;
            GridMaps.TryGetValue(key, out map);
            return map;
        }
        void Remove(string key)
        {
            XfsGridMap tem;
            GridMaps.TryGetValue(key, out tem);
            if (tem != null)
            {
                GridMaps.Remove(key);
            }
        }
    }
}