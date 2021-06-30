using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Constants;
using WebAPI.Requests;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [Authorize(Policy = PolicyNames.Administrator)]
    [ApiController]
    [Route("halls/{hallId}/seats")]
    public class SeatsController : ControllerBase
    {
        private readonly SeatService _seatService;

        public SeatsController(SeatService seatService)
        {
            _seatService = seatService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SeatsRequest request)
        {
            SeatModel model = request.Adapt<SeatModel>();
            await _seatService.CreateAsync(model);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(int hallId)
        {
            SeatsModel model = await _seatService.GetAllByAsync(hallId);
            SeatsResponse response = model.Adapt<SeatsResponse>();
            return Ok(response);
        }

 
        public async Task<IActionResult> Edit(int hallId, [FromBody] SeatsRequest request)
        {
            SeatsModel model = request.Adapt<SeatsModel>();
            await _seatService.EditAsync(hallId, model);
            return Ok();
        }
    }
}
