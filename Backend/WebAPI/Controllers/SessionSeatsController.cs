using BusinessLogic.Models;
using BusinessLogic.Services;
using BusinessLogic.Statuses;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Constants;
using WebAPI.Parameters;
using WebAPI.Requests;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("sessions/{sessionId}/seats")]
    public class SessionSeatsController : ControllerBase
    {
        private readonly SessionSeatService _sessionSeatService;

        public SessionSeatsController(SessionSeatService seatService)
        {
            _sessionSeatService = seatService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int sessionId)
        {
            SessionSeatsModel model = await _sessionSeatService.GetAllByAsync(sessionId);
            SessionSeatsResponse response = model.Adapt<SessionSeatsResponse>();
            return Ok(response);
        }

        [HttpGet("{seatId}")]
        public async Task<IActionResult> Get(int sessionId, int seatId)
        {
            SessionSeatModel? model = await _sessionSeatService.GetByAsync(sessionId, seatId);
            if (model == null)
            {
                return NotFound();
            }
            SessionSeatResponse response = model.Adapt<SessionSeatResponse>();
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int sessionId, [FromBody] SessionSeatsRequest request)
        {
            SessionSeatsModel model = request.Adapt<SessionSeatsModel>();
            await _sessionSeatService.UpdateSeatStatusesAsync(sessionId, model);
            return Ok();
        }

        [HttpPut("{seatId}")]
        public async Task<IActionResult> Modify(
            [FromQuery] SessionSeatRequestQueryParameters parameters, 
            [FromBody] SessionSeatRequest request)
        {
            if (parameters.Free == null && parameters.Take == null)
            {
                return BadRequest(new { errortext = "There is no option selected" });
            }
            if (parameters.Free != null && parameters.Take != null)
            {
                return BadRequest(new { errorText = "Only one option allowed" });
            }

            SessionSeatModel model = request.Adapt<SessionSeatModel>();
            if (parameters.Take != null)
            {
                if (parameters.Take.Value)
                {
                    SeatTakeStatus status = await _sessionSeatService.TakeAsync(model);
                    return Ok(status);
                }
            }
            if (parameters.Free != null)
            {
                if (parameters.Free.Value)
                {
                    await _sessionSeatService.FreeAsync(model);
                }
            }

            return Ok();
        }
    }
}
