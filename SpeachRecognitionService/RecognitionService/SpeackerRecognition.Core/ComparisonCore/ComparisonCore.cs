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
				for (int i = 0; i < speakerToUpdate.Samples[0].MelFrequency.Length; i++)
				{
					speakerToUpdate.Samples[0].MelFrequency[i] = (speakerToUpdate.Samples[0].MelFrequency[i] + voiceSample.MelFrequency[i]) / 2;
				}
			}
			else
			{
				var speaker = new Speaker(speakerName);
				speaker.Id = Guid.NewGuid().ToString();
				speaker.Samples.Add(voiceSample);
				Speakers.Add(speaker);
			}
		}

		public string IdentifySpeaker(byte[] sampleToIdentify)
		{
			var sample = new VoiceSample(sampleToIdentify, _sampleRate);

			var speakersDistances = Speakers.Select(x =>
			{
				double distance = 0;
				for (int i = 0; i < x.Samples[0].MelFrequency.Length; i++)
				{
					distance += Math.Abs(x.Samples[0].MelFrequency[i] - sample.MelFrequency[i]);
				}

				return new { Speaker = x.Name, Distance = distance };
			});

			return speakersDistances.OrderBy(x => x.Distance).First().Speaker;
		}

		public void InitializeCore(List<SpeakerModel> speakersData)
		{
			foreach (var speakerData in speakersData)
			{
				var speaker = new Speaker(speakerData.Name) 
				{
					Id = speakerData.Id
				};
				foreach(var dataSample in speakerData.Features)
				{
					var sample = new VoiceSample(dataSample.Features.Split(';').Select(x => double.Parse(x)).ToArray());
					sample.Id = dataSample.Id;
					speaker.Samples.Add(sample);
				}

				Speakers.Add(speaker);
			}
		}
	}
}
