using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    internal class FirstLesson_09_02_2023
    {
        static int max = 0;
        static int min = 0;
        static double avg = 0;

        static bool isMaxReady = false;
        static bool isMinReady = false;
        static bool isAvgReady = false;

        static object locker = new();

        static void Main1(string[] args)
        {
            Threads_14_02_2023.Task();
            Program.PressAnyKey();

            var m = new Mutex_14_02_2023();
            m.Task();

            //string str = Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString();
            //var mutex = new Mutex(true);

            var rand = new Random();
            var list = new List<int>(10000);
            for (int i = 0; i < 10000; i++)
            {
                list.Add(rand.Next(10000));
            }

            CalculateWithThreads(list);
            Console.WriteLine();
            Program.PressAnyKey();
            DropBools();

            CalculateWithThreadPool(list);
            Program.PressAnyKey();
        }

        private static void CalculateWithThreads(List<int> list)
        {
            new Thread(() => { max = list.Max(); isMaxReady = true; }).Start();
            new Thread(() => { min = list.Min(); isMinReady = true; }).Start();
            new Thread(() => { avg = list.Average(); isAvgReady = true; }).Start();

            while (true)
            {
                if (isMaxReady && isMinReady && isAvgReady)
                {
                    Console.WriteLine($"Max: {max}\nMin: {min}\nAvg: {avg}");
                    break;
                }
            }
        }

        private static void CalculateWithThreadPool(List<int> list)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                max = list.Max();
                WriteToFile("Max: " + Convert.ToString(max));
                isMaxReady = true;
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                min = list.Min();
                WriteToFile("Min: " + Convert.ToString(min));
                isMinReady = true;
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                avg = list.Average();
                WriteToFile("Avg: " + Convert.ToString(avg));
                isAvgReady = true;
            });

            while (true)
            {
                if (isMaxReady && isMinReady && isAvgReady)
                {
                    Console.WriteLine($"Max: {max}\nMin: {min}\nAvg: {avg}");
                    break;
                }
            }
        }

        static void DropBools()
        {
            isMaxReady = false;
            isMinReady = false;
            isAvgReady = false;
        }

        static void WriteToFile(string line)
        {
            lock (locker)
            {
                using (var sw = new StreamWriter("Results.txt", true))
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
