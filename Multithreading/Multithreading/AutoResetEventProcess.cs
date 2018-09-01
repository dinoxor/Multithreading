using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Multithreading
{
    public class AutoResetEventProcess
    {
        private string _threadOutput = string.Empty;
        AutoResetEvent _blockThread1 = new AutoResetEvent(false); //blocks thread aka "Am I able to automatically go now?"
        AutoResetEvent _blockThread2 = new AutoResetEvent(true);

        public void Run()
        {
            Thread thread1 = new Thread(new ThreadStart(DisplayThread1));
            Thread thread2 = new Thread(new ThreadStart(DisplayThread2));

            Console.WriteLine("Auto Reset Event \n");

            thread1.Start();
            thread2.Start();
        }

        private void DisplayThread1()
        {
            while (true)
            {
                //1. Sees that AutoResetEvent is false, so it waits for Set()
                //4. Receives a signal so it will proceed
                _blockThread1.WaitOne();

                Console.WriteLine($"Thread 1 is starting");

                _threadOutput = "1";
                Thread.Sleep(500);

                Console.WriteLine($"Thread 1 finishes with output of: {_threadOutput}\n");

                //6. Signals that thread 2 can continue
                _blockThread2.Set();
            }
        }
        private void DisplayThread2()
        {
            while (true)
            {
                //2. sees that AutoResetEvent is true, so it continues 
                //5. waits until it receives a signal from SET
                _blockThread2.WaitOne();

                Console.WriteLine($"Thread 2 is starting");

                _threadOutput = "2";
                Thread.Sleep(1000);

                Console.WriteLine($"Thread 2 finishes with output of: {_threadOutput}\n");

                //3. Signals that thread1 can continue
                _blockThread1.Set();
            }
        }
    }
}
