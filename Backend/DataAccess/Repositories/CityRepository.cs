using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IReadOnlyCollection<CityEntity>> GetAsync(CityEntitySearchParameters parameters)
        {
            string separator = " ";
            IQueryable<CityEntity> query = _context.Cities;
            if (!string.IsNullOrEmpty(parameters.CityName))
            {
                string[] substrings = parameters.CityName.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                foreach (string substring in substrings)
                {
                    query = query.Where(city => city.Name.Contains(substring));
                }
            }
            List<CityEntity> cities = await query.ToListAsync();
            return cities;
        }
    }
}
