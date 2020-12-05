using System.Data.Entity;
using Authentication.Core;

namespace Authentication.Data
{
	public class AuthenticationDbContext : DbContext
	{
		public AuthenticationDbContext()
			:base("AuthentificationDb")
		{ }

		public DbSet<User> Users { get; set; }
		public DbSet<TokenIdentity> TokenIdentities { get; set; }
	}
}
