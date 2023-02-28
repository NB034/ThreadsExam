using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    internal class Parallel_24_02_2023
    {
        static int number = 100;

        public static void Task1()
        {
            BigInteger factorial = 1;
            Parallel.For(1, number, (step) =>
            {
                factorial *= step + 1;
            });

            Console.WriteLine($"Number - {number}, factorial - {factorial}");
            Console.Read();
        }

        public static void Task2()
        {
            Parallel.Invoke(
                Task1,
                () => Console.WriteLine($"Amount of digits in the number: {number.ToString().Length}"),
                () => Console.WriteLine($"Sum of digits in the number: {number.ToString().Select(c => Convert.ToInt32(c) - 48).ToArray().Sum()}"));
        }

        public static void Task3()
        {
            var random = new Random();

            int leftBound = random.Next(1, 11);
            int rightBound = random.Next(leftBound, 12);

            Console.WriteLine($"Bounds: {leftBound} - {rightBound}");

            Parallel.For(leftBound, rightBound, (step) =>
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i <= 10; i++)
                {
                    builder.AppendLine($"{step} * {i} = {step * i}");
                }
                builder.AppendLine("..........");
                WriteToFile(builder.ToString());
            });
        }

        static object locker = new();
        static void WriteToFile(string text)
        {
            lock (locker)
            {
                using (StreamWriter sw = new StreamWriter("MathTable.txt", true))
                {
                    sw.WriteLine(text);
                }
            }
        }
    }
}
