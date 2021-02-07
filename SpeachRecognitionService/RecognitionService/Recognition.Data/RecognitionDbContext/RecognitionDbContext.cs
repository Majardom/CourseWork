using System.Data.Entity;
using SpeackerRecognition.Core;

namespace Recognition.Data
{
	public class RecognitionDbContext : DbContext
	{
		public RecognitionDbContext()
				: base("RecognitionDb")
		{ }

		public DbSet<SpeakerModel> Speakers { get; set; }
	}
}
