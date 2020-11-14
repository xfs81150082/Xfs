using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    public abstract class XfsBranch : XfsBtNode
    {
        protected int activeChild;
        protected List<XfsBtNode> children = new List<XfsBtNode>();
        public virtual XfsBranch OpenBranch(params XfsBtNode[] children)
        {
            for (var i = 0; i < children.Length; i++)
            {
                this.children.Add(children[i]);
            }
            return this;
        }

        public List<XfsBtNode> Children()
        {
            return children;
        }

        public int ActiveChild()
        {
            return activeChild;
        }

        public virtual void ResetChildren()
        {
            activeChild = 0;
            for (var i = 0; i < children.Count; i++)
            {
                XfsBranch b = children[i] as XfsBranch;
                if (b != null)
                {
                    b.ResetChildren();
                }
            }
        }
    }



}
