using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Numerics;

namespace Multithreading.Project_Euler
{
    public class Problem2
    {
        //By considering the terms in the Fibonacci sequence whose values do not exceed four million, find the sum of the even-valued terms.

        private HelperService _helperService;
        BigInteger result1;
        BigInteger result2;

        public Problem2(HelperService helperService)
        {
            _helperService = helperService;
        }

        public void Run()
        {
            var fibSequences = _helperService.GetFibonacciSequenceByLessThanOrEqualToValue(4000001);
            var evenFibSequences = fibSequences.Where(x => x % 2 == 0);

            foreach (var fibValue in fibSequences)
            {
                Debug.WriteLine(fibValue);
            }

            DisplaySumFibonacci(evenFibSequences);
            DisplaySumFibonacciWithTwoThreads(evenFibSequences);

            ////////////////////
            var fibSequences2 = _helperService.GetFibonacciSequenceByTerm(50);

            DisplaySumFibonacci(fibSequences2);
            DisplaySumFibonacciWithTwoThreads(fibSequences2);

            /////////////////////

        }

        private void DisplaySumFibonacci(IEnumerable<BigInteger> fibSequences)
        {
            var watch = Stopwatch.StartNew();            

            BigInteger evenSum = 0;

            foreach (var fibValue in fibSequences)
            {
                evenSum += fibValue;
                Debug.WriteLine(fibValue);
            }

            watch.Stop();

            Debug.WriteLine($"\nSum without threading: {evenSum} with elapsed time of {watch.ElapsedMilliseconds}");
        }

        //////////////////////////////////////////////////////////////////////////////

        private void DisplaySumFibonacciWithTwoThreads(IEnumerable<BigInteger> fibSequences)
        {
            var watch = Stopwatch.StartNew();
            
            var totalInList = fibSequences.Count();
            var firstHalfValue = totalInList / 2;
            int secondHalfValue;

            if (totalInList % 2 != 0)
            {
                secondHalfValue = (totalInList / 2) + 1;
            }
            else
            {
                secondHalfValue = firstHalfValue;
            }

            var firstHalf = fibSequences.Take(firstHalfValue);
            var secondHalf = fibSequences.TakeLast(secondHalfValue);

            //int result1;
            //int result2;

            Thread thread1 = new Thread(delegate() { result1 = calculateSum(firstHalf); });
            Thread thread2 = new Thread(() => { result2 = calculateSum(secondHalf); });

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            watch.Stop();

            Debug.WriteLine($"\nSum with threading: {result1 + result2} with elapse time of {watch.ElapsedMilliseconds}");
        }

        private BigInteger calculateSum(IEnumerable<BigInteger> listOfNumbers)
        {
            BigInteger sum = 0;
            foreach (var number in listOfNumbers)
            {
                sum += number;
            }

            return sum;
        }

    }
}
