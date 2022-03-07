using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Demo6
{
    public class Program
    {
        /// <summary>
        /// 带返回值的Task<int>案例代码
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            TaskMethod("main task");
            Task<int> task = CreateTask("task1");
            task.Start();
            //此处取出来Task的执行结果
            int result = task.Result;
            Console.WriteLine("task1 result is :" + result);

            task = CreateTask("task2");
            task.RunSynchronously();//在当前主线程中同步执行task，线程的ID和主线程的一致
            result = task.Result;
            Console.WriteLine("task2 result is :" + result);


            task = CreateTask("task3");
            Console.WriteLine(task.Status);
            task.Start();
            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(1000);
            }

            //常规使用方法
            task = new Task<int>(() => Getsum());
            task.Start();
            Console.WriteLine("主线程执行其他任务");
            task.Wait();
            Console.WriteLine("获取task执行结果" + task.Result.ToString());
            Console.ReadLine();

        }
        static Task<int> CreateTask(string name)
        {
            return new Task<int>(() => TaskMethod(name));
        }
        static int TaskMethod(string name)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(2));//sleep2秒
            return 42;
        }
        static int Getsum()
        {
            int sum = 0;
            Console.WriteLine("使用Task执行异步操作.");
            for (int i = 0; i < 100; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
}
