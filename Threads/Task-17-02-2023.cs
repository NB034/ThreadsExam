using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    internal class Task_17_02_2023
    {
        #region Task1
        public static void Task1()
        {
            Task.Run(PrintTime);
            var t1 = new Task(PrintTime); t1.Start();
            var t2 = Task.Factory.StartNew(PrintTime);

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(1000);
                Console.Clear();
            }
        }

        private static void PrintTime()
        {
            while (true)
            {
                Thread.Sleep(1000);
                Console.WriteLine(DateTime.Now);
            }
        }
        #endregion

        #region Task2_3
        static public void Task2_3()
        {
            var random = new Random();
            int a = random.Next(100);
            int b = random.Next(100, 1000);
            Console.WriteLine($"Range: {a} - {b}");
            var t = Task<int>.Factory.StartNew(() =>
            {
                int res = 0;
                for (int i = a; i <= b; i++)
                {
                    if(IsPrime(i))
                        res++;
                }
                return res;
            });
            Console.WriteLine($"Amount of simple numbers: { t.Result}");
        }

        private static bool IsPrime(int num)
        {
            if(num < 1) return false;

            int count = 0;
            for (int i = 2; i <= num; i++)
            {
                if(num % i == 0)
                {
                    count++;
                }
                if (count > 1) return false;
            }
            return true;
        }
        #endregion

        #region Task4
        static int[] array = new int[5];
        public static void Task4()
        {
            var random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(100);
            }

            Console.Write("Array:");
            foreach (var item in array)
            {
                Console.Write($" {item}");
            }
            Console.WriteLine();

            Task<int>[] tArr = new Task<int>[4];
            tArr[0] = Task.Factory.StartNew(() => { return array.Min(); });
            tArr[1] = Task.Factory.StartNew(() => { return array.Max(); });
            tArr[2] = Task.Factory.StartNew(() => { return Convert.ToInt32(array.Average()); });
            tArr[3] = Task.Factory.StartNew(() => { return array.Sum(); });

            Console.Write("Results:");
            foreach (var item in tArr)
            {
                Console.Write($" {item.Result}");
            }

            Console.ReadKey();
        }
        #endregion
    }
}
