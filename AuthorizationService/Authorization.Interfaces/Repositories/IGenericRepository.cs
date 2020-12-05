using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Interfaces
{
    public interface IGenericRepository<T> : IDisposable
    {
        IEnumerable<T> GetAll();

        T Get(int id);

        void Create(T item);

        void Update(T item);

        void Delete(int id);

        void Save();
    }
}
