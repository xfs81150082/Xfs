using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XfsConsoleTest.TestAttribute;
using XfsConsoleTest.TestAwait;

namespace XfsConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //new TestAttributeInit();     ///2020.11.5 测试Attribute属性使用方法
            //new TestAwaitInit();         ///2020.11.5 测试Attribute属性使用方法
            //new TestTaskInit();          ///2020.11.6 测试TestTaskInit使用方法
            new TestActionInit();          ///2020.11.7 测试TestTaskInit使用方法




            Console.ReadKey();     ///2020.11.5 控制台等待 输入。。。
            Console.ReadLine();    ///2020.11.5 控制台等待 输入。。。


        }
    }
}
