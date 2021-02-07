using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeackerRecognition.Core.VoiceSamples;

namespace SpeackerRecognition.Core.ComparisonCore
{
	public class ComparisonCore
	{
		public List<Speaker> Speakers { get; private set; }

		private int _sampleRate = 44100;

		private static ComparisonCore _instace;
		public static ComparisonCore Instance
		{
			get
			{
				if (_instace == null)
					_instace = new ComparisonCore();
				return _instace;
			}
		}


		private ComparisonCore()
		{
			Speakers = new List<Speaker>();
		}

		public void LoadVoiceSample(string speakerName, byte[] sample)
		{
			var speakerToUpdate = Speakers.FirstOrDefault(x => x.Name == speakerName);
			var voiceSample = new VoiceSample(sample, _sampleRate);

			if (speakerToUpdate != null)
			{
				for (int i = 0; i < speakerToUpdate.Samples.MelFrequency.Length; i++)
				{
					speakerToUpdate.Samples.MelFrequency[i] = (speakerToUpdate.Samples.MelFrequency[i] + voiceSample.MelFrequency[i]) / 2;
				}
			}
			else
			{
				var speaker = new Speaker(speakerName);
				speaker.Samples = voiceSample;
				Speakers.Add(speaker);
			}
		}

		public string IdentifySpeaker(byte[] sampleToIdentify)
		{
			var sample = new VoiceSample(sampleToIdentify, _sampleRate);

			var speakersDistances = Speakers.Select(x =>
			{
				double distance = 0;
				for (int i = 0; i < x.Samples.MelFrequency.Length; i++)
				{
					distance += Math.Abs(x.Samples.MelFrequency[i] - sample.MelFrequency[i]);
				}

				return new { Speaker = x.Name, Distance = distance };
			});

			return speakersDistances.OrderBy(x => x.Distance).First().Speaker;
		}
	}
}
