using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Authetication.IntegrationTest.Authentication
{
	[TestClass]
	public class AuthenticationTest
	{
		[TestMethod]
		public async Task Test()
		{
			var process = AuthentificationClientFactory.ServeApplication();

			using (var client = new HttpClient())
			{
				var stringContent = new StringContent("grant_type=password&username=admin&password=admin");
				var response = await client.PostAsync("http://localhost:8080/token", stringContent);
				//Product product = await response.Content.ReadAsAsync<ResponseDto>();
			}

			process.Close();
		}
	}

	public class ResponseDto
	{
		public string AccessToken;
	}
}
