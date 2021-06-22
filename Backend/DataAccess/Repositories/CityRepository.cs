using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CityRepository
    {
        private readonly CinemabooContext _context;

        public CityRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(CityEntity entity)
        {
            _context.Cities.Add(entity);
            return _context.SaveChangesAsync();
        }

        public Task<CityEntity?> GetByAsync(string name)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Cities.FirstOrDefaultAsync(
                city => city.Name == name
            );
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public ValueTask<CityEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Cities.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public async Task<IReadOnlyCollection<CityEntity>> GetAllAsync()
        {
            List<CityEntity> cities = await _context.Cities.ToListAsync();
            return cities;
        }
    }
}
