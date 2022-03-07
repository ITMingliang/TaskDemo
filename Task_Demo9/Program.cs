using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_Demo9
{
    public class Program
    {
        /// <summary>
        ///  子任务
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Task<string[]> parent = new Task<string[]>(() =>
            {
                string[] array = new string[2];
                //子任务
                new Task(() => { array[0] = "result1"; Thread.Sleep(2000); }, TaskCreationOptions.AttachedToParent).Start();//依附于父进程执行
                new Task(() => { array[1] = "result2"; Thread.Sleep(2000); }, TaskCreationOptions.AttachedToParent).Start();
                return array;
            });

            parent.ContinueWith((task) =>
            {
                foreach (string str in task.Result)
                {
                    Console.WriteLine("结果：" + str);
                }

            });

            parent.Start();
            Console.WriteLine("主线程开始");
            parent.Wait();//wait只能等到父Task结束，不能等到父线程的ContinueWith结束
            Console.WriteLine("主线程结束");
            Console.ReadLine();
        }
    }
}
