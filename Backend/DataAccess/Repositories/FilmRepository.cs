using DataAccess.Contexts;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataAccess.Parameters;
using DataAccess.Services;

namespace DataAccess.Repositories
{
    public class FilmRepository
    {
        private readonly CinemabooContext _context;

        public FilmRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(FilmEntity film)
        {
            _context.Films.Add(film);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            FilmEntity? film = await _context.Films.FindAsync(id);

            if (film?.IsDeleted == false)
            {
                film.IsDeleted = true;
                _context.Films.Update(film);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<FilmEntity>> GetAsync(int pageNumber, int pageSize, FilmParameters parameters)
        {
            IQueryable<FilmEntity> query = _context.Films
                .Where(
                    film =>
                        !film.IsDeleted
                        && _context.Sessions.Where(session => session.FilmId == film.Id).Any()
                );

            if (!string.IsNullOrEmpty(parameters.Name))
            {
                query = query.Where(film => SearchService.Contains(film.Name, parameters.Name));
            }

            if (parameters.StartDateTime != null)
            {
                query = query.Where(
                    film => 
                        _context.Sessions
                            .Where(
                                session => 
                                    parameters.StartDateTime > session.StartDateTime)
                            .Any()
                        );
            }

            if (parameters.EndDateTime != null)
            {
                query = query.Where(
                    film =>
                        _context.Sessions
                            .Where(
                                session =>
                                    parameters.EndDateTime < session.StartDateTime)
                            .Any()
                        );
            }

            List<FilmEntity> films = await query.OrderByDescending(on => on.ReleaseYear)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return films;
        }

        public ValueTask<FilmEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Films.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(FilmEntity film)
        {
            _context.Films.Update(film);
            return _context.SaveChangesAsync();
        }
    }
}
