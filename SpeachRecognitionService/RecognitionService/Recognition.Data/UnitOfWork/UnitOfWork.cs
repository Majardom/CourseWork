using System;
using Recognition.Interfaces;
using SpeackerRecognition.Core;

namespace Recognition.Data
{
	public class UnitOfWork : IUnitOfWork
    {
        public ISpeakerRepository Speakers { get; }

        public UnitOfWork(ISpeakerRepository speakers)
        {
            Speakers = speakers;   
        }

        public void SaveChanges()
        {
            Speakers.Save();
        }

        #region IDisposable

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;

            if (disposing)
            {
                Speakers.Dispose();
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
