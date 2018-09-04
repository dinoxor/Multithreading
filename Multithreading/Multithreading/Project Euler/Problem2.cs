using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;

namespace Multithreading.Project_Euler
{
    public class Problem2
    {
        //By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.

        private HelperService _helperService;

        public Problem2 (HelperService helperService)
        {
            _helperService = helperService;
        }

        public void Run()
        {
            var fibSequences = _helperService.GetFibonacciSequenceByLessThanOrEqualToValue(4000001);

            foreach (var fibValue in fibSequences)
            {
                Debug.WriteLine(fibValue);
            }

            DisplayEvenSumFibonacci(fibSequences);
        }

        private void DisplayEvenSumFibonacci(List<int> fibSequences)
        {
            var evenFibSequences = fibSequences.Where(x => x % 2 == 0);

            Debug.WriteLine("\nEven");
            var evenSum = 0;

            foreach (var fibValue in evenFibSequences)
            {
                evenSum += fibValue;
                Debug.WriteLine(fibValue);
            }

            Debug.WriteLine($"\nSum of even Fibonacci Sequence less than four million: {evenSum}");
        }

    }
}
