using System;
using System.Collections.Generic;

namespace Recognition.Core.Algorithms.Windowing
{
    public class HannWindowFunction : WindowFunction
    {
        private static readonly object _lock = new object();

        private static readonly Dictionary<int, double[]> factorsByWindowSize = new Dictionary<int, double[]>();


        /**
* Constructor imposed by WindowFunction
* @param windowSize the windowSize
* @see WindowFunction#WindowFunction(int)
*/
        HannWindowFunction(int windowSize) : base(windowSize) { }


        protected override double[] GetPrecomputedFactors(int windowSize)
        {
            // precompute factors for given window, avoid re-calculating for several instances
            lock (_lock)
            {
                if (factorsByWindowSize.ContainsKey(windowSize))
                    return factorsByWindowSize[windowSize];




                var factors = new double[windowSize];
                int sizeMinusOne = windowSize - 1;
                for (int i = 0; i < windowSize; i++)
                {
                    factors[i] = 0.5d * (1 - Math.Cos((TWO_PI * i) / sizeMinusOne));
                }

                factorsByWindowSize.Add(windowSize, factors);
                return factors;
            }
        }
    }
}
