using BusinessLogic.Models;
using BusinessLogic.Parameters;
using DataAccess.Entities;
using DataAccess.Parameters;
using DataAccess.Repositories;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SessionService
    {
        private readonly SessionRepository _sessionRepository;

        public SessionService(SessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public Task<int> CreateAsync(SessionModel session)
        {
            SessionEntity? sessionEntity = session.Adapt<SessionEntity>();
            return _sessionRepository.CreateAsync(sessionEntity);
        }

        public async Task<IReadOnlyCollection<SessionModel>> GetAllByAsync(int cinemaId, SessionModelSearchParameters parameters)
        {
            IReadOnlyCollection<SessionEntity> models = await _sessionRepository.GetAllByAsync(
                cinemaId,
                parameters.Adapt<SessionEntitySearchParameters>()
            );
            return models.Adapt<IReadOnlyCollection<SessionModel>>();
        }

        public async Task<SessionModel?> GetByAsync(int id)
        {
            SessionEntity? sessionEntity = await _sessionRepository.GetByAsync(id);
            return sessionEntity?.Adapt<SessionModel>();
        }

        public async Task EditAsync(SessionModel session)
        {
            SessionEntity sessionEntity = session.Adapt<SessionEntity>();
            await _sessionRepository.UpdateAsync(sessionEntity);
        }

        public Task<bool> DeleteByAsync(int id)
        {
            return _sessionRepository.DeleteByAsync(id);
        }
    }
}
