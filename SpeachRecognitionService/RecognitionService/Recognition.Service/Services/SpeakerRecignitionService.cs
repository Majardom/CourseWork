using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recognition.Interfaces;
using Recognition.Services.Interfaces.Services;
using Recognito;
using SpeackerRecognition.Core;
using SpeackerRecognition.Core.ComparisonCore;

namespace Recognition.Service.Services
{
	public class SpeakerRecignitionService : IRecognitionService
	{
		private readonly IUnitOfWork _uow;

		public SpeakerRecignitionService(IUnitOfWork unitOfWork)
		{
			_uow = unitOfWork;
		}

		public void AddVoiceSamples(string samplesSpeaker, List<byte[]> samples)
		{
			foreach(var sample in samples)
			{
				ComparisonCore.Instance.LoadVoiceSample(samplesSpeaker, sample);
			}

			var dataSpeakers = ComparisonCore.Instance.Speakers.Select(x =>
			{
				var dataModel = new SpeakerModel()
				{
					Id = x.Id,
					Name = x.Name,
					Features = x.Samples.Select(y => new FeaturesModel() { Id = y.Id, Features = string.Join(";", y.MelFrequency) }).ToList()
				};

				return dataModel;
			});

			foreach (var dataSpeaker in dataSpeakers)
			{
				var speaker = dataSpeaker.Id != null ? _uow.Speakers.GetWithFeatures(dataSpeaker.Id) : null;
				if (speaker != null)
				{
					_uow.Speakers.Update(dataSpeaker);
				}
				else
				{
					_uow.Speakers.Create(dataSpeaker);	
				}
			}

			_uow.SaveChanges();
		}

		public MatchResult<string> RecognizeSpeaker(byte[] sample)
		{
			return new MatchResult<string>(ComparisonCore.Instance.IdentifySpeaker(sample), 100, 100);
		}
	}
}
