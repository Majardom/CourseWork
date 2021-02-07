using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recognition.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISpeakerRepository Speakers { get; }

        void SaveChanges();
    }
}
