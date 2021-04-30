using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly CinemabooContext _context;
        private EFUserRepository userRepository;
        private bool _disposed = false;

        public EFUnitOfWork(DbContextOptions<CinemabooContext> options)
        {
            _context = new CinemabooContext(options);
        }

        public IRepository<UserEntity> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new EFUserRepository(_context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
