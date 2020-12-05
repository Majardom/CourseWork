using System;
using Authentication.Interfaces;

namespace Authorization.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		public IUserRepository Users { get; }

		public ITokenIdentityRepository TokenIdentities { get; }

	
        public UnitOfWork(IUserRepository users,
          ITokenIdentityRepository tokenIdentities)
        {
            Users = users;

            TokenIdentities = tokenIdentities;
        }

        public void SaveChanges()
        {
            Users.Save();
            TokenIdentities.Save();
        }

        #region IDisposable

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                Users.Dispose();
                TokenIdentities.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}
