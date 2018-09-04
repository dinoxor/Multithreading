using System;
using System.Collections.Generic;
using System.Text;

namespace Multithreading.Project_Euler
{
    public class HelperService
    {
        public List<int> GetFibonacciSequenceByTerm(int targetTerm)
        {
            var fibSequences = new List<int> { 1, 2 };

            if (targetTerm == 1)
            {
                return new List<int> { 1 };
            }
            else if (targetTerm == 2)
            {
                return new List<int> { 1, 2 };
            }
            else
            {
                var fibValue1 = 1;
                var fibValue2 = 2;

                for (int term = 3; term <= targetTerm; term++)
                {
                    var newFibValue = fibValue1 + fibValue2;
                    fibValue1 = fibValue2;
                    fibValue2 = newFibValue;

                    fibSequences.Add(newFibValue);
                }
            }

            return fibSequences;
        }

        public List<int> GetFibonacciSequenceByLessThanOrEqualToValue(int value)
        {
            var fibSequences = new List<int> { 1, 2 };

            var fibValue1 = 1;
            var fibValue2 = 2;
            var newFibValue = 0;

            var continueFib = true;

            do
            {
                newFibValue = fibValue1 + fibValue2;

                if (newFibValue <= value)
                {
                    fibValue1 = fibValue2;
                    fibValue2 = newFibValue;

                    fibSequences.Add(newFibValue);
                }
                else
                {
                    continueFib = false;
                }

            } while (continueFib);

            return fibSequences;
        }
    }
}
