using System;

namespace Recognito.Distances
{
    public abstract class DistanceCalculator
    {
        public abstract double GetDistance(double[] features1, double[] features2);

        protected double PositiveInfinityIfEitherOrBothAreNull(double[] features1, double[] features2)
        {
            if (features1 == null || features2 == null)
                return Double.PositiveInfinity;


            return -1.0d;
        }
    }
}
