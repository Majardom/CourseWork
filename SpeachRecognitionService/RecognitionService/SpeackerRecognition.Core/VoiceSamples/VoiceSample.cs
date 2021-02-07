using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Recognito.Utils;
using Recognito.Vad;

namespace SpeackerRecognition.Core.VoiceSamples
{
	public class VoiceSample
	{
		private int _windowsSize = 1024;
		private int _featuresSize = 20;

		private int _sampleRate;
		public double[] MelFrequency { get; private set; }

		public VoiceSample(byte[] sampleData, int sampleRate)
		{
			_sampleRate = sampleRate;

			var amplitudes = ProvideAmplitudeValues(sampleData);
			var normalizedAmplitudes = Normalize(amplitudes);
			MelFrequency = GetFeatures(normalizedAmplitudes);
		}

		private double[] ProvideAmplitudeValues(byte[] sample)
		{
			var voiceDetector = new AutocorrellatedVoiceActivityDetector();
			File.WriteAllBytes("tmp.wav", sample);
			double[] result = null;
			using (var stream = File.OpenRead("tmp.wav"))
			{
				result = AudioConverter.ConvertAudioToDoubleArray(stream, _sampleRate);
			}

			File.Delete("tmp.wav");

			return voiceDetector.RemoveSilence(result, _sampleRate);
		}

		private double[] Normalize(double[] sampleAmplitudes)
		{
			var maxAmplitude = sampleAmplitudes.Max();

			return sampleAmplitudes.Select(x => x / maxAmplitude)
			.ToArray();
		}

		private double[] GetFeatures(double[] sampleAmplitudes)
		{
			var windowsNumber = (int)Math.Ceiling((double)sampleAmplitudes.Length / _windowsSize);

			var features = new double[_featuresSize];

			for(int i = 0; i < windowsNumber; i++)
			{
				var window = new SampleWindow(_windowsSize, _featuresSize);

				var windowData = new List<double>();

				var windowStart = i * _windowsSize;
				var windowEnd = windowStart + _windowsSize;
				windowEnd = windowEnd > sampleAmplitudes.Length ? sampleAmplitudes.Length : windowEnd;

				for(int j = windowStart; j < windowEnd; j++)
				{
					var widowHemingCoef = 0.53836 - 0.46164 * Math.Cos(2 * Math.PI * (windowData.Count - 1) / _windowsSize);
					windowData.Add(sampleAmplitudes[j] * widowHemingCoef);
				}

				window.FillWithData(windowData.ToArray());

				for(int j = 0; j < features.Length; j++)
				{
					features[j] += window.MelFrequencyCoef[j];
				}
			}

			return features.Select(x => x / windowsNumber)
				.ToArray();
		}
	}
}
