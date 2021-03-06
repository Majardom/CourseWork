﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Recognition.Services.Interfaces.Services;
using Recognito;
using Recognito.Vad;

namespace Recognition.Service.Services
{
	public class RecognitionService : IRecognitionService
	{
		private readonly string _baseDirectory;

		private const int sampleRate = 44100;
		private readonly Recognito<string> recognition = new Recognito<string>(sampleRate);

		public RecognitionService(string baseDirectory)
		{
			if (baseDirectory == null)
				throw new ArgumentNullException(nameof(baseDirectory));

			_baseDirectory = baseDirectory;
		}

		public void AddVoiceSamples(string samplesSpeaker, List<byte[]> samples)
		{
			var sampleDirectory = Path.Combine(_baseDirectory, samplesSpeaker);
			Directory.CreateDirectory(sampleDirectory);

			for(int i = 0; i < samples.Count; i++)
			{
				var fileName = i.ToString() + ".wav";
				var fullFileName = Path.Combine(sampleDirectory, fileName);
				File.WriteAllBytes(fullFileName, samples[i]);
			}
		}

		public MatchResult<string> RecognizeSpeaker(byte[] sample)
		{
			var voiceDetector = new AutocorrellatedVoiceActivityDetector();

			var fullFileName = Path.Combine(_baseDirectory, "identify.wav");
			File.WriteAllBytes(fullFileName, sample);

			foreach (var persons in Directory.GetDirectories(_baseDirectory).OrderBy(f => f))
			{
				var info = new DirectoryInfo(persons);
				var name = info.Name;

				VoicePrint voice = null;

				foreach (var audio in Directory.GetFiles(persons, "*.wav", SearchOption.TopDirectoryOnly))
				{
					using (var fs = File.OpenRead(audio))
					{
						if (voice == null)
							voice = recognition.CreateVoicePrint(name, fs);
						else
							voice = recognition.MergeVoiceSample(name, fs);
					}
				}
			}

			MatchResult<string> identify;


			using (var stream = new FileStream(fullFileName, FileMode.Open))
			{
				identify = recognition.Identify(stream).FirstOrDefault();
			}

			File.Delete(fullFileName);

			return identify;
		}
	}
}
