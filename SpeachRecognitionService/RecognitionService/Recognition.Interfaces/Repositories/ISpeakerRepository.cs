using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeackerRecognition.Core;

namespace Recognition.Interfaces
{
	public interface ISpeakerRepository : IGenericRepository<SpeakerModel>
	{
		IEnumerable<SpeakerModel> GetAllWithFeatures();

		SpeakerModel GetWithFeatures(string id);
	}
}
