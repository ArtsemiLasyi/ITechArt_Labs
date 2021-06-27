using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ServiceRepository
    {
        private readonly CinemabooContext _context;

        public ServiceRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(ServiceEntity service)
        {
            _context.Services.Add(service);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            ServiceEntity? service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<ServiceEntity>> GetAllAsync()
        {
            List<ServiceEntity> services = await _context.Services.ToListAsync();
            return services;
        }

        public ValueTask<ServiceEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Services.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(ServiceEntity service)
        {
            _context.Services.Update(service);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> IsUsedAsync(int id)
        {
            ServiceCinemaEntity? entity = await _context.ServiceCinemas.FirstOrDefaultAsync(entity => entity.ServiceId == id);
            if (entity != null)
            {
                return true;
            }
            return false;
        }
    }
}
