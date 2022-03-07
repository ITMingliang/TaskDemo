using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Cancel
{
    public class Program
    {
        /// <summary>
        ///  取消任务
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var task = new Task<int>(() => TaskMethod("task 1", 10, cts.Token), cts.Token);//只是创建，并没有进行Start执行
            Console.WriteLine(task.Status);
            Console.ReadLine();

            cts.Cancel();
            Console.WriteLine(task.Status);
            Console.WriteLine("task1已经被取消！");

            cts = new CancellationTokenSource();
            task = new Task<int>(() => TaskMethod("task2 ", 10, cts.Token), cts.Token);
            task.Start();
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine(task.Status);
            }
            cts.Cancel();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(500);
                Console.WriteLine(task.Status);
            }
            Console.ReadLine();
        }
        static int TaskMethod(string name, int seconds, CancellationToken token)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                  name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            for (int i = 0; i < seconds; i++)
            {

                Thread.Sleep(500);
                if (token.IsCancellationRequested)
                {
                    return -1;
                }
                Console.WriteLine("TaskMethod....");
            }
            return 42 * seconds;
        }
    }
}
