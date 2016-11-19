﻿using System;
using System.Threading;

namespace IV.Race.Condition
{
    public class Startup
    {
        private static volatile int counter = 0;
        private static object counterLock = new object();

        public static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(new string('*',60));
                StartCounterConcurrently();
                counter = 0;
            }
        }

        public static void StartCounterConcurrently()
        {
            var t1 = new Thread(()=> Run(ConsoleColor.Green));
            var t2 = new Thread(()=> Run(ConsoleColor.Yellow));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.ResetColor();
        }

        public static void Run(ConsoleColor foregroundColor)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            while (counter < 10)
            {
                Thread.Sleep(100);
                counter++;
                Console.ForegroundColor = foregroundColor;
                Console.WriteLine($"[Thread:{threadId}] - {counter}");
            }
        }
    }
}
