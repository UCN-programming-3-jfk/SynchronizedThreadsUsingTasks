using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SynchronizedThreads
{
    class Program
    {
        private static object _lockObject = new object();
        private static List<int> _ints = new List<int>();
        static void Main(string[] args)
        {
            var task1 = new Task(() => Add10Ints());
            var task2 = new Task(() => Add10Ints());
            task1.Start();
            task2.Start();

            Task.WaitAll(task1, task2);

            for (int i = 0; i < _ints.Count; i++)
            {
                Console.WriteLine($"{i:00}:{_ints[i]}");
            }
        }

        public static void Add10Ints()
        {
            for (int number = 0; number < 10; number++)
            {
                AddInt(number);
                //AddIntLocked(number);
            }
        }


        private static void AddInt(int number)
        {
            _ints.Add(number);
        }

        private static void AddIntLocked(int number)
        {
            lock (_lockObject)
            {
                _ints.Add(number); 
            }
        }
    }
}
