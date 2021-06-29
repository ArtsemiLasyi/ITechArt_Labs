using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class HallService
    {
        private readonly HallRepository _hallRepository;
        private readonly SeatService _seatService;

        public HallService(
            HallRepository hallRepository,
            SeatService seatService)
        {
            _hallRepository = hallRepository;
            _seatService = seatService;
        }

        public async Task<int> CreateAsync(HallModel hall)
        {
            HallEntity? hallEntity = hall.Adapt<HallEntity>();
            int id = await _hallRepository.CreateAsync(hallEntity);
            return id;
        }

        public async Task<IReadOnlyCollection<HallModel>> GetAllByAsync(int cinemaId)
        {
            IReadOnlyCollection<HallEntity> models = await _hallRepository.GetAllByAsync(cinemaId);
            return models.Adapt<IReadOnlyCollection<HallModel>>();
        }

        public async Task<HallModel?> GetByAsync(int id)
        {
            HallEntity? hallEntity = await _hallRepository.GetByAsync(id);
            return hallEntity?.Adapt<HallModel>();
        }

        public Task<bool> DeleteByAsync(int id)
        {
            return _hallRepository.DeleteByAsync(id);
        }

        public async Task EditAsync(HallModel hall)
        {
            HallEntity hallEntity = hall.Adapt<HallEntity>();
            await _seatService.DeleteAllByAsync(hallEntity.Id);
            await _hallRepository.UpdateAsync(hallEntity);
        }
    }
}
