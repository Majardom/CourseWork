using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Recognition.Web.Controllers;
using Recognitoin.IntegrationTests.Factories;

namespace Recognitoin.IntegrationTests.Recognition
{
	[TestClass]
	public class RecognitionTest
	{
		private Process _iisProces;

		[TestInitialize]
		public void Initialize()
		{
			_iisProces = RecognitionClientFactory.ServeApplication();
		}

		[TestCleanup]
		public void CleanUp()
		{
			_iisProces.Kill();
		}

		[TestMethod]
		public async Task AddSamples_Sucessfully()
		{
			//arrange
			var author = "a1";
			var url = $"http://localhost:8080/api/recognition?sampleAuthorName={author}";

			string sample = "sample";
			byte[] sampleBytes = Encoding.ASCII.GetBytes(sample);

			HttpResponseMessage httpResponse = null;

			//act
			using (var client = new HttpClient())
			{
				var sampleDto = new SampleDto() { SamplesBase64 = new List<string> { Convert.ToBase64String(sampleBytes) } };
				var stringContent = new StringContent(JsonConvert.SerializeObject(sampleDto), Encoding.UTF8, "application/json");
				httpResponse = await client.PostAsync(url, stringContent);
			}

			//assert
			httpResponse
				.Should()
				.NotBeNull();

			httpResponse.StatusCode
				.Should()
				.Be(HttpStatusCode.OK);
		}
	}
}
