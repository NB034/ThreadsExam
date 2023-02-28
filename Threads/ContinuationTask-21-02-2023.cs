using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    internal class ContinuationTask_21_02_2023
    {
        public static void Task1()
        {
            int size = 40;
            Task.Run(() =>
            {
                int[] arr = new int[size];
                var random = new Random();
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = random.Next(size);
                    Console.Write($"{arr[i]} ");
                }
                Console.WriteLine();
                return arr;

            }).ContinueWith(task =>
            {
                Array.Sort(task.Result);
                foreach (var item in task.Result)
                {
                    Console.Write($"{item} ");
                }
                Console.WriteLine();
                return task.Result;
            }).ContinueWith(task =>
            {
                var random = new Random();
                var num = random.Next(size);
                Console.WriteLine($"Random number: {num}{Environment.NewLine}" +
                    $"Index of random number: {Array.IndexOf(task.Result, num)}");
            });

            Console.Read();
        }
    }
}
