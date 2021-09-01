using BusinessLogic.Models;
using BusinessLogic.Services;
using BusinessLogic.Statuses;
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
    [Route("/currencies")]
    public class CurrenciesController : ControllerBase
    {
        private readonly CurrencyService _currencyService;

        public CurrenciesController(CurrencyService cityService)
        {
            _currencyService = cityService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyCollection<CurrencyModel> cities = await _currencyService.GetAsync();
            IReadOnlyCollection<CurrencyResponse> response = cities.Adapt<IReadOnlyCollection<CurrencyResponse>>();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            CurrencyModel? model = await _currencyService.GetByAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            CurrencyResponse response = model.Adapt<CurrencyResponse>();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CurrencyRequest request)
        {
            CurrencyModel model = request.Adapt<CurrencyModel>();
            model.Id = id;
            await _currencyService.EditAsync(model);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CurrencyRequest request)
        {
            CurrencyModel model = request.Adapt<CurrencyModel>();
            await _currencyService.CreateAsync(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            CurrencyDeletionStatus status = await _currencyService.DeleteByAsync(id);
            if (status == CurrencyDeletionStatus.NotFound)
            {
                return NotFound();
            }
            if (status == CurrencyDeletionStatus.ForbiddenAsUsed)
            {
                return BadRequest(new { errorText = "Currency is already in use!" });
            }
            return Ok();
        }
    }
}
