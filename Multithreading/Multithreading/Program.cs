using System;
using System.Threading;
using System.Diagnostics;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            var badProcess = new BadProcess();
            badProcess.Run();
        }
    }
}
