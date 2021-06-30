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
    [Route("cinemas/{cinemaId}/services")]
    public class CinemaServicesController : ControllerBase
    {
        private readonly CinemaServiceService _cinemaServiceService;

        public CinemaServicesController(CinemaServiceService cinemaServiceService)
        {
            _cinemaServiceService = cinemaServiceService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CinemaServiceRequest request)
        {
            CinemaServiceModel model = request.Adapt<CinemaServiceModel>();
            await _cinemaServiceService.CreateAsync(model);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, int cinemaId)
        {
            CinemaServiceModel? model = await _cinemaServiceService.GetByAsync(id, cinemaId);
            if (model == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(int cinemaId)
        {
            IReadOnlyCollection<CinemaServiceModel> services = await _cinemaServiceService.GetAllByAsync(cinemaId);
            IReadOnlyCollection<CinemaServiceResponse> response = services.Adapt<IReadOnlyCollection<CinemaServiceResponse>>();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, int cinemaId, [FromBody] CinemaServiceRequest request)
        {
            CinemaServiceModel model = request.Adapt<CinemaServiceModel>();
            model.ServiceId = id;
            model.CinemaId = cinemaId;
            await _cinemaServiceService.EditAsync(model);
            return Ok();
        }
    }
}
