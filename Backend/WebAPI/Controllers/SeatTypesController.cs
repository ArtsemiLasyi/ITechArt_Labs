using BusinessLogic.Models;
using BusinessLogic.Services;
using BusinessLogic.Statuses;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Requests;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/seat-types")]
    public class SeatTypesController : ControllerBase
    {
        private readonly SeatTypeService _seatTypeService;

        public SeatTypesController(SeatTypeService seatTypeService)
        {
            _seatTypeService = seatTypeService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SeatTypeRequest request)
        {
            SeatTypeModel model = request.Adapt<SeatTypeModel>();
            await _seatTypeService.CreateAsync(model);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyCollection<SeatTypeModel> seatTypes = await _seatTypeService.GetAllAsync();
            IReadOnlyCollection<SeatTypeResponse> response = seatTypes.Adapt<IReadOnlyCollection<SeatTypeResponse>>();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SeatTypeRequest request)
        {
            SeatTypeModel model = request.Adapt<SeatTypeModel>();
            model.Id = id;
            await _seatTypeService.EditAsync(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            SeatTypeDeletionStatus status = await _seatTypeService.DeleteByAsync(id);
            if (status == SeatTypeDeletionStatus.NotFound)
            {
                return NotFound();
            }
            if (status == SeatTypeDeletionStatus.ForbiddenAsUsed)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
