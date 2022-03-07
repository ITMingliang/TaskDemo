using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Demo2
{
    public class Program
    {
        static void Main(string[] args)
        {

            // Task.Factory.StartNew
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("1.开始");
                Task.Delay(5000).Wait();
                Console.WriteLine("1.执行一次");
            });


            // Task.Run
            Task.Run(() =>
            {
                Console.WriteLine("2.开始");
                Task.Delay(5000).Wait();
                Console.WriteLine("2.执行一次");
            });

            //var t = Test1();
            //var t2 = Test2();
            Console.WriteLine("结束");
            Console.ReadKey();
        }

        public static async Task Test1()
        {
            await  Task.Run( async () =>
            {
                Console.WriteLine("3.开始");
                await Task.Delay(5000);
                Console.WriteLine("3.执行一下");
            } );
            Console.WriteLine("3.结束");
        }

        public static async Task Test2()
        {

            Console.WriteLine("4.开始");
            await Task.Delay(5000);
            Console.WriteLine("4.执行一下");
            Console.WriteLine("4.结束");
        }
    }
}
