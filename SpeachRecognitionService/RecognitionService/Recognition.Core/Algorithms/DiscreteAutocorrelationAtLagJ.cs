using System;

namespace Recognito.Algorithms
{
    public class DiscreteAutocorrelationAtLagJ
    {
        public double Autocorrelate(double[] buffer, int lag)
        {
            if (lag > -1 && lag < buffer.Length)
            {
                double result = 0.0;
                for (int i = lag; i < buffer.Length; i++)
                {
                    result += buffer[i] * buffer[i - lag];
                }
                return result;
            }
            else
            {
                throw new IndexOutOfRangeException($"Lag parameter range is : -1 < lag < buffer size. Received [{lag}] for buffer size of [{buffer.Length}]");
            }
        }

    }
}
