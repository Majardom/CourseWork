using Authentication.Core;
using Authentication.Interfaces;

namespace Authentication.Data
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(AuthenticationDbContext context)
			 : base(context)
		{ }
	}
}
