using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    internal class Semaphore_16_02_2023
    {
        static Mutex mutex = new Mutex();
        static int max = 0;
        static Random random = new Random();

        public static void Task()
        {
            //var sem = new Semaphore(3, 6, "Sem");
            //for (int i = 0; i < 10; i++)
            //{
            //    ThreadPool.QueueUserWorkItem(Do, sem);
            //}

            //var arr = new[] { 1, 2, 3, 4, 5 };
            //ThreadPool.QueueUserWorkItem(o =>
            //{
            //    mutex.WaitOne();
            //    var arr = (int[])o;
            //    Random random = new Random();
            //    for (int i = 0; i < arr.Length; i++)
            //    {
            //        arr[i] += random.Next(10);
            //        Console.WriteLine(arr[i]);
            //    }
            //    mutex.ReleaseMutex();
            //}, arr);
            //ThreadPool.QueueUserWorkItem(o =>
            //{
            //    Thread.Sleep(1000);
            //    mutex.WaitOne();
            //    var arr = (int[])o;
            //    max = arr.Max();
            //    Console.WriteLine($"Max: {max}");
            //    mutex.ReleaseMutex();
            //}, arr);

            //Thread.Sleep(2000);
            //mutex.WaitOne();
            //foreach(var i in arr)
            //{
            //    Console.Write($"{i} ");
            //}
            //Console.WriteLine($"Max: {max}");
            //mutex.ReleaseMutex();

            var sem = new Semaphore(3, 3, "Sem");
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(o =>
                {
                    var sem = (Semaphore)o;
                    sem.WaitOne();
                    var id = Thread.CurrentThread.ManagedThreadId;
                    //var arr = new int[4];
                    //for (int i = 0; i < 3; i++)
                    //{
                    //    arr[i] = random.Next(10);
                    //}
                    //Thread.Sleep(1000);
                    //Console.WriteLine($"Man {id} has entered the lift.{Environment.NewLine}{id}: {String.Join(" ", arr)}" +
                    //    $"{Environment.NewLine}Man {id} has left the lift.{Environment.NewLine}");
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(200);
                        Console.WriteLine($"{id} - {random.Next(10)}");
                    }

                    sem.Release();
                }, sem);
            }
        }



        static void Do(object state)
        {
            var sem = (Semaphore)state;
            sem.WaitOne();
            var id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Man {id} has entered the lift.");
            Thread.Sleep(3000);
            Console.WriteLine($"Man {id} has left the lift.");
            sem.Release();
        }
    }
}
