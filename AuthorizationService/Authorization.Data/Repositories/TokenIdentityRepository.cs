using Authentication.Core;
using Authentication.Interfaces;

namespace Authentication.Data
{
	public class TokenIdentityRepository : GenericRepository<TokenIdentity>,  ITokenIdentityRepository
	{
		public TokenIdentityRepository(AuthenticationDbContext context)
				 : base(context)
		{ }
	}
}
