using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);

        T FindFirst(Func<T, Boolean> predicate);

        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
