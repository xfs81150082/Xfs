using System.Collections;
using System.Collections.Generic;
namespace Xfs
{
    public static class XfsObjects
    {
        public static ArrayList Entities { get; set; } = new ArrayList();                                                      //服务器 类 实例集合
        public static Dictionary<string, XfsComponent> Components { get; set; } = new Dictionary<string, XfsComponent>();
        public static List<string> Logs { get; set; } = new List<string>();
        
    }
}