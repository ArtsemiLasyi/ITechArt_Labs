using DataAccess.Contexts;
using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DataAccess.Parameters;
using System;

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

        public async Task<IReadOnlyCollection<FilmEntity>> GetAsync(int pageNumber, int pageSize, FilmEntitySearchParameters parameters)
        {
            string separator = " ";
            IQueryable<FilmEntity> query = _context.Films
                .Where(film => !film.IsDeleted);

            if (!string.IsNullOrEmpty(parameters.FilmName))
            {
                string[] substrings = parameters.FilmName.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                foreach (string substring in substrings)
                {
                    query = query.Where(film => film.Name.Contains(substring));
                }
            }

            if (parameters.CinemaId != null)
            {
                query = query.Where(
                    film =>
                        _context.Sessions
                            .Where(
                                session =>
                                    session.FilmId == film.Id
                                        && _context.Halls
                                            .Where(
                                                hall => hall.CinemaId == parameters.CinemaId
                                            )
                                            .Any()
                            )
                            .Any()
                        );
            }

            if (parameters.FirstSessionDateTime != null)
            {
                query = query.Where(
                    film => 
                        _context.Sessions
                            .Where(
                                session => 
                                    parameters.FirstSessionDateTime >= session.StartDateTime
                                        && session.FilmId == film.Id
                            )
                            .Any()
                        );
            }

            if (parameters.LastSessionDateTime != null)
            {
                query = query.Where(
                    film =>
                        _context.Sessions
                            .Where(
                                session =>
                                    parameters.LastSessionDateTime <= session.StartDateTime
                                        && session.FilmId == film.Id
                            )
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
