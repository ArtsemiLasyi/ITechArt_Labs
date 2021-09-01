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
    [Route("/sessions")]
    public class SessionsController : ControllerBase
    {
        private readonly SessionService _sessionService;

        public SessionsController(SessionService sessionService)
        {
            _sessionService = sessionService;
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
            return Ok();
        }

        [AllowAnonymous]
        [Route("~/films/{filmId}/sessions")]
        public async Task<IActionResult> GetAll(int filmId)
        {
            IReadOnlyCollection<SessionModel> services = await _sessionService.GetAllByAsync(filmId);
            IReadOnlyCollection<SessionResponse> response = services.Adapt<IReadOnlyCollection<SessionResponse>>();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SessionRequest request)
        {
            SessionModel model = request.Adapt<SessionModel>();
            await _sessionService.CreateAsync(model);
            return Ok();
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
