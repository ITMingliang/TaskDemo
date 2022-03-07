using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Demo3
{
    public class Program
    {
        static  void Main(string[] args)
        {
            //Test1();
             Test2();
            Console.ReadKey();
        }


        private static async void Test1()
        {
            Console.WriteLine("开始 线程" + Thread.CurrentThread.ManagedThreadId);
            await Task.Run(() =>
            {
                Console.WriteLine("进入 线程" + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("退出 线程" + Thread.CurrentThread.ManagedThreadId);
            });
            //Console.WriteLine("退出 线程" + Thread.CurrentThread.ManagedThreadId);
        }


        private static async Task Test2()
        {
            Console.WriteLine("开始 线程" + Thread.CurrentThread.ManagedThreadId);
            var t = Task.Run(() =>
            {
                Console.WriteLine("进入 线程" + Thread.CurrentThread.ManagedThreadId);
            });

            Task.WaitAll(t);

            Console.WriteLine("退出 线程" + Thread.CurrentThread.ManagedThreadId);
        }


        private static  void Test3()
        {
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine("进行 线程" + Thread.CurrentThread.ManagedThreadId);
            }, 
           //设置线程长时间运行
           //设置线程长时间运行
            TaskCreationOptions.LongRunning);
        }

    }
}
