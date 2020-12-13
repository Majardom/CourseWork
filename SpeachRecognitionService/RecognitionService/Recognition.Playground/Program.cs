using Recognition.Core.Utils;
using Recognition.Core.Vad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using Recognition.Core;

namespace Recognito.Playground
{
	class Program
	{
		const int sampleRate = 44100;
		static Recognition<string> recognito = new Recognition<string>(sampleRate);

		static void Main(string[] args)
		{
			var start = DateTime.Now;

			string base_dir = "D:\\samples";

			var tests = new List<string>();

			using (var waveOut = new WaveOutEvent())
			{

				var voiceDetector = new AutocorrellatedVoiceActivityDetector();


				foreach (var pessoas in Directory.GetDirectories(base_dir).OrderBy(f => f))
				{
					var info = new DirectoryInfo(pessoas);
					var nome = info.Name;

					Console.WriteLine($"nome:{nome}");

					VoicePrint voice = null;

					foreach (var audio in Directory.GetFiles(pessoas, "audio_*.wav", SearchOption.TopDirectoryOnly))
					{
						Console.WriteLine($"nome:{audio}");




						using (var fs = File.OpenRead(audio))
						{
							if (voice == null)
								voice = recognito.CreateVoicePrint(nome, fs);
							else
								voice = recognito.MergeVoiceSample(nome, fs);
						}

						//using (var wr = new WaveFileReader(audio))
						//{
						//	Console.WriteLine("Play Original");
						//	waveOut.Init(wr);
						//	waveOut.PlayAndWait();
						//}
					}
				}
			}

			Console.WriteLine("\n\nTestes");
			tests = Directory.GetFiles(base_dir, "teste_*.wav", SearchOption.AllDirectories).ToList();

			foreach (var test in tests)
			{
				Console.WriteLine($"Testando: {test}");

				using (var fs = new FileStream(test, FileMode.Open))
				{
					var identify = recognito.Identify(fs).FirstOrDefault();

					Console.WriteLine($"identify.Key:{identify.Key},identify.Distance: {identify.Distance}, identify.LikelihoodRatio:{identify.LikelihoodRatio}");
				}

			}


			Console.WriteLine($"Time:{(DateTime.Now - start)}");

			Console.WriteLine("Press Any Key do Close");
			Console.ReadKey();

		}
	}
}
