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

        public HallService(HallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }

        public async Task<int> CreateAsync(HallModel hall)
        {
            HallEntity? hallEntity = hall.Adapt<HallEntity>();
            await _hallRepository.CreateAsync(hallEntity);
            return hallEntity.Id;
        }

        public async Task<IReadOnlyCollection<HallModel>> GetAllByAsync(int cinemaId)
        {
            IReadOnlyCollection<HallEntity> models = await _hallRepository.GetAllByAsync(cinemaId);
            List<HallModel> result = new List<HallEntity>(models).Adapt<List<HallModel>>();
            return result;
        }

        public async Task<HallModel?> GetByAsync(int id)
        {
            HallEntity? user = await _hallRepository.GetByAsync(id);
            return user?.Adapt<HallModel>();
        }

        public Task<bool> DeleteByAsync(int id)
        {
            return _hallRepository.DeleteByAsync(id);
        }

        public Task EditAsync(HallModel hall)
        {
            HallEntity hallEntity = hall.Adapt<HallEntity>();
            return _hallRepository.UpdateAsync(hallEntity);
        }
    }
}
