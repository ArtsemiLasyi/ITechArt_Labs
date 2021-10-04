using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SeatService
    {
        private readonly SeatRepository _seatRepository;

        public SeatService(SeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public async Task<int> CreateAsync(SeatModel seat)
        {
            SeatEntity? seatEntity = seat.Adapt<SeatEntity>();
            await _seatRepository.CreateAsync(seatEntity);
            return seatEntity.Id;
        }

        public Task CreateAsync(SeatsModel model)
        {
            IReadOnlyCollection<SeatEntity> entity = model.Value.Adapt<IReadOnlyCollection<SeatEntity>>();
            return _seatRepository.CreateAsync(entity);
        }

        public async Task<SeatsModel> GetAllByAsync(int hallId)
        {
            IReadOnlyCollection<SeatEntity> entities = await _seatRepository.GetAllBy(hallId);
            IReadOnlyCollection<SeatModel> seats = entities.Adapt<IReadOnlyCollection<SeatModel>>();
            return new SeatsModel(seats);
        }

        public async Task<SeatModel?> GetByAsync(int id)
        {
            SeatEntity? seatEntity = await _seatRepository.GetByAsync(id);
            return seatEntity?.Adapt<SeatModel>();
        }

        public Task<bool> DeleteByAsync(int id)
        {
            return _seatRepository.DeleteByAsync(id);
        }

        public Task EditAsync(SeatModel seat)
        {
            SeatEntity seatEntity = seat.Adapt<SeatEntity>();
            return _seatRepository.UpdateAsync(seatEntity);
        }

        public async Task EditAsync(int hallId, SeatsModel model)
        {
            await DeleteAllByAsync(hallId);
            await CreateAsync(model);
        }

        public Task DeleteAllByAsync(int hallId)
        {
            return _seatRepository.DeleteAllByAsync(hallId);
        }
    }
}
