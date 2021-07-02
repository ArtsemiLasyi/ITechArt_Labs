using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Requests;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("sessions/{sessionId}/seat-types/{seatTypeId}")]
    public class SeatTypePricesController : ControllerBase
    {
        private readonly SeatTypePriceService _seatTypePriceService;

        public SeatTypePricesController(SeatTypePriceService seatTypePriceService)
        {
            _seatTypePriceService = seatTypePriceService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get(int sessionId, int seatTypeId)
        {
            SeatTypePriceModel? model = await _seatTypePriceService.GetByAsync(sessionId, seatTypeId);
            if (model == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SeatTypePriceRequest request)
        {
            SeatTypePriceModel model = request.Adapt<SeatTypePriceModel>();
            await _seatTypePriceService.CreateAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int sessionId, int seatTypeId, [FromBody] SeatTypePriceRequest request)
        {
            SeatTypePriceModel model = request.Adapt<SeatTypePriceModel>();
            model.SessionId = sessionId;
            model.SeatTypeId = seatTypeId;
            await _seatTypePriceService.EditAsync(model);
            return Ok();
        }
    }
}
