using Recognition.Core.Algorithms;
using Recognition.Core.Algorithms.Windowing;
using System;

namespace Recognition.Core.Features
{
    public class LpcFeaturesExtractor : WindowedFeaturesExtractor<double[]>
    {
        private readonly int poles;
        private readonly WindowFunction windowFunction;
        private readonly LinearPredictiveCoding lpc;



        public LpcFeaturesExtractor(float sampleRate, int poles)
            : base(sampleRate)
        {
            this.poles = poles;
            windowFunction = new HammingWindowFunction(windowSize);
            lpc = new LinearPredictiveCoding(windowSize, poles);
        }

        public override double[] ExtractFeatures(double[] voiceSample)
        {
            double[] voiceFeatures = new double[poles];
            double[] audioWindow = new double[windowSize];

            int counter = 0;
            int halfWindowLength = windowSize / 2;

            for (int i = 0; (i + windowSize) <= voiceSample.Length; i += halfWindowLength)
            {
                Array.Copy(voiceSample, i, audioWindow, 0, windowSize);

                windowFunction.ApplyFunction(audioWindow);
                double[] lpcCoeffs = lpc.ApplyLinearPredictiveCoding(audioWindow)[0];

                for (int j = 0; j < poles; j++)
                {
                    voiceFeatures[j] += lpcCoeffs[j];
                }
                counter++;
            }

            if (counter > 1)
            {
                for (int i = 0; i < poles; i++)
                {
                    voiceFeatures[i] /= counter;
                }
            }

            return voiceFeatures;
        }
    }
}
