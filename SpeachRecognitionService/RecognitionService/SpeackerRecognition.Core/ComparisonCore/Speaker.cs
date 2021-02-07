using System;
using System.Collections.Generic;
using SpeackerRecognition.Core.VoiceSamples;

namespace SpeackerRecognition.Core.ComparisonCore
{
	public class Speaker
	{
		public string Id { get; set; }

		public string Name { get; private set; }

		public List<VoiceSample> Samples { get; set; }

		public Speaker(string name)
		{
			Name = name 
				?? throw new ArgumentNullException(nameof(name));

			Samples = new List<VoiceSample>();
		}
	}
}
