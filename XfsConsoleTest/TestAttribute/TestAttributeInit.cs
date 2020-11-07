using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XfsConsoleTest.TestAttribute
{
    public class TestAttributeInit
    {
        public TestAttributeInit()
        {
            //System.Reflection.MemberInfo info = typeof(TestMyClass);
            //object[] attributes = info.GetCustomAttributes(true);
            //for (int i = 0; i < attributes.Length; i++)
            //{
            //    Console.WriteLine(attributes[i]);
            //    Console.WriteLine(info);
            //}

            Rectangle r = new Rectangle(4.5, 7.5);  ///实例化
            r.Display();

            Type type = typeof(Rectangle);          ///让type对应这个Rectangle类

            /// 遍历 Rectangle 类的特性
            foreach (Object attributes in type.GetCustomAttributes(false))  ///遍历Rectangle的所有特性
            {
                DeBugInfo dbi = (DeBugInfo)attributes;    ///强制转换
                if (null != dbi)
                {
                    Console.WriteLine("Bug no: {0}", dbi.BugNo);
                    Console.WriteLine("Developer: {0}", dbi.Developer);
                    Console.WriteLine("Last Reviewed: {0}", dbi.LastReview);
                    Console.WriteLine("Remarks: {0}", dbi.Message);
                    Console.WriteLine(type.GetCustomAttributes(false).Length);
                }
            }

            Console.ReadKey();

            /// 遍历方法特性
            foreach (MethodInfo m in type.GetMethods())    ///遍历Rectangle类下的所有方法
            {
                Console.WriteLine(type.GetMethods().Length);

                Console.ReadKey();

                foreach (Attribute a in m.GetCustomAttributes(true))      ///遍历每个方法的特性
                {
                    Console.WriteLine(m.GetCustomAttributes(false).Length);
                    Console.WriteLine(a);
                    Console.ReadKey();
                    DeBugInfo dbi = a as DeBugInfo;    //通过拆装箱代替强制转换
                                                       //通过 object 声明对象，是用了装箱和取消装箱的概念.
                                                       //也就是说 object 可以看成是所有类型的父类。
                                                       //因此 object 声明的对象可以转换成任意类型的值。
                                                       
                    if (null != dbi)
                    {
                        Console.WriteLine("Bug no: {0}, for Method: {1}", dbi.BugNo, m.Name);
                        Console.WriteLine("Developer: {0}", dbi.Developer);
                        Console.WriteLine("Last Reviewed: {0}", dbi.LastReview);
                        Console.WriteLine("Remarks: {0}", dbi.Message);
                    }
                }
            }



        }



    }
}
