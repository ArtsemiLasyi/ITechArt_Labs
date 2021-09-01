using BusinessLogic.Models;
using BusinessLogic.Options;
using BusinessLogic.Statuses;
using DataAccess.Entities;
using DataAccess.Repositories;
using Mapster;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class SessionSeatService
    {
        private readonly SessionSeatRepository _sessionSeatRepository;
        private readonly SeatOprions _seatSnapshotOptions;

        public SessionSeatService(
            SessionSeatRepository seatRepository,
            IOptionsSnapshot<SeatOprions> seatSnapshotOptionsAccessor)
        {
            _sessionSeatRepository = seatRepository;
            _seatSnapshotOptions = seatSnapshotOptionsAccessor.Value;
        }

        public Task CreateAsync(SessionSeatModel seat)
        {
            SessionSeatEntity? seatEntity = seat.Adapt<SessionSeatEntity>();
            return _sessionSeatRepository.CreateAsync(seatEntity);
        }

        public Task CreateAsync(SessionSeatsModel model)
        {
            IReadOnlyCollection<SessionSeatEntity> entity = model.SessionSeats.Adapt<IReadOnlyCollection<SessionSeatEntity>>();
            return _sessionSeatRepository.CreateAsync(entity);
        }

        public async Task UpdateSeatStatuses(SessionSeatsModel sessionSeats)
        {
            foreach (SessionSeatModel model in sessionSeats.SessionSeats)
            {
                SeatTakeStatus status = await GetStatusAsync(model);
                if (status == SeatTakeStatus.NeedToFree)
                {
                    await FreeAsync(model);
                }
            }
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
            foreach (SessionSeatModel sessionSeat in sessionSeats.SessionSeats)
            {
                foreach (SeatModel seat in seats.Seats)
                {
                    if(seat.Id == sessionSeat.SeatId)
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
            model.TakenAt = DateTime.UtcNow;
            model.Status = SessionSeatStatus.Taken;
            SeatTakeStatus status = await GetStatusAsync(model);
            if (status == SeatTakeStatus.Free || status == SeatTakeStatus.NeedToFree)
            {
                SessionSeatEntity seatEntity = model.Adapt<SessionSeatEntity>();
                await _sessionSeatRepository.CreateAsync(seatEntity);
            }
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
            SessionSeatModel? oldEntity = await GetByAsync(model.SessionId, model.SeatId);
            if (oldEntity == null)
            {
                return SeatTakeStatus.Error;
            }
            if (model.Status == SessionSeatStatus.Ordered)
            {
                return SeatTakeStatus.Ordered;
            }
            if (oldEntity.Status == SessionSeatStatus.Taken 
                && (model.TakenAt - oldEntity.TakenAt <= _seatSnapshotOptions.SeatOccupancyInterval))
            {
                return SeatTakeStatus.Taken;
            }
            if (oldEntity.Status == SessionSeatStatus.Taken
                && (model.TakenAt - oldEntity.TakenAt > _seatSnapshotOptions.SeatOccupancyInterval))
            {
                return SeatTakeStatus.NeedToFree;
            }
            return SeatTakeStatus.Free;
        }

    }

}
