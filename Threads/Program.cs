using System.Data.SqlTypes;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parallel_24_02_2023.Task3();
            //PressAnyKey();
        }

        public static void PressAnyKey(bool showMessage = false)
        {
            if (showMessage)
                Console.WriteLine($"{Environment.NewLine}[Press any key to continue]");
            Console.ReadKey();
            Console.Clear();
        }
    }
}