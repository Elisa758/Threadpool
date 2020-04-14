using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Threadpool
{
    class Program
    {
        private static CountdownEvent _countdown;
        private Int32 _threadsCount;
        public static ConcurrentQueue<int> Scores { get; set; } = new ConcurrentQueue<int>();
        public Random Random { get; set; } = new Random();

        private static void Main()
        {
            var program = new Program(1000000);
            program.Run();
            _countdown.Wait();
            Console.WriteLine("Total = " + Scores.Count);
        }

        public Program(Int32 threadsCount)
        {
            _threadsCount = threadsCount;
            _countdown = new CountdownEvent(threadsCount); // Set the counter to the number of threads executing
        }

        public void Run()
        {
            for (int i = 0; i < _threadsCount; i++)
            {
                ThreadPool.QueueUserWorkItem(x => AddNumber());
            }

        }

        public void AddNumber()
        {
            int number = Random.Next(50);
            Scores.Enqueue(number);
            _countdown.Signal();
        }
    }
}
