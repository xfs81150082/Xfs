using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XfsConsoleTest.TestAttribute
{
    /// 一个自定义特性 BugFix 被赋给类及其成员
    [AttributeUsage(
        AttributeTargets.Class |           ///规定了特性能被放在class的前面
        AttributeTargets.Constructor |     ///规定了特性能被放在构造函数的前面
        AttributeTargets.Field |           ///规定了特性能被放在域的前面
        AttributeTargets.Method |          ///规定了特性能被放在方法的前面
        AttributeTargets.Property,         ///规定了特性能被放在属性的前面
        AllowMultiple = true               ///这个属性标记了我们的定制特性能否被重复放置在同一个程序实体前多次。
        )]            
    public class DeBugInfo : Attribute    //继承了预定义特性后的自定义特性
    {
        private int bugNo;
        private string developer;
        private string lastReview;
        public string message;


        public DeBugInfo(int bg, string dev, string d)    ///构造函数，接收三个参数并赋给对应实例
        {
            this.bugNo = bg;
            this.developer = dev;
            this.lastReview = d;
        }

        public int BugNo
        {
            get
            {
                return bugNo;
            }
        }
        public string Developer
        {
            get
            {
                return developer;
            }
        }
        public string LastReview
        {
            get
            {
                return lastReview;
            }
        }
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

    }
}
