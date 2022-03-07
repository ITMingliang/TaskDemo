using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Demo7
{
    public class Program
    {
        /// <summary>
        ///  async/await实现带返回值task
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            Task<int> task = AsyncMethod();
            Console.WriteLine("主线程执行其他处理,线程id:" + Thread.CurrentThread.ManagedThreadId);
            for (int i = 1; i <= 3; i++)
                Console.WriteLine("Call Main()");
            int result = task.Result;//阻塞主线程，等待子线程执行结束
            Console.WriteLine("任务执行结果：{0}", result);
            Console.ReadLine();
        }

        //异步方法
        async static Task<int> AsyncMethod()
        {
            Console.WriteLine("使用Task执行异步操作.线程id:" + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000);
            int sum = 0;
            Console.WriteLine("使用Task执行异步操作.线程id:" + Thread.CurrentThread.ManagedThreadId);
            for (int i = 0; i < 100; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
}
