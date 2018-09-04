using System;
using System.Threading;
using System.Diagnostics;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            IExampleProcess multithreadProcess = new AutoResetEventProcess();
                       
            multithreadProcess.Run();
        }
    }
}
