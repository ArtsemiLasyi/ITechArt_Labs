using BusinessLogic.Models;
using BusinessLogic.Parameters;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Constants;
using WebAPI.Parameters;
using WebAPI.Requests;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [Authorize(Policy = PolicyNames.Administrator)]
    [ApiController]
    [Route("/sessions")]
    public class SessionsController : ControllerBase
    {
        private readonly SessionService _sessionService;
        private readonly SeatTypePriceService _seatTypePriceService;

        public SessionsController(SessionService sessionService, SeatTypePriceService seatTypePriceService)
        {
            _sessionService = sessionService;
            _seatTypePriceService = seatTypePriceService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            SessionModel? model = await _sessionService.GetByAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model.Adapt<SessionResponse>());
        }

        [AllowAnonymous]
        [HttpGet("~/cinemas/{cinemaId}/sessions")]
        public async Task<IActionResult> GetAll(int cinemaId, [FromQuery] SessionRequestSearchParameters parameters)
        {
            IReadOnlyCollection<SessionModel> services = await _sessionService.GetAllByAsync(
                cinemaId, 
                parameters.Adapt<SessionModelSearchParameters>()
            );
            IReadOnlyCollection<SessionResponse> response = services.Adapt<IReadOnlyCollection<SessionResponse>>();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SessionRequest request)
        {
            SessionModel model = request.Adapt<SessionModel>();
            int id = await _sessionService.CreateAsync(model);

            List<SeatTypePriceModel> seatTypePrices = request.SeatTypePrices
                .Adapt<List<SeatTypePriceModel>>();
            seatTypePrices.ForEach(seatTypePrice => seatTypePrice.SessionId = id);
            await _seatTypePriceService.CreateAsync(seatTypePrices.AsReadOnly());

            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SessionRequest request)
        {
            SessionModel model = request.Adapt<SessionModel>();
            model.Id = id;
            await _sessionService.EditAsync(model);
            return Ok();
        }
    }
}
