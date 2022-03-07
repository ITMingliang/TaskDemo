using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Demo5
{
    public class Program
    {
        //创建Task任务的三种方式（无返回值）
        static void Main(string[] args)
        {
            var t1 = new Task(() => TaskMethod("task1"));
            var t2 = new Task(() => TaskMethod("task2"));
            t1.Start();
            t2.Start();
            Task.WaitAll(t1, t2);


            Task.Run(() => TaskMethod("task3"));
            Task.Factory.StartNew(TaskMethod, "task4");//参数传递的另外一种形式
            Task.Factory.StartNew(() => TaskMethod("task5"), TaskCreationOptions.LongRunning);//设置长时间运行

            Console.WriteLine("主线程开始执行任务");

            Task task = new Task(() =>
            {
                Console.WriteLine("使用System.Threading.Tasks.Task执行异步操作.");
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine(i);
                }
            });

            task.Start();
            Console.WriteLine("主线程执行其他任务");
            task.Wait();

            Console.ReadLine();
        }
        static void TaskMethod(object taskName)
        {
            Console.WriteLine($"任务名称：{taskName}，线程Id：{Thread.CurrentThread.ManagedThreadId},是否为线程池执行：{Thread.CurrentThread.IsThreadPoolThread}");
        }
    }
}
