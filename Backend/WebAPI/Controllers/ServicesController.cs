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
    [Route("/services")]
    public class ServicesController : ControllerBase
    {
        private readonly ServiceService _serviceService;

        public ServicesController(ServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceModel? service = await _serviceService.GetByAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service.Adapt<ServiceResponse>());
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IReadOnlyCollection<ServiceModel> services = await _serviceService.GetAsync();
            IReadOnlyCollection<ServiceResponse> response = services.Adapt<IReadOnlyCollection<ServiceResponse>>();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceRequest request)
        {
            ServiceModel model = request.Adapt<ServiceModel>();
            await _serviceService.CreateAsync(model);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ServiceRequest request)
        {
            ServiceModel model = request.Adapt<ServiceModel>();
            model.Id = id;
            await _serviceService.EditAsync(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceDeletionStatus status = await _serviceService.DeleteByAsync(id);
            if (status == ServiceDeletionStatus.NotFound)
            {
                return NotFound();
            }
            if (status == ServiceDeletionStatus.ForbiddenAsUsed)
            {
                return BadRequest(new { errorText = "Service is already in use!" });
            }
            return Ok();
        }
    }
}
