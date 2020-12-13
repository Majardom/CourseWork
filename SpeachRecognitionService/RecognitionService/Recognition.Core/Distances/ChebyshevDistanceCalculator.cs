using System;

namespace Recognition.Core.Distances
{
    class ChebyshevDistanceCalculator : DistanceCalculator
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
                    var currentDistance = Math.Abs(features1[i] - features2[i]);

                    distance = (currentDistance > distance) ? currentDistance : distance;
                }
            }

            return distance;
        }
    }
}
