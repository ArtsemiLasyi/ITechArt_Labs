using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SessionSeatsRequest request)
        {
            SessionSeatModel model = request.Adapt<SessionSeatModel>();
            await _sessionSeatService.CreateAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Take([FromBody] SessionSeatRequest request)
        {
            SessionSeatModel model = request.Adapt<SessionSeatModel>();
            await _sessionSeatService.TakeAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Free([FromBody] SessionSeatRequest request)
        {
            SessionSeatModel model = request.Adapt<SessionSeatModel>();
            await _sessionSeatService.FreeAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int sessionId, [FromBody] SessionSeatsRequest request)
        {
            SessionSeatsModel model = request.Adapt<SessionSeatsModel>();
            await _sessionSeatService.EditAsync(sessionId, model);
            return Ok();
        }
    }
}
