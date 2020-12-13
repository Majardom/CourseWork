using System;

namespace Recognition.Core.Enchancements
{
    public class Normalizer
    {
        public double Normalize(double[] audioSample, float sampleRate)
        {

            double max = Double.MinValue;

            for (int i = 0; i < audioSample.Length; i++)
            {
                double abs = Math.Abs(audioSample[i]);
                if (abs > max)
                {
                    max = abs;
                }
            }

            if (max > 1.0d)
            {
                throw new ArgumentException("Expected value for audio are in the range -1.0 <= v <= 1.0 ");
            }

            if (max < 5 * MathHelper.Ulp(0.0d))
            { // ulp of 0.0 is extremely small ! i.e. as small as it can get
                return 1.0d;
            }


            for (int i = 0; i < audioSample.Length; i++)
            {
                audioSample[i] /= max;
            }
            return 1.0d / max;
        }
    }
}
