using System;

namespace Authentication.Interfaces
{
	public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        ITokenIdentityRepository TokenIdentities { get; }

        void SaveChanges();
    }
}
