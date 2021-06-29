using BusinessLogic.Models;
using BusinessLogic.Statuses;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class CurrencyService
    {
        private readonly CurrencyRepository _currencyRepository;

        public CurrencyService(CurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<CurrencyModel> CreateAsync(CurrencyModel currency)
        {
            CurrencyEntity currencyEntity = currency.Adapt<CurrencyEntity>();
            await _currencyRepository.CreateAsync(currencyEntity);
            return currencyEntity.Adapt<CurrencyModel>();
        }

        public async Task<CurrencyModel?> GetByAsync(int id)
        {
            CurrencyEntity? currency = await _currencyRepository.GetByAsync(id);
            if (currency == null)
            {
                return null;
            }
            return currency.Adapt<CurrencyModel>();
        }

        public async Task<IReadOnlyCollection<CurrencyModel>> GetAsync()
        {
            IReadOnlyCollection<CurrencyEntity> models = await _currencyRepository.GetAsync();
            return models.Adapt<IReadOnlyCollection<CurrencyModel>>();
        }

        public async Task<CurrencyDeletionStatus> DeleteByAsync(int id)
        {
            CurrencyEntity? seatTypeEntity = await _currencyRepository.GetByAsync(id);
            if (seatTypeEntity == null)
            {
                return CurrencyDeletionStatus.NotFound;
            }
            if (await _currencyRepository.CheckUsedAsync(id))
            {
                return CurrencyDeletionStatus.ForbiddenAsUsed;
            }
            if (await _currencyRepository.DeleteByAsync(id))
            {
                return CurrencyDeletionStatus.Successful;
            }
            return CurrencyDeletionStatus.NotFound;
        }

        public Task EditAsync(CurrencyModel model)
        {
            CurrencyEntity currencyEntity = model.Adapt<CurrencyEntity>();
            return _currencyRepository.UpdateAsync(currencyEntity);
        }
    }
}
