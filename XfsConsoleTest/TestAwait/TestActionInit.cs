using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XfsConsoleTest.TestAwait
{
    public class TestActionInit
    {

        public TestActionInit()
        {
            TestAction1();


        }

        //Dictionary<string ,Action<>>
        void TestCall2()
        {




        }



        void TestAction1()
        {
            string myName = "CC";
            GetFullInfo(myName, ss => Console.WriteLine(ss));
        }
        void GetFullInfo(string yourname, Action<string> action)
        {
            string firstStr = "Welcome to cnblogs ";
            action(firstStr + yourname);
        }


    }
}
