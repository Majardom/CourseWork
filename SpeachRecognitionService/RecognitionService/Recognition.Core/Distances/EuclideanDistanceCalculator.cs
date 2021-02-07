using System;

namespace Recognito.Distances
{
    class EuclideanDistanceCalculator : DistanceCalculator
    {
        public override double GetDistance(double[] features1, double[] features2)
        {
            double distance = PositiveInfinityIfEitherOrBothAreNull(features1, features2);

            if (distance < 0)
            {
                if (features1.Length != features2.Length)
                    throw new ArgumentException($"Both features should have the same length. Received lengths of [{ features1.Length }] and [{features2.Length}]");

                distance = 0.0;
                for (int i = 0; i < features1.Length; i++)
                {
                    double diff = features1[i] - features2[i];
                    distance += diff * diff;
                }
            }

            return distance;
        }
    }
}
