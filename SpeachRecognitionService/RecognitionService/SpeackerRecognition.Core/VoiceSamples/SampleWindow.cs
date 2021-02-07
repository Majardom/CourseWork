using System;
using System.Linq;
using System.Numerics;

namespace SpeackerRecognition.Core.VoiceSamples
{
	public class SampleWindow
	{
		private Complex[] _complexData;

		private double[] _frequency;

		public double[] MelFrequencyCoef { get; private set; }

		public SampleWindow(int size, int featuresSize)
		{
			_complexData = new Complex[size];
			MelFrequencyCoef = new double[featuresSize];
		}

		public void FillWithData(double[] data)
		{
			if (data.Length > _complexData.Length)
				throw new ArgumentException($"Window data should be less or equal to window size; Expected:{_complexData.Length} Actual:{data.Length}");

			for (int i = 0; i < data.Length; i++)
			{
				_complexData[i] = data[i];
			}

			var fft = FFT.fft(_complexData);

			_frequency = fft.Select(x => ConvertToMelFrequency(x.Magnitude)).ToArray();

			for(int n = 0; n < MelFrequencyCoef.Length; n++)
			{
				double coef = 0;

				for(int k = 1; k  < MelFrequencyCoef.Length; k++)
				{
					coef += Math.Log10(_frequency[k])*(n*(k - 0.5) * Math.PI / MelFrequencyCoef.Length);
				}

				MelFrequencyCoef[n] = coef;
			}
		}

		private double ConvertToMelFrequency(double hzFrequency)
		{
			return 2595 * Math.Log10(1 + hzFrequency / 700);
		}
	}
}
