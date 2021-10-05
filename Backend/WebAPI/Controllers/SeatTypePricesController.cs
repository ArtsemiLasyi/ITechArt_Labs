using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Requests;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("sessions/{sessionId}/seat-types")]
    public class SeatTypePricesController : ControllerBase
    {
        private readonly SeatTypePriceService _seatTypePriceService;

        public SeatTypePricesController(SeatTypePriceService seatTypePriceService)
        {
            _seatTypePriceService = seatTypePriceService;
        }

        [AllowAnonymous]
        [HttpGet("{seatTypeId}")]
        public async Task<IActionResult> Get(int sessionId, int seatTypeId)
        {
            SeatTypePriceModel? model = await _seatTypePriceService.GetByAsync(sessionId, seatTypeId);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model.Adapt<SeatTypePriceResponse>());
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int sessionId)
        {
            IReadOnlyCollection<SeatTypePriceModel> models = await _seatTypePriceService.GetAllByAsync(sessionId);
            return Ok(models.Adapt<IReadOnlyCollection<SeatTypePriceResponse>>());
        }

        [HttpPost("{seatTypeId}")]
        public async Task<IActionResult> Create([FromBody] SeatTypePriceRequest request)
        {
            SeatTypePriceModel model = request.Adapt<SeatTypePriceModel>();
            await _seatTypePriceService.CreateAsync(model);
            return Ok();
        }

        [HttpPut("{seatTypeId}")]
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
