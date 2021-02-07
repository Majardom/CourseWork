using System.Collections.Generic;
using Recognito;

namespace Recognition.Services.Interfaces.Services
{
	public interface IRecognitionService
	{
		void AddVoiceSamples(string samplesSpeaker, List<byte[]> samples);

		MatchResult<string> RecognizeSpeaker(byte[] sample);
	}
}
