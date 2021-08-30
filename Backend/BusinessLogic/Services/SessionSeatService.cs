using BusinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SessionSeatService
    {
        private readonly SessionSeatRepository _seatRepository;

        public SessionSeatService(SessionSeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        public Task CreateAsync(SessionSeatModel seat)
        {
            SessionSeatEntity? seatEntity = seat.Adapt<SessionSeatEntity>();
            return _seatRepository.CreateAsync(seatEntity);
        }

        public Task CreateAsync(SessionSeatsModel model)
        {
            IReadOnlyCollection<SessionSeatEntity> entity = model.SessionSeats.Adapt<IReadOnlyCollection<SessionSeatEntity>>();
            return _seatRepository.CreateAsync(entity);
        }

        public async Task<SessionSeatsModel> GetAllByAsync(int hallId)
        {
            IReadOnlyCollection<SessionSeatEntity> entities = await _seatRepository.GetAllBy(hallId);
            IReadOnlyCollection<SessionSeatModel> seats = entities.Adapt<IReadOnlyCollection<SessionSeatModel>>();
            return new SessionSeatsModel(seats);
        }

        public Task<bool> DeleteByAsync(int id)
        {
            return _seatRepository.DeleteByAsync(id);
        }

        public Task EditAsync(SessionSeatModel seat)
        {
            SessionSeatEntity seatEntity = seat.Adapt<SessionSeatEntity>();
            return _seatRepository.UpdateAsync(seatEntity);
        }

        public async Task EditAsync(int sessionId, SessionSeatsModel model)
        {
            await DeleteAllByAsync(sessionId);
            await CreateAsync(model);
        }

        public Task DeleteAllByAsync(int sessionId)
        {
            return _seatRepository.DeleteAllByAsync(sessionId);
        }
    }
}
