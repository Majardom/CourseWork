using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recognition.Services.Interfaces.Services;
using Recognito;
using SpeackerRecognition.Core.ComparisonCore;

namespace Recognition.Service.Services
{
	public class SpeakerRecignitionService : IRecognitionService
	{
		public void AddVoiceSamples(string samplesSpeaker, List<byte[]> samples)
		{
			foreach(var sample in samples)
			{
				ComparisonCore.Instance.LoadVoiceSample(samplesSpeaker, sample);
			}
		}

		public MatchResult<string> RecognizeSpeaker(byte[] sample)
		{
			return new MatchResult<string>(ComparisonCore.Instance.IdentifySpeaker(sample), 100, 100);
		}
	}
}
