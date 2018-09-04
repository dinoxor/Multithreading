using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Multithreading
{
    public class BadProcess:IExampleProcess
    {
        private string _threadOutput = string.Empty;

        public void Run()
        {
            Thread thread1 = new Thread(new ThreadStart(DisplayThread1));
            Thread thread2 = new Thread(new ThreadStart(DisplayThread2));

            Console.WriteLine("Nothing \n");

            thread1.Start();
            thread2.Start();
        }

        private void DisplayThread1()
        {
            while (true)
            {
                Console.WriteLine($"Thread 1 is starting");

                _threadOutput = "1";                
                Thread.Sleep(1000);

                Console.WriteLine($"Thread 1 finishes with output of: {_threadOutput}\n");
            }
        }
        private void DisplayThread2()
        {
            while (true)
            {
                Console.WriteLine($"Thread 2 is starting");

                _threadOutput = "2";
                Thread.Sleep(1000);

                Console.WriteLine($"Thread 2 finishes with output of: {_threadOutput}\n");
            }
        }

    }

}

    
