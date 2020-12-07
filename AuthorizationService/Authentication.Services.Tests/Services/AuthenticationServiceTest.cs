using System;
using Authentication.Interfaces;
using Authentication.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Authentification.Services.Tests.Services
{
	[TestClass]
	public class AuthenticationServiceTest
	{
		[TestMethod]
		public void Construct_UnitOfWork_Is_Null_Throws()
		{
			//arrange
			IUnitOfWork uow = null;

			//act
			Action act = () => { var sut = new AuthenticationService(uow); };

			//assert
			act.Should()
				.Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void Construct_UnitOfWork_Is_Set_Correctly_Constructs_Succesfully()
		{
			//arrange
			IUnitOfWork uow = AuthentificateTestDataDactory.ProvideUnitOfWork();

			//act
			var sut = new AuthenticationService(uow);
		}

		[TestMethod]
		public void Authorize_Not_Existing_User_Email_Returns_Null()
		{
			//arrange
			var sut = AuthentificateTestDataDactory.ProvideService();
			var email = "NOT_EXISTS";
			var password = "PASS";

			//act
			var user = sut.Autheticate(email, password);

			//assert
			user.Should()
				.BeNull();
		}

		[TestMethod]
		public void Authorize_Incorrect_Password_Returns_Null()
		{
			//arrange
			var sut = AuthentificateTestDataDactory.ProvideService();
			var email = AuthentificateTestDataDactory.UT_USER;
			var password = "WRONG_PASSWORD";

			//act
			var user = sut.Autheticate(email, password);

			//assert
			user.Should()
				.BeNull();
		}

		[TestMethod]
		public void Authorize_Data_Is_Correct_Returns_User()
		{
			//arrange
			var email = AuthentificateTestDataDactory.UT_USER;
			var password = AuthentificateTestDataDactory.UT_USER;

			var sut = AuthentificateTestDataDactory.ProvideService();

			//act
			var user = sut.Autheticate(email, password);

			//assert
			user.Should()
				.NotBeNull();

			user.Name
				.Should()
				.Be(AuthentificateTestDataDactory.UT_USER);

			user.Password
				.Should()
				.Be(password);
			
			user.Email
				.Should()
				.Be(email);
		}

		[TestMethod]
		public void Authenticate_Correct_Data_Performs_Less_Than_One_Second()
		{
			//arrange
			var email = AuthentificateTestDataDactory.UT_USER;
			var password = AuthentificateTestDataDactory.UT_USER;

			var sut = AuthentificateTestDataDactory.ProvideService();

			var stopWatch = new Stopwatch();

			//act
			stopWatch.Start();

			var user = sut.Autheticate(email, password);

			stopWatch.Stop();

			//assert
			user.Should()
				.NotBeNull();

			user.Name
				.Should()
				.Be(AuthentificateTestDataDactory.UT_USER);

			user.Password
				.Should()
				.Be(password);

			user.Email
				.Should()
				.Be(email);

			stopWatch.ElapsedMilliseconds
				.Should()
				.BeLessThan(1000);
		}
	}
}
