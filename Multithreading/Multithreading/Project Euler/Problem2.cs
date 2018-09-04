using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Multithreading.Project_Euler
{
    public class Problem2
    {
        private HelperService _helperService;

        public Problem2 (HelperService helperService)
        {
            _helperService = helperService;
        }
        public void Run()
        {
            var fibSequences = _helperService.GetFibonacciSequenceByTerm(10);

            foreach (var fibValue in fibSequences)
            {
                Debug.WriteLine(fibValue);
            }

        }
    }
}
