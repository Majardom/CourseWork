using System.Collections.Generic;
using System.Linq;
using Recognition.Interfaces;
using SpeackerRecognition.Core;

namespace Recognition.Data
{
	public class SpeakersRepository : GenericRepository<SpeakerModel>, ISpeakerRepository
	{
		public SpeakersRepository(RecognitionDbContext context)
			: base(context)
		{ }

		public IEnumerable<SpeakerModel> GetAllWithFeatures()
		{
			return DbContext.Speakers.Include("Features");
		}

		public SpeakerModel GetWithFeatures(string id)
		{
			return DbContext.Speakers.Include("Features").Where(x => x.Id == id).FirstOrDefault();
		}
	}
}
