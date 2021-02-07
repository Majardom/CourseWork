using System;
using System.Collections.Generic;
using SpeackerRecognition.Core.VoiceSamples;

namespace SpeackerRecognition.Core.ComparisonCore
{
	public class Speaker
	{
		public string Name { get; private set; }

		public VoiceSample Samples { get; set; }

		public Speaker(string name)
		{
			Name = name 
				?? throw new ArgumentNullException(nameof(name));
		}
	}
}
