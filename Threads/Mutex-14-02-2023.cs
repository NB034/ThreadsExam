using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    internal class Mutex_14_02_2023
    {
        Mutex mutex = new();

        public void Task()
        {
            //var thread1 = new Thread(Do);
            //var thread2 = new Thread(Do);

            //thread1.Start();
            //thread2.Start();

            ThreadPool.QueueUserWorkItem(Do, mutex);
            ThreadPool.QueueUserWorkItem(Do, mutex);

            Thread.Sleep(1000);
            Console.Read();
        }

        void Do(object obj)
        {
            var mutex = (Mutex)obj;

            mutex.WaitOne(0);
            Thread.Sleep(1000);
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - {i}");                 
            }
            mutex.ReleaseMutex();

            mutex.WaitOne(0);
            for (int i = 10; i > 0; i--)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} - {i}");
            }
            mutex.ReleaseMutex();
        }
    }
}
