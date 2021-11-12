using BusinessLogic.Models;
using BusinessLogic.Options;
using BusinessLogic.Statuses;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SessionSeatService
    {
        private readonly SessionSeatRepository _sessionSeatRepository;
        private readonly SeatOptions _seatSnapshotOptions;

        public SessionSeatService(
            SessionSeatRepository seatRepository,
            IOptionsSnapshot<SeatOptions> seatOptionsSnapshotAssessor)
        {
            _sessionSeatRepository = seatRepository;
            _seatSnapshotOptions = seatOptionsSnapshotAssessor.Value;
        }

        public Task CreateAsync(SessionSeatModel seat)
        {
            SessionSeatEntity? seatEntity = seat.Adapt<SessionSeatEntity>();
            return _sessionSeatRepository.CreateAsync(seatEntity);
        }

        public Task CreateAsync(SessionSeatsModel model)
        {
            IReadOnlyCollection<SessionSeatEntity> entities = model.Value.Adapt<IReadOnlyCollection<SessionSeatEntity>>();
            return _sessionSeatRepository.CreateAsync(entities);
        }

        public Task UpdateSeatStatusesAsync(int sessionId, SessionSeatsModel sessionSeats)
        {
            IReadOnlyCollection<SessionSeatEntity> entities = sessionSeats.Value.Adapt<IReadOnlyCollection<SessionSeatEntity>>();
            return _sessionSeatRepository.UpdateStatusesAsync(sessionId, entities);
        }

        public async Task<SessionSeatsModel> GetAllByAsync(int sessionId)
        {
            IReadOnlyCollection<SessionSeatEntity> entities = await _sessionSeatRepository.GetAllByAsync(sessionId);
            IReadOnlyCollection<SessionSeatModel> sessionSeats = entities.Adapt<IReadOnlyCollection<SessionSeatModel>>();
            return new SessionSeatsModel(sessionSeats);
        }

        public Task<int> GetNumberOfFreeSeatsAsync(int sessionId)
        {
            return _sessionSeatRepository.GetNumberOfFreeSeatsAsync(sessionId);
        }

        public async Task OrderAsync(SessionSeatsModel sessionSeats)
        {
            foreach (var sessionSeat in sessionSeats.Value)
            {
                await OrderAsync(sessionSeat);
            }
        }

        public async Task<SessionSeatModel?> GetByAsync(int sessionId, int seatId)
        {
            SessionSeatEntity? entity = await _sessionSeatRepository.GetByAsync(sessionId, seatId);
            return entity?.Adapt<SessionSeatModel>();
        }

        public Task<bool> DeleteByAsync(int id)
        {
            return _sessionSeatRepository.DeleteByAsync(id);
        }

        public async Task<SeatTakeStatus> TakeAsync(SessionSeatModel model)
        {
            SeatTakeStatus status = await GetStatusAsync(model);
            if (status == SeatTakeStatus.Taken || status == SeatTakeStatus.Ordered)
            {
                return status;
            }
            model.TakenAt = DateTime.UtcNow;
            model.Status = SessionSeatStatus.Taken;
            SessionSeatEntity seatEntity = model.Adapt<SessionSeatEntity>();
            if (status == SeatTakeStatus.Empty)
            {
                await _sessionSeatRepository.CreateAsync(seatEntity);
                return status;
            }
            await _sessionSeatRepository.UpdateAsync(seatEntity);
            return status;
        }

        public async Task FreeAsync(SessionSeatModel model)
        {
            model.Status = SessionSeatStatus.Free;
            SessionSeatEntity seatEntity = model.Adapt<SessionSeatEntity>();
            await _sessionSeatRepository.UpdateAsync(seatEntity);
        }

        public async Task OrderAsync(SessionSeatModel model)
        {
            model.Status = SessionSeatStatus.Ordered;
            SessionSeatEntity seatEntity = model.Adapt<SessionSeatEntity>();
            await _sessionSeatRepository.UpdateAsync(seatEntity);
        }

        public async Task EditAsync(int sessionId, SessionSeatsModel model)
        {
            await DeleteAllByAsync(sessionId);
            await CreateAsync(model);
        }

        public Task DeleteAllByAsync(int sessionId)
        {
            return _sessionSeatRepository.DeleteAllByAsync(sessionId);
        }

        private async Task<SeatTakeStatus> GetStatusAsync(SessionSeatModel model)
        {
            SessionSeatModel? oldModel = await GetByAsync(model.SessionId, model.SeatId);
            if (oldModel == null)
            {
                return SeatTakeStatus.Empty;
            }
            if (oldModel.Status == SessionSeatStatus.Ordered)
            {
                return SeatTakeStatus.Ordered;
            }
            if (oldModel.Status == SessionSeatStatus.Taken 
                && (model.TakenAt - oldModel.TakenAt <= _seatSnapshotOptions.SeatOccupancyInterval))
            {
                return SeatTakeStatus.Taken;
            }
            if (oldModel.Status == SessionSeatStatus.Taken
                && (model.TakenAt - oldModel.TakenAt > _seatSnapshotOptions.SeatOccupancyInterval))
            {
                return SeatTakeStatus.NeedToFree;
            }
            return SeatTakeStatus.Free;
        }
    }

}
