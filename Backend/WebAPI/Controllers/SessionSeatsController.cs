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
        private readonly SessionSeatService _seatService;

        public SessionSeatsController(SessionSeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int sessionId)
        {
            SessionSeatsModel model = await _seatService.GetAllByAsync(sessionId);
            SessionSeatsResponse response = model.Adapt<SessionSeatsResponse>();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SessionSeatsRequest request)
        {
            SessionSeatModel model = request.Adapt<SessionSeatModel>();
            await _seatService.CreateAsync(model);
            return Ok();
        }

        public async Task<IActionResult> Edit(int sessionId, [FromBody] SessionSeatsRequest request)
        {
            SessionSeatsModel model = request.Adapt<SessionSeatsModel>();
            await _seatService.EditAsync(sessionId, model);
            return Ok();
        }
    }
}
