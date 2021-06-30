using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Responses;
using WebAPI.Requests;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.StaticFiles;
using MimeTypes;
using System.Net.Mime;
using System.Collections.ObjectModel;
using WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/films")]
    public class FilmsController : ControllerBase
    {
        private readonly FilmService _filmService;
        private readonly PosterService _posterFileService;

        public FilmsController(
            FilmService filmService,
            PosterService fileService)
        {
            _filmService = filmService;
            _posterFileService = fileService;
        }

        [Authorize(Policy = PolicyNames.Administrator)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FilmRequest request)
        {
            FilmModel model = request.Adapt<FilmModel>();
            await _filmService.CreateAsync(model);

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            FilmModel? film = await _filmService.GetByAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            return Ok(film.Adapt<FilmResponse>());
        }

        [HttpGet("{id}/poster")]
        public async Task<IActionResult> GetPoster(int id)
        {
            PosterModel? model = await _posterFileService.GetAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            string contentType;
            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(model.FileName, out contentType))
            {
                contentType = MediaTypeNames.Application.Octet;
            }
            return File(model.FileStream, contentType);
        }

        [Authorize(Policy = PolicyNames.Administrator)]
        [HttpPost("{id}/poster")]
        public async Task<IActionResult> UploadPoster(int id, IFormFile formFile)
        {
            FilmModel? model = await _filmService.GetByAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            using Stream stream = formFile.OpenReadStream();
            string extension = MimeTypeMap.GetExtension(formFile.ContentType);

            await _posterFileService.UploadAsync(id, stream, extension);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageRequest request)
        {
            IReadOnlyCollection<FilmModel> films = await _filmService.GetAsync(request.PageNumber, request.PageSize);
            IReadOnlyCollection<FilmResponse> response = films.Adapt<IReadOnlyCollection<FilmResponse>>();
            return Ok(response);
        }

        [Authorize(Policy = PolicyNames.Administrator)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] FilmRequest request)
        {
            FilmModel model = request.Adapt<FilmModel>();
            model.Id = id;
            await _filmService.EditAsync(model);

            return Ok();
        }

        [Authorize(Policy = PolicyNames.Administrator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool successful = await _filmService.DeleteByAsync(id);
            if (!successful)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
