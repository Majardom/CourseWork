using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeackerRecognition.Core
{
	public class SpeakerModel: BaseModel
	{
		public string Name { get; set; }

		public List<FeaturesModel> Features { get; set; }
	}
}
