using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MicroserviceAuthentication.Autenticate.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Authetication.IntegrationTest.Authentication
{
	[TestClass]
	public class AuthenticationTest
	{
		private Process _iisProces;
		
		[TestInitialize]
		public void Initialize()
		{
			_iisProces = AuthentificationClientFactory.ServeApplication();
		}

		[TestCleanup]
		public void CleanUp()
		{
			_iisProces.Kill();
		}

		[TestMethod]
		public async Task LogIn_When_Credentials_Are_Correct_Issues_Valid_Token()
		{
			//arange
			var stringContent = new StringContent("grant_type=password&username=admin&password=admin");
			var tokenURL = "http://localhost:8080/token";
			TokenResponseDto tokenResponse = null;
			HttpResponseMessage httpTokenResponse = null;

			var validationURL = "http://localhost:8080/api/validation";
			bool validationResponce = false;
			HttpResponseMessage httpValidationResponse = null;

			//act
			using (var client = new HttpClient())
			{
				httpTokenResponse = await client.PostAsync(tokenURL, stringContent);
				var jsonResonce = await httpTokenResponse.Content.ReadAsStringAsync();
				tokenResponse = JsonConvert.DeserializeObject<TokenResponseDto>(jsonResonce);

				var validationDto = new TokenValidationDto { Token = tokenResponse.access_token };

				stringContent = new StringContent(JsonConvert.SerializeObject(validationDto), Encoding.UTF8, "application/json");
				httpValidationResponse = await client.PostAsync(validationURL, stringContent);
				var validationStringResponce = await httpValidationResponse.Content.ReadAsStringAsync();
				validationResponce = bool.Parse(validationStringResponce);
			}

			//Assert
			httpTokenResponse
				.Should()
				.NotBeNull();
			httpValidationResponse
				.Should()
				.NotBeNull();
			httpTokenResponse.StatusCode
				.Should()
				.Be(HttpStatusCode.OK);
			httpValidationResponse.StatusCode
				.Should()
				.Be(HttpStatusCode.OK);
			tokenResponse.Should()
				.NotBeNull();
			tokenResponse.access_token
				.Should()
				.NotBeNull();
			tokenResponse.token_type
				.Should()
				.Be("bearer");
			validationResponce
				.Should()
				.BeTrue();
		}


		[TestMethod]
		public async Task LogIn_When_Credentials_Are_Not_Correct_Responses_With_Bad_Request_Error()
		{
			//arange
			var stringContent = new StringContent("grant_type=password&username=notCorrect&password=notCorrect");
			var tokenURL = "http://localhost:8080/token";
			ErrorTokenResponseDto tokenResponce = null;
			HttpResponseMessage httpTokenResponse = null;

			//act
			using (var client = new HttpClient())
			{
				httpTokenResponse = await client.PostAsync(tokenURL, stringContent);
				var jsonResonce = await httpTokenResponse.Content.ReadAsStringAsync();
				tokenResponce = JsonConvert.DeserializeObject<ErrorTokenResponseDto>(jsonResonce);
			}

			//Assert
			httpTokenResponse
				.Should()
				.NotBeNull();
			httpTokenResponse.StatusCode
				.Should()
				.Be(HttpStatusCode.BadRequest);
			tokenResponce
				.Should()
				.NotBeNull();
			tokenResponce.error
				.Should()
				.NotBeNull();
			tokenResponce.error_description
				.Should()
				.NotBeNull();
			tokenResponce.error
				.Should()
				.Be("invalid_grant");
		}
	}

	public class TokenResponseDto
	{
		public string access_token;
		public string token_type;
		public int expires_in;
	}

	public class ErrorTokenResponseDto
	{
		public string error;
		public string error_description;
	}
}
