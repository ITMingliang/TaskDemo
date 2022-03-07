using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Demo4
{
    public class Program
    {
        //创建Task任务的三种方式（无返回值）
        static void Main(string[] args)
        {
            
            //方法一
            Task task1 = new Task(() =>
            {
                Console.WriteLine("方法一：异步任务");
                Thread.Sleep(2000);

            });
            task1.Start();
            Task.WaitAll(task1);
            //方法二
            Task.Run(() =>
            {
                Console.WriteLine("方法二：异步任务");
            });
            //方法三
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("方法三：通过工厂创建异步任务对象");
            });


            //参数传递
            int val = 5;

            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("测试StartNew：无参数");
            });

            //形参定义成object类型
            Task.Factory.StartNew(a =>
            {
                Console.WriteLine("测试StartNew：参数值" + (int)a);
            }, val);

            Console.ReadLine();
        }
    }
}
