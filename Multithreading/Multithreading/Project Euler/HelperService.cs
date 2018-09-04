using System;
using System.Collections.Generic;
using System.Numerics;

namespace Multithreading.Project_Euler
{
    public class HelperService
    {
        public List<BigInteger> GetFibonacciSequenceByTerm(int targetTerm)
        {
            var fibSequences = new List<BigInteger> { 1, 2 };

            if (targetTerm == 1)
            {
                return new List<BigInteger> { 1 };
            }
            else if (targetTerm == 2)
            {
                return new List<BigInteger> { 1, 2 };
            }
            else
            {
                BigInteger fibValue1 = 1;
                BigInteger fibValue2 = 2;

                for (int term = 3; term <= targetTerm; term++)
                {
                    BigInteger newFibValue = fibValue1 + fibValue2;
                    fibValue1 = fibValue2;
                    fibValue2 = newFibValue;

                    fibSequences.Add(newFibValue);
                }
            }

            return fibSequences;
        }

        public List<BigInteger> GetFibonacciSequenceByLessThanOrEqualToValue(int value)
        {
            var fibSequences = new List<BigInteger> { 1, 2 };

            BigInteger fibValue1 = 1;
            BigInteger fibValue2 = 2;
            BigInteger newFibValue = 0;

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
