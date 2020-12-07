using System;
using System.Linq;
using Authentication.Core;
using Authentication.Interfaces;
using Authentication.Services.Intefaces;

namespace Authentication.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IUnitOfWork _uow;

		public AuthenticationService(IUnitOfWork uow)
		{
			if (uow == null)
				throw new ArgumentNullException(nameof(uow));

			_uow = uow;
		}

		public User Autheticate(string email, string password)
		{
			if (string.IsNullOrEmpty(email))
				throw new ArgumentNullException(nameof(email));
			if (string.IsNullOrEmpty(password))
				throw new ArgumentNullException(nameof(password));
			
			var authentificatedUser = _uow.Users.GetAll()
				.FirstOrDefault(x => x.Email == email && x.Password == password);

			if (authentificatedUser != null)
			{
				var ids = _uow.TokenIdentities.GetAll().Where(x => x.UserId == authentificatedUser.Id).Select(x => x.Id).ToList();
				ids.ForEach(x => _uow.TokenIdentities.Delete(x));
			}

			return authentificatedUser;
		}

		public void SaveTokenIdentity(TokenIdentity tokenIdentity)
		{
			_uow.TokenIdentities.Create(tokenIdentity);
			_uow.TokenIdentities.Save();
		}

		public bool Validate(string token)
		{
			return _uow.TokenIdentities.GetAll().Where(x => x.Token == token).Any();
		}
	}
}
