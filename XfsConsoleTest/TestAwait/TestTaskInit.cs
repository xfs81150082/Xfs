using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XfsConsoleTest.TestAwait
{
    public class TestTaskInit
    {
        public TestTaskInit()
        {
            //TestTask1();
            //TestTask2();
            //TestTask3();
            //TestTask4();
            //TestTask5();
            //TestTask6();
            //TestTask7();
            //TestTask8();
            //TestTask9();
            //TestTask10();
            //TestTask11();
            //new TestNodeTask();
            //TestTask12();
            //TestTask13();
            //TestTask14();
            //TestTask15();
            //TestTask16();
            //TestTask17();

            TestTask18();


        }



        private delegate string AsynchronousTask18(string threadName);
        //执行的流程是 先执行Test--->Callback--->task.ContinueWith
        void TestTask18()
        {
            Console.WriteLine("Thread TestTask18-42 id: {0}", Thread.CurrentThread.ManagedThreadId);
            AsynchronousTask18 d18 = Test18;
            Console.WriteLine("Option 1");
            Task<string> task = Task<string>.Factory.FromAsync(d18.BeginInvoke("AsyncTaskThread", Callback18, "a delegate asynchronous call"), d18.EndInvoke);

            task.ContinueWith(t17 => Console.WriteLine("Callback is finished, now running a continuation! Result: {0}",t17.Result));

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine("Thread TestTask18-54 id: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(task.Status);
        }
        private string Test18(string threadName)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine("Thread Test18 id: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Is thread pool thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Thread.CurrentThread.Name = threadName;
            return string.Format("Thread name: {0}", Thread.CurrentThread.Name);
        }
        private void Callback18(IAsyncResult ar)
        {
            Console.WriteLine("Starting a callback...");
            Console.WriteLine("State passed to a callbak: {0}", ar.AsyncState);
            Console.WriteLine("Is thread pool thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("Thread Callback18 id: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Thread pool worker thread id: {0}", Thread.CurrentThread.ManagedThreadId);
        }      
        //执行的流程是 先执行Test--->task.ContinueWith
        void TestTask17()
        {
            Console.WriteLine("Thread 82 Id: {0}", Thread.CurrentThread.ManagedThreadId);
            AsynchronousTask17 d17 = Test17;
            Task<string> task = Task<string>.Factory.FromAsync(d17.BeginInvoke, d17.EndInvoke, "AsyncTaskThread", "a delegate asynchronous call");

            Console.WriteLine("Thread 86 Id: {0}", Thread.CurrentThread.ManagedThreadId);
            task.ContinueWith(t => Console.WriteLine("Task is completed, now running a continuation! Result: {0}",t.Result));
            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine(task.Status);
        }  
        private delegate string AsynchronousTask17(string threadName);
        private string Test17(string threadName)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine("Is thread pool thread: {0}", Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Thread.CurrentThread.Name = threadName;
            Console.WriteLine("Thread Test17-102 Id: {0}", Thread.CurrentThread.ManagedThreadId);
            return string.Format("Thread name: {0}", Thread.CurrentThread.Name);
        }
        void TestTask16()
        {
            Console.WriteLine("主程序开始执行。。。。");
            Task<string> task = GetValueFromCache16("0006");
            Console.WriteLine("主程序继续执行。。。。"); 
            string result = task.Result;
            Console.WriteLine("result={0}", result);
        }
        IDictionary<string, string> cache = new Dictionary<string, string>(){{"0001","A"},{"0002","B"},{"0003","C"},{"0004","D"},{"0005","E"},{"0006","F"},};
        Task<string> GetValueFromCache16(string key)
        {
            Console.WriteLine("GetValueFromCache开始执行。。。。"); 
            string result = string.Empty;            //Task.Delay(5000);
            Thread.Sleep(5000);
            Console.WriteLine("GetValueFromCache继续执行。。。。"); 
            if (cache.TryGetValue(key, out result))
            {
                return Task.FromResult(result);
            }
            return Task.FromResult("");
        }
        void TestTask15()
        {
            Task task = ObserveOneExceptionAsync15();
            Console.WriteLine("主线程继续运行........");
            task.Wait();
        }
        async Task ObserveOneExceptionAsync15()
        {
            var task1 = ThrowNotImplementedExceptionAsync();
            var task2 = ThrowInvalidOperationExceptionAsync();
            var task3 = Normal6();
            try
            {                
                //异步的方式
                Task allTasks = Task.WhenAll(task1, task2, task3);
                await allTasks;

                //同步的方式                 
                //Task.WaitAll(task1, task2, task3);
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine("task1 任务报错!");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("task2 任务报错!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("任务报错!");
            }
        }
        async Task ThrowNotImplementedExceptionAsync()
        {
            throw new NotImplementedException();
        }
        async Task ThrowInvalidOperationExceptionAsync()
        {
            throw new InvalidOperationException();
        }
        async Task Normal6()
        {
            await Fun16();
        }        
        Task Fun16()
        {
            return Task.Run(() =>
            {
                for (int i = 1; i <= 10; i++)
                {
                    Console.WriteLine("i={0}", i);
                    Thread.Sleep(200);
                }
            });
        }
        void TestTask14()
        {
            try
            {
                var t1 = new Task<int>(() => TaskMethod14("Task 3", 3));
                var t2 = new Task<int>(() => TaskMethod14("Task 4", 2)); 
                var complexTask = Task.WhenAll(t1, t2); 
                var exceptionHandler = complexTask.ContinueWith(t =>Console.WriteLine("Result: {0}", t.Result),TaskContinuationOptions.OnlyOnFaulted);
                t1.Start();
                t2.Start();
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ex)
            {
                ex.Handle(exception =>
                {
                    Console.WriteLine(exception.Message); return true;
                });
            }
        } 
        int TaskMethod14(string name, int seconds)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            throw new Exception(string.Format("Task {0} Boom!", name));
            return 42 * seconds;
        }
        void TestTask13()
        {
            try
            {
                Task<int> task = Task.Run(() => TaskMethod13("Task 2", 2));
                int result = task.GetAwaiter().GetResult();
                Console.WriteLine("Result: {0}", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Task 2 Exception caught: {0}", ex.Message);
            }
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();
        }
        int TaskMethod13(string name, int seconds)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            //throw new Exception("Boom!");
            return 42 * seconds;
        }
        void TestTask12()
        {
            var cts = new CancellationTokenSource();
            var longTask = new Task<int>(() => TaskMethod("Task 1", 10, cts.Token), cts.Token);
            Console.WriteLine(longTask.Status);
            cts.Cancel();
            Console.WriteLine(longTask.Status);
            Console.WriteLine("First task has been cancelled before execution");

            cts = new CancellationTokenSource();
            longTask = new Task<int>(() => TaskMethod("Task 2", 10, cts.Token), cts.Token);
            longTask.Start();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }
            cts.Cancel();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }

            Console.WriteLine("A task has been completed with result {0}.", longTask.Result);
        }
        int TaskMethod(string name, int seconds, CancellationToken token)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            for (int i = 0; i < seconds; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                if (token.IsCancellationRequested) return -1;
            }
            return 42 * seconds;
        }
        void TestTask11()
        {
            Task<string[]> parent = new Task<string[]>(state =>
            {
                Console.WriteLine(state);
                string[] result = new string[2];
                //创建并启动子任务
                new Task(() => { result[0] = "我是子任务1。"; }, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => { result[1] = "我是子任务2。"; }, TaskCreationOptions.AttachedToParent).Start();
                return result;
            }, "我是父任务，并在我的处理过程中创建多个子任务，所有子任务完成以后我才会结束执行。");
            //任务处理完成后执行的操作
            parent.ContinueWith(t =>
            {
                Array.ForEach(t.Result, r => Console.WriteLine(r));
            });

            //启动父任务
            parent.Start();
            //等待任务结束 Wait只能等待父线程结束,没办法等到父线程的ContinueWith结束
            //parent.Wait(); 
            
            //foreach(var res in parent.Result)
            //{
            //    Console.WriteLine(res);
            //}

            Console.ReadLine();
        }
        void TestTask10()
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();

            //t1先串行
            var t1 = Task.Factory.StartNew(() =>
            {
                stack.Push(1);
                stack.Push(2);
            });

            //t2,t3并行执行
            var t2 = t1.ContinueWith(t =>
            {
                int result;
                stack.TryPop(out result);
                Console.WriteLine("Task t2 result={0},Thread id {1}", result, Thread.CurrentThread.ManagedThreadId);
            });

            //t2,t3并行执行
            var t3 = t1.ContinueWith(t =>
            {
                int result;
                stack.TryPop(out result);
                Console.WriteLine("Task t3 result={0},Thread id {1}", result, Thread.CurrentThread.ManagedThreadId);
            });

            //等待t2和t3执行完
            Task.WaitAll(t2, t3);

            //t7串行执行
            var t4 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("当前集合元素个数：{0},Thread id {1}", stack, Thread.CurrentThread.ManagedThreadId);
            });
            t4.Wait();
        }
        void TestTask9()
        {
            //创建一个任务
            Task<int> task = new Task<int>(() =>
            {
                int sum = 0;
                Console.WriteLine("使用Task执行异步操作.");
                for (int i = 0; i < 100; i++)
                {
                    sum += i;
                }
                return sum;
            });
            //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
            task.Start();
            task.Wait();
            Console.WriteLine("主线程执行其他处理");
            //任务完成时执行处理。
            Task cwt = task.ContinueWith(t =>
            {
                Console.WriteLine("任务完成后的执行结果：{0}", t.Result.ToString());
            });
            cwt.Wait();
        }
        void TestTask8()
        {
            var ret1 = AsyncGetsum8();
            Console.WriteLine("主线程执行其他处理");
            for (int i = 1; i <= 3; i++)
                Console.WriteLine("Call Main()");
            int result = ret1.Result;                  //阻塞主线程
            Console.WriteLine("任务执行结果：{0}", result);
        }
        async Task<int> AsyncGetsum8()
        {
            await Task.Delay(1);
            int sum = 0;
            Console.WriteLine("使用Task执行异步操作.");
            for (int i = 0; i < 100; i++)
            {
                sum += i;
            }
            return sum;
        }
        void TestTask7()
        {
            TaskMethod7("Main Thread Task");
            Task<int> task = CreateTask7("Task 1");
            task.Start();
            int result = task.Result;
            Console.WriteLine("Task 1 Result is: {0}", result);

            task = CreateTask7("Task 2");
            //该任务会运行在主线程中
            task.RunSynchronously();
            result = task.Result;
            Console.WriteLine("Task 2 Result is: {0}", result);

            task = CreateTask7("Task 3");
            Console.WriteLine(task.Status);
            task.Start();

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }

            Console.WriteLine(task.Status);
            result = task.Result;
            Console.WriteLine("Task 3 Result is: {0}", result);

            #region 常规使用方式
            //创建任务
            Task<int> getsumtask = new Task<int>(() => Getsum());
            //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
            getsumtask.Start();
            Console.WriteLine("主线程执行其他处理");
            //等待任务的完成执行过程。
            getsumtask.Wait();
            //获得任务的执行结果
            Console.WriteLine("任务执行结果：{0}", getsumtask.Result.ToString());
            #endregion
        }
        Task<int> CreateTask7(string name)
        {
            return new Task<int>(() => TaskMethod7(name));
        }
         int TaskMethod7(string name)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return 77;
        }
        int Getsum()
        {
            int sum = 0;
            Console.WriteLine("使用Task执行异步操作.");
            for (int i = 0; i < 100; i++)
            {
                sum += i;
            }
            return sum;
        }       
        void TestTask6()
        {
            Task<int> task = CreateTask("Task 1");
            task.Start();
            int result = task.Result;
        }
        private Task<int> CreateTask(string v)
        {
            throw new NotImplementedException();
        }
        void TestTask5()
        {
            Console.WriteLine("主线程执行业务处理.");
            AsyncFunction5();
            Console.WriteLine("主线程执行其他处理");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(string.Format("Main:i={0}", i));
            }
            Console.ReadLine();
        }
        async void AsyncFunction5()
        {
            await Task.Delay(1);
            Console.WriteLine("使用System.Threading.Tasks.Task执行异步操作.");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(string.Format("AsyncFunction:i={0}", i));
            }
        }
        void TestTask4()
        {
            var t1 = new Task(() => TaskMethod4("Task 1"));
            var t2 = new Task(() => TaskMethod4("Task 2"));
            t2.Start();
            t1.Start();
            Task.WaitAll(t1, t2);
            Task.Run(() => TaskMethod4("Task 3"));
            Task.Factory.StartNew(() => TaskMethod4("Task 4"));
            ///标记为长时间运行任务,则任务不会使用线程池,而在单独的线程中运行。
            Task.Factory.StartNew(() => TaskMethod4("Task 5"), TaskCreationOptions.LongRunning);

            #region 常规的使用方式
            Console.WriteLine("主线程执行业务处理.");
            //创建任务
            Task task = new Task(() =>
            {
                Console.WriteLine("使用System.Threading.Tasks.Task执行异步操作.");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(i);
                }
            });
            //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
            task.Start();
            Console.WriteLine("主线程执行其他处理");
            task.Wait();
            #endregion

            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.ReadLine();
        }
        void TaskMethod4(string name)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }
        void TestTask3()
        {
            Console.WriteLine(23);

            Task.Factory.StartNew(() => TaskMethod1("Task 3")); ///直接异步的方法

                                                               ///或者              
            var t3 = Task.Factory.StartNew(() => TaskMethod1("Task 3"));

            Task.WaitAll(t3);  //等待所有任务结束

            Console.WriteLine(30);
        }
        void TestTask2()
        {
            Console.WriteLine(23);

            Task.Run(() => TaskMethod1("Task 2"));

            Console.WriteLine(27);
        }
        void TestTask1()
        {
            var t1 = new Task(() => TaskMethod1("Task 1"));

            Console.WriteLine(24);

            t1.Start();

            Console.WriteLine(28);

            Task.WaitAll(t1);//等待所有任务结束 //注: 任务的状态://Start之前为: Created// Start之后为:WaitingToRun

        }
        void TaskMethod1(string v)
        {
            Console.WriteLine(59 + v);
        }





    }
}
