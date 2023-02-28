using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    internal class Threads_14_02_2023
    {
        public static void Task()
        {
            AutoResetEvent mre = new(true);
            var events = new AutoResetEvent[10];
            for (int i = 0; i < 10; i++)
            {
                events[i] = new AutoResetEvent(true);
                ThreadPool.QueueUserWorkItem(Do1, events[i]);
            }

            Thread.Sleep(800);
            var num = AutoResetEvent.WaitAny(events);
            Console.WriteLine($"{num} is done");
        }

        static void Do1(object obj)
        {
            EventWaitHandle ev = obj as EventWaitHandle;

            ev.WaitOne();
            {
                Thread.Sleep(new Random().Next(1000));
                Console.WriteLine("Поток {0}", Thread.CurrentThread.ManagedThreadId);
                ev.Set();
            }
        }

        static void Do2(object obj)
        {
            EventWaitHandle ev = obj as EventWaitHandle;

            if (ev.WaitOne(1000))
            {
                Console.WriteLine("Поток {0} успел", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(700);
                ev.Set();
            }
            else
            {
                Console.WriteLine("Поток {0} опоздал", Thread.CurrentThread.ManagedThreadId);
            }
        }
    }
}
