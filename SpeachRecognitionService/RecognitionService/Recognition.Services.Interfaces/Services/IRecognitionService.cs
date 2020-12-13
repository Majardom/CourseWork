using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recognition.Core;

namespace Recognition.Services.Interfaces.Services
{
	public interface IRecognitionService
	{
		void AddVoiceSamples(string samplesSpeaker, List<byte[]> samples);

		MatchResult<string> RecognizeSpeaker(byte[] sample);
	}
}
