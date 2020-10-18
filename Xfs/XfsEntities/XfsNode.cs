using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xfs
{
    public class XfsNode : XfsComponent
    {
        public XfsNodeType Type { get; set; }
        public XfsNode() { }
        public XfsNode(XfsNodeType type)
        {
            Type = type;
        }     
    }
    public enum XfsNodeType
    {
        Login,
        Gate,
        Node1,
        Node2,
        Node3,
        Node4,
        Game,
        Chat,
        War,
        BD,
        All,
        None,
    }
}