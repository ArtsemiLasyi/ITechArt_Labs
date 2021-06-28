

using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly CityService _cityService;

        public CitiesController(CityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyCollection<CityModel> cities = await _cityService.GetAllAsync();
            IReadOnlyCollection<CityResponse> response = cities.Adapt<IReadOnlyCollection<CityResponse>>();
            return Ok(response);
        }
    }
}
