using System.Collections.Generic;

namespace Xfs
{
    public static class XfsTaskHelper
    {
        private class XfsCoroutineBlocker
        {
            private int count;

            private List<XfsTaskCompletionSource> tcss = new List<XfsTaskCompletionSource>();

            public XfsCoroutineBlocker(int count)
            {
                this.count = count;
            }

            public async XfsTask WaitAsync()
            {
                --this.count;
                if (this.count < 0)
                {
                    return;
                }

                if (this.count == 0)
                {
                    List<XfsTaskCompletionSource> t = this.tcss;
                    this.tcss = null;
                    foreach (XfsTaskCompletionSource ttcs in t)
                    {
                        ttcs.SetResult();
                    }

                    return;
                }

                XfsTaskCompletionSource tcs = new XfsTaskCompletionSource();
                tcss.Add(tcs);
                await tcs.Task;
            }
        }

        public static async XfsTask WaitAny<T>(XfsTask<T>[] tasks)
        {
            XfsCoroutineBlocker coroutineBlocker = new XfsCoroutineBlocker(2);
            foreach (XfsTask<T> task in tasks)
            {
                RunOneTask(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();

            async XfsVoid RunOneTask(XfsTask<T> task)
            {
                await task;
                await coroutineBlocker.WaitAsync();
            }
        }
        public static async XfsTask WaitAny(XfsTask[] tasks)
        {
            XfsCoroutineBlocker coroutineBlocker = new XfsCoroutineBlocker(2);
            foreach (XfsTask task in tasks)
            {
                RunOneTask(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();

            async XfsVoid RunOneTask(XfsTask task)
            {
                await task;
                await coroutineBlocker.WaitAsync();
            }
        }
        public static async XfsTask WaitAll<T>(XfsTask<T>[] tasks)
        {
            XfsCoroutineBlocker coroutineBlocker = new XfsCoroutineBlocker(tasks.Length + 1);
            foreach (XfsTask<T> task in tasks)
            {
                RunOneTask(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();

            async XfsVoid RunOneTask(XfsTask<T> task)
            {
                await task;
                await coroutineBlocker.WaitAsync();
            }
        }
        public static async XfsTask WaitAll(XfsTask[] tasks)
        {
            XfsCoroutineBlocker coroutineBlocker = new XfsCoroutineBlocker(tasks.Length + 1);
            foreach (XfsTask task in tasks)
            {
                RunOneTask(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();

            async XfsVoid RunOneTask(XfsTask task)
            {
                await task;
                await coroutineBlocker.WaitAsync();
            }
        }

    }
}