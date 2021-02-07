using System;
using System.Collections.Generic;

namespace Recognito.Algorithms.Windowing
{
    public class HammingWindowFunction : WindowFunction
    {
        private static readonly object _lock = new object();

        private static readonly Dictionary<int, double[]> factorsByWindowSize = new Dictionary<int, double[]>();


        public HammingWindowFunction(int windowSize) : base(windowSize) { }

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
                    factors[i] = 0.54d - (0.46d * Math.Cos((TWO_PI * i) / sizeMinusOne));
                }
                factorsByWindowSize.Add(windowSize, factors);
                return factors;
            }
        }
    }
}
