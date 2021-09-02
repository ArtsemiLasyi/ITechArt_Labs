using BusinessLogic.Models;
using BusinessLogic.Statuses;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SessionSeatService
    {
        private readonly SessionSeatRepository _sessionSeatRepository;

        public SessionSeatService(SessionSeatRepository seatRepository)
        {
            _sessionSeatRepository = seatRepository;
        }

        public Task CreateAsync(SessionSeatModel seat)
        {
            SessionSeatEntity? seatEntity = seat.Adapt<SessionSeatEntity>();
            return _sessionSeatRepository.CreateAsync(seatEntity);
        }

        public Task CreateAsync(SessionSeatsModel model)
        {
            IReadOnlyCollection<SessionSeatEntity> entity = model.Value.Adapt<IReadOnlyCollection<SessionSeatEntity>>();
            return _sessionSeatRepository.CreateAsync(entity);
        }

        public Task UpdateSeatStatuses(SessionSeatsModel sessionSeats)
        {
            return _sessionSeatRepository.UpdateStatusesAsync();
        }

        public async Task<SessionSeatsModel> GetAllByAsync(int sessionId)
        {
            IReadOnlyCollection<SessionSeatEntity> entities = await _sessionSeatRepository.GetAllByAsync(sessionId);
            IReadOnlyCollection<SessionSeatModel> sessionSeats = entities.Adapt<IReadOnlyCollection<SessionSeatModel>>();
            return new SessionSeatsModel(sessionSeats);
        }

        public int GetNumberOfFreeSeats(int sessionId)
        {
            return _sessionSeatRepository.GetNumberOfFreeSeats(sessionId);
        }

        public async Task OrderAsync(int sessionId, SeatsModel seats)
        {
            SessionSeatsModel sessionSeats = await GetAllByAsync(sessionId);
            foreach (SessionSeatModel sessionSeat in sessionSeats.Value)
            {
                foreach (SeatModel seat in seats.Seats)
                {
                    if (seat.Id == sessionSeat.SeatId)
                    {
                        await OrderAsync(sessionSeat);
                    }
                }
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
            if (status == SeatTakeStatus.Free || status == SeatTakeStatus.NeedToFree)
            {
                model.TakenAt = DateTime.UtcNow;
                model.Status = SessionSeatStatus.Taken;
                SessionSeatEntity seatEntity = model.Adapt<SessionSeatEntity>();
                await _sessionSeatRepository.CreateAsync(seatEntity);
            }
            return status;
        }

        public async Task FreeAsync(SessionSeatModel model)
        {
            model.Status = SessionSeatStatus.Free;
            SessionSeatEntity seatEntity = model.Adapt<SessionSeatEntity>();
            await DeleteAsync(model);
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
                return SeatTakeStatus.Free;
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
