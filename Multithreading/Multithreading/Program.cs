using System;
using System.Threading;
using System.Diagnostics;
using Multithreading.Project_Euler;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            IExampleProcess multithreadProcess = new AutoResetEventProcess();

            //multithreadProcess.Run();

            var helperService = new HelperService();
            var eulerProblem = new Problem2(helperService);

            eulerProblem.Run();
        }
    }
}
