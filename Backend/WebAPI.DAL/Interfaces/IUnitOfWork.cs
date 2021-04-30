using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<UserEntity> Users { get; }
        void Save();
    }
}
