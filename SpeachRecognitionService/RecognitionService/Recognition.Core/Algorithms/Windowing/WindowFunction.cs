using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognition.Core.Algorithms.Windowing
{
    public abstract class WindowFunction
    {
        protected static readonly double TWO_PI = 2 * Math.PI;

        readonly int windowSize;
        readonly double[] factors;


        /**
      * Constructor of a WindowFunction
      * <p>
      * Please note this constructor precomputes all coefficiencies for the given window size
      * </p>
      * @param windowSize the window size
      */
        public WindowFunction(int windowSize)
        {
            this.windowSize = windowSize;
            factors = GetPrecomputedFactors(windowSize);
        }


        /**
         * Applies window function to an array of doubles
         * @param window array of doubles to apply windowing to
         */
        public void ApplyFunction(double[] window)
        {
            if (window.Length == windowSize)
            {
                for (int i = 0; i < window.Length; i++)
                {
                    window[i] *= factors[i];
                }
            }
            else
            {
                throw new ArgumentException($"Incompatible window size for this WindowFunction instance : expected {windowSize}, received {window.Length}");
            }
        }

        /**
         * Precomputes factors to be applied for this function, called from constructor<br/>
         * Implementing classes are strongly advised to cache the results for subsequent instances
         * @param windowSize the window size
         * @return the precomputed factors
         */
        protected abstract double[] GetPrecomputedFactors(int windowSize);
    }
}
