using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Demo1
{
    public  class Program
    {
        static void Main(string[] args)
        {
            do1();
           // do2();
            Task.Delay(-1).Wait();
         
            Console.ReadKey();
        }


        //Task.Run会自动执行Unwrap操作，但是Task.Factory.StartNew不会
        static async void do1()
        {
            //运行嵌套的Task
            //Task返回Task<Task<string>>
            var result = await Task.Run<Task<string>>(() =>
              {
                  var task = Task.Run<string>(() =>
                    {
                        Task.Delay(1000);
                        return "Men";
                    });

                  //第一个返回为Task<string>
                  return task;  
              });


            Console.WriteLine(result);
            //第二个await返回为string
            Console.WriteLine(await result);
        }

        static async void do2()
        {
            //运行嵌套的Task
            //Task返回Task<Task<string>>
            var result = await Task.Run<Task<string>>(() =>
            {
                var task = Task.Run<string>(() =>
                {
                    Task.Delay(1000);
                    return "Men";
                });

                //第一个返回为Task<string>
                return task;
                //Unwrap会把嵌套的Task<Task> 或者Task<Task< T >> 的结果提取出来。
            }).Unwrap();


            Console.WriteLine(result);
            //第二个await返回为string
            //Console.WriteLine(await result);//已经完成
        }

        #region Unwrap

        //Task.Factory.StartNew和Task.Run区别之一就有Task.Run会自动执行Unwrap操作，
        static void do3()
        {
            var task1 = Task.Factory.StartNew(async () => "Mgen");
            var task2 = Task.Run(async () => "Mgen");

            Console.WriteLine(task1.GetType());
            Console.WriteLine(task2.GetType());
        }


        static void doo()
        {
            //int数据
            var ints = Enumerable.Range(1, 10);
            //转换并输出结果
            foreach (var str in ints.Select(async i => await Int2StringAsync(i)))
                Console.WriteLine(str);


            //Select调用异步方法
            // 如果一定要使用async Lambda，则必须将嵌套的Task进行Unwrap

            //使用Task.Factory.StartNew需要进行一个Unwrap
            IEnumerable<string> strs1 = ints.Select(i =>
            Task.Factory.StartNew(async () => await Int2StringAsync(i)).Unwrap().Result);

            //而Task.Run的话，不需要Unwrap:
            IEnumerable<string> strs2 = ints.Select(i =>
            Task.Run(async () => await Int2StringAsync(i)).Result);


        }

        //异步将int转换成string
        static async Task<string> Int2StringAsync(int i)
        {
            return await Task.Run<string>(() => i.ToString());
        }

        #endregion
    }
}
