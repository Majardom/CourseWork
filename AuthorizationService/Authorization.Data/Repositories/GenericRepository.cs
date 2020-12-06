using System;
using System.Collections.Generic;
using System.Data.Entity;
using Authentication.Interfaces;

namespace Authentication.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T: class
	{
		protected readonly AuthenticationDbContext DbContext;
		protected readonly DbSet<T> Entities;

		public GenericRepository(AuthenticationDbContext context)
		{
			DbContext = context;

			Entities = DbContext.Set<T>();

			Entities.Load();
		}

		public void Create(T item)
		{
			Entities.Add(item);
		}

		public void Delete(string id)
		{
			Entities.Remove(Entities.Find(id));
		}


		public T Get(string id)
		{
			return Entities.Find(id);
		}

		public IEnumerable<T> GetAll()
		{
			return Entities;
		}

		public void Save()
		{
			DbContext.SaveChanges();
		}

		public void Update(T item)
		{
			throw new NotImplementedException();
		}

		#region IDisposable

		private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (this.disposed)
				return;

			if (disposing)
			{
				DbContext.Dispose();
			}

			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~GenericRepository()
		{
			Dispose(false);
		}

		#endregion

	}
}
