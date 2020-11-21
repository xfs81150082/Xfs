using System;
using System.Collections.Generic;
using System.Text;

namespace Xfs
{
    //[ObjectSystem]
    //public class TriggerAwakeSystem : AwakeSystem<Trigger, string,bool>
    //{
    //    public override void Awake(Trigger self, string a,bool b)
    //    {
    //        self.Awake(a,b);
    //    }
    //}

    public class XfsTrigger : XfsBtNode
    {
        //Animator animator;
        int id;
        string triggerName;
        bool set = true;

        public void Awake(string a, bool b)
        {
            //this.id = Animator.StringToHash(name);
            //this.animator = animator;
            this.triggerName = a;
            this.set = b;
        }

        //if set == false, it reset the trigger istead of setting it.
        public XfsTrigger(/*Animator animator,*/ string name, bool set = true)
        {
            //this.id = Animator.StringToHash(name);
            //this.animator = animator;
            //this.triggerName = name;
            //this.set = set;
        }

        public override XfsBtState Tick()
        {
            //if (set)
            //    //animator.SetTrigger(id);
            //else
            //    //animator.ResetTrigger(id);

            return XfsBtState.Success;
        }

        public override string ToString()
        {
            return "Trigger : " + triggerName;
        }
    }

}
