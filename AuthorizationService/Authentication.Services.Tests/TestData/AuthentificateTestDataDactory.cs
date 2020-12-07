using System.Collections.Generic;
using Authentication.Core;
using Authentication.Interfaces;
using Authentication.Services;
using Authentication.Services.Intefaces;
using Moq;

namespace Authentification.Services.Tests
{
	public static class AuthentificateTestDataDactory
	{
		public const string UT_USER = "UT_USER";
		public const string UT_TOKEN = "TOKEN";

		public static IUnitOfWork ProvideUnitOfWork()
		{
			var uowMock = new Mock<IUnitOfWork>();

			var tokenIdentityRepo = new Mock<ITokenIdentityRepository>();
			tokenIdentityRepo.Setup(x => x.GetAll()).Returns(new List<TokenIdentity> { new TokenIdentity { Id = UT_USER, Token = UT_TOKEN } });

			var usersRepo = new Mock<IUserRepository>();
			usersRepo.Setup(x => x.GetAll()).Returns(new List<User> { new User { Id = "0", Email = UT_USER, Name = UT_USER, Password = UT_USER } });

			uowMock.Setup(x => x.TokenIdentities).Returns(tokenIdentityRepo.Object);
			uowMock.Setup(x => x.Users).Returns(usersRepo.Object);

			return uowMock.Object;
		}

		public static IAuthenticationService ProvideService()
		{
			var uow = ProvideUnitOfWork();

			var service = new AuthenticationService(uow);

			return service;
		}
	}
}
