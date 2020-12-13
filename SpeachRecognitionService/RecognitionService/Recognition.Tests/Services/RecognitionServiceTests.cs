using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recognition.Service.Services;

namespace Recognition.Tests.Services
{
	[TestClass]
	public class RecognitionServiceTests
	{
		private const string BASE_DIRECTORY_PATH = "test";	

		[TestCleanup]
		public void CleanUp()
		{
			var directory = new DirectoryInfo(BASE_DIRECTORY_PATH);
			if(directory.Exists)
				directory.Delete(true);
		}

		[TestMethod]
		public  void Construct_With_Null_BaseDirectory_Throws_Exception()
		{
			//arrange
			string baseDirectoryPath = null;

			//act
			Action act = () => { var sut = new RecognitionService(baseDirectoryPath); };

			//assert
			act.Should()
				.Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void Construct_With_Base_Directory_Succesfully()
		{
			//arrange
			

			//act
			Action act = () => { var sut = new RecognitionService(BASE_DIRECTORY_PATH); };

			//assert
			act.Should()
				.NotThrow<ArgumentNullException>();
		}

		[TestMethod]
		public void AddVoiceSamples_New_Sample_File_Is_Added_To_Coresponding_Directory_Succesfully()
		{
			//arange
			string sampleAuthor = "a1";

			string sample = "sample";
			byte[] sampleBytes = Encoding.ASCII.GetBytes(sample);

			var sut = new RecognitionService(BASE_DIRECTORY_PATH);


			//act
			sut.AddVoiceSamples(sampleAuthor, new List<byte[]> { sampleBytes });
			var directory = new DirectoryInfo(Path.Combine(BASE_DIRECTORY_PATH, sampleAuthor));

			//assert
			directory
				.Exists
				.Should()
				.BeTrue();

			directory.GetFiles("*.wav")
				.Should()
				.HaveCount(1, "New samle should be added");
		}
	}
}
