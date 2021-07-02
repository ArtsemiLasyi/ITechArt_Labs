using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CinemaServiceRepository
    {
        private readonly CinemabooContext _context;

        public CinemaServiceRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(CinemaServiceEntity entity)
        {
            _context.CinemaServices.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(CinemaServiceEntity entity)
        {
            _context.CinemaServices.Update(entity);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int serviceId, int cinemaId)
        {
            CinemaServiceEntity? service = await GetByAsync(serviceId, cinemaId);
            if (service != null)
            {
                _context.CinemaServices.Remove(service);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<CinemaServiceEntity?> GetByAsync(int serviceId, int cinemaId)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.CinemaServices
                .Include("Currencies")
                .FirstOrDefaultAsync(
                    cinemaService => cinemaService.CinemaId == cinemaId && cinemaService.ServiceId == serviceId
                );
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public async Task<IReadOnlyCollection<CinemaServiceEntity>> GetAllByAsync(int cinemaId)
        {
            List<CinemaServiceEntity> entities = await _context.CinemaServices
                .Where(cinemaService => cinemaService.CinemaId == cinemaId)
                .ToListAsync();
            return entities;
        }
    }
}
