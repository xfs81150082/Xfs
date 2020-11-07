using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XfsConsoleTest.TestAwait
{
    public class TestAwaitInit
    {
        public TestAwaitInit()
        {
            //Test1();
            //Test2();


        }


        void Test2()
        {
            CallerWithAsync();
        }
        async void CallerWithAsync()
        {
            Console.WriteLine("开始执行await：" + DateTime.Now.ToString("yyyy年MM月dd日hh时mm分ss秒", DateTimeFormatInfo.InvariantInfo));
            string result = await GreetingAsync("WPF");
            Console.WriteLine(result);
            Console.WriteLine(DateTime.Now.ToString("等待结束，开始继续执行 " + "yyyy年MM月dd日hh时mm分ss秒", DateTimeFormatInfo.InvariantInfo));
        }
        Task<string> GreetingAsync(string name)
        {
            return Task.Run<string>(() => { return Greeting(name); });
        }
        string Greeting(string name)
        {
            Task.Delay(3000).Wait();
            return $"Hello,{name}";
        }





        void Test1()
        {
            Console.WriteLine("1.同步ThreaID：" + Thread.CurrentThread.ManagedThreadId);
            RunTimeAsync();
            Console.WriteLine("4.异步执行完毕ThreaID：" + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);
            Console.WriteLine("5.同步延迟完毕ThreaID：" + Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();

        }
        async void RunTimeAsync1()
        {
            await RunTimeAsync();
        }
        Task RunTimeAsync()
        {
            Console.WriteLine("2.进入异步ThreaID：" + Thread.CurrentThread.ManagedThreadId);
            Task.Delay(1000);
            Console.WriteLine("3.异步执行后ThreaID：" + Thread.CurrentThread.ManagedThreadId);
            return null;
        }


    }
}
