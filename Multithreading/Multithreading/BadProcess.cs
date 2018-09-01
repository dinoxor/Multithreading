using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Multithreading
{
    public class BadProcess
    {
        private string _threadOutput = "";

        public void Run()
        {
            Thread thread1 = new Thread(new ThreadStart(DisplayThread1));
            Thread thread2 = new Thread(new ThreadStart(DisplayThread2));

            thread1.Start();
            thread2.Start();
        }

        private void DisplayThread1()
        {
            while (true)
            {
                //Console.WriteLine("Inside thread 1");
                _threadOutput = "thread 1";

                Thread.Sleep(1000);
                Console.WriteLine(_threadOutput);
            }
        }
        private void DisplayThread2()
        {
            while (true)
            {
                //Console.WriteLine("Inside thread 2");
                _threadOutput = "thread 2";

                Thread.Sleep(1000);
                Console.WriteLine(_threadOutput);
            }
        }

    }

}

    
