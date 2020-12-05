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
			_uow = uow;
		}

		public User Authorize(string email, string password)
		{
			return _uow.Users.GetAll()
				.FirstOrDefault(x => x.Email == email && x.Password == password);
		}
	}
}
