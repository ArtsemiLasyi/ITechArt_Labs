using BusinessLogic.Models;
using BusinessLogic.Statuses;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SeatTypeService
    {
        private readonly SeatTypeRepository _seatTypeRepository;

        public SeatTypeService(SeatTypeRepository seatTypeRepository)
        {
            _seatTypeRepository = seatTypeRepository;
        }

        public async Task<int> CreateAsync(SeatTypeModel seatType)
        {
            SeatTypeEntity? seatTypeEntity = seatType.Adapt<SeatTypeEntity>();
            await _seatTypeRepository.CreateAsync(seatTypeEntity);
            return seatTypeEntity.Id;
        }

        public async Task<SeatTypeModel?> GetByAsync(int id)
        {
            SeatTypeEntity? seatTypeEntity = await _seatTypeRepository.GetByAsync(id);
            return seatTypeEntity?.Adapt<SeatTypeModel>();
        }

        public async Task<IReadOnlyCollection<SeatTypeModel>> GetAllAsync()
        {
            IReadOnlyCollection<SeatTypeEntity> models = await _seatTypeRepository.GetAllAsync();
            return models.Adapt<IReadOnlyCollection<SeatTypeModel>>();
        }

        public async Task<SeatTypeDeletionStatus> DeleteByAsync(int id)
        {
            SeatTypeEntity? seatTypeEntity = await _seatTypeRepository.GetByAsync(id);
            if (seatTypeEntity == null)
            {
                return SeatTypeDeletionStatus.NotFound;
            }
            if (await _seatTypeRepository.IsUsedAsync(id))
            {
                return SeatTypeDeletionStatus.ForbiddenAsUsed;
            }
            if (await _seatTypeRepository.DeleteByAsync(id))
            {
                return SeatTypeDeletionStatus.Successful;
            }
            return SeatTypeDeletionStatus.NotFound;
        }

        public Task EditAsync(SeatTypeModel seatType)
        {
            SeatTypeEntity seatTypeEntity = seatType.Adapt<SeatTypeEntity>();
            return _seatTypeRepository.UpdateAsync(seatTypeEntity);
        }
    }
}
