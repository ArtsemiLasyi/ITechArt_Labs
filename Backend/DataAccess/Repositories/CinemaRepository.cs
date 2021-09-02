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
    public class CinemaRepository
    {
        private readonly CinemabooContext _context;

        public CinemaRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(CinemaEntity cinema)
        {
            _context.Cinemas.Add(cinema);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            CinemaEntity? cinema = await _context.Cinemas.FindAsync(id);
            if (cinema?.IsDeleted == false)
            {
                cinema.IsDeleted = true;
                _context.Update(cinema);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<CinemaEntity>> GetAllByAsync(int cityId, CinemaEntitySearchParameters parameters)
        {
            string separator = " ";
            IQueryable<CinemaEntity> query = _context.Cinemas
                .Where(cinema => !cinema.IsDeleted && cinema.CityId == cityId);

            if (!string.IsNullOrEmpty(parameters.CinemaName))
            {
                string[] substrings = parameters.CinemaName.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                foreach (string substring in substrings)
                {
                    query = query.Where(cinema => cinema.Name.Contains(substring));
                }
            }

            List<CinemaEntity> cinemas = await query.ToListAsync();
            return cinemas;
        }

        public async Task<CinemaEntity?> GetByAsync(int id)
        {
            CinemaEntity? entity = await _context.Cinemas.FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            await _context.Entry(entity).Reference(cinema => cinema.City).LoadAsync();
            return entity;
        }

        public Task UpdateAsync(CinemaEntity cinema)
        {
            _context.Cinemas.Update(cinema);
            return _context.SaveChangesAsync();
        }
    }
}
