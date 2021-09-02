

using BusinessLogic.Models;
using BusinessLogic.Parameters;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Parameters;
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
        public async Task<IActionResult> GetAll([FromQuery] CityRequestSearchParameters parameters)
        {
            CityModelSearchParameters searchParameters = parameters.Adapt<CityModelSearchParameters>();
            IReadOnlyCollection<CityModel> cities = await _cityService.GetAsync(searchParameters);
            IReadOnlyCollection<CityResponse> response = cities.Adapt<IReadOnlyCollection<CityResponse>>();
            return Ok(response);
        }
    }
}
