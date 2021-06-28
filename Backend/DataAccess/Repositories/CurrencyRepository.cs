using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CurrencyRepository
    {
        private readonly CinemabooContext _context;

        public CurrencyRepository(CinemabooContext context)
        {
            _context = context;
        }

        public Task CreateAsync(CurrencyEntity currency)
        {
            _context.Currencies.Add(currency);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteByAsync(int id)
        {
            CurrencyEntity? currency = await _context.Currencies.FindAsync(id);
            if (currency != null)
            {
                _context.Currencies.Remove(currency);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IReadOnlyCollection<CurrencyEntity>> GetAllAsync()
        {
            List<CurrencyEntity> currencys = await _context.Currencies.ToListAsync();
            return currencys;
        }

        public ValueTask<CurrencyEntity?> GetByAsync(int id)
        {
            // This measure is temporary. The directive will be removed with the release of EF 6.0
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            return _context.Currencies.FindAsync(id);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }

        public Task UpdateAsync(CurrencyEntity currency)
        {
            _context.Currencies.Update(currency);
            return _context.SaveChangesAsync();
        }

        public async Task<bool> CheckUsedAsync(int id)
        {
            CinemaServiceEntity? entity = await _context
                .CinemaServices
                .FirstOrDefaultAsync(entity => entity.CurrencyId == id);
            if (entity != null)
            {
                return true;
            }
            return false;
        }
    }
}
