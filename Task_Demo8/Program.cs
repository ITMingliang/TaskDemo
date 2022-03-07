using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Demo8
{
    public class Program
    {
        /// <summary>
        ///  并行任务
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //泛型类
            ConcurrentStack<int> stack = new ConcurrentStack<int>();
            var t1 = new Task(() =>
            {
                stack.Push(1);
                stack.Push(2);
            });
            t1.Start();
            Console.WriteLine(DateTime.Now.ToString());

            //创建一个在目标 Task 完成时异步执行的延续任务
            //t2 t3并行执行
            var t2 = t1.ContinueWith((task) =>
            {
                int result;
                stack.TryPop(out result);
                Console.WriteLine("Task t2  pop result={0},Thread id {1}，Time: {2}", result, Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            });

            //t2,t3并行执行
            var t3 = t1.ContinueWith(t =>
            {
                int result;
                stack.TryPop(out result);
                Console.WriteLine("Task t3  pop result={0},Thread id {1},Time: {2}", result, Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            });
            Task.WaitAll(t2, t3);

            //t4串行执行
            var t4 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("当前集合元素个数：{0},Thread id {1}", stack.Count, Thread.CurrentThread.ManagedThreadId);
            });
            t4.Wait();
           //Task.WaitAll(t4);
            Console.ReadLine();
        }
    }
}
