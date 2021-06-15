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

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/films")]
    public class FilmsController : ControllerBase
    {
        private readonly FilmService _filmService;
        private readonly PosterFileService _posterFileService;

        public FilmsController(
            FilmService filmService,
            PosterFileService fileService)
        {
            _filmService = filmService;
            _posterFileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FilmCreateRequest request)
        {
            if (request.Poster == null)
            {
                return BadRequest(new { errorText = "Poster must be not null!" });
            }
            string filename = await UploadPosterAsync(request.Poster);
            
            FilmModel model = request.Adapt<FilmModel>();
            model.PosterFileName = filename;
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
            FilmModel? entity = await _filmService.GetByAsync(id);
            if (entity != null)
            {
                Stream? stream = _posterFileService.Get(entity.PosterFileName);
                if (stream != null)
                {
                    string contentType = "image/" + Path.GetExtension(entity.PosterFileName).Trim('.');
                    return File(stream, contentType);
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Get([FromBody] PageRequest request)
        {
            IReadOnlyCollection<FilmResponse> films = _filmService
                .GetAsync(request.PageNumber, request.PageSize)
                .Adapt<IReadOnlyCollection<FilmResponse>>();
            return Ok(films);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] FilmEditRequest request)
        {
            FilmModel model = request.Adapt<FilmModel>();
            model.Id = id;

            if (request.Poster != null)
            {
                string filename = await UploadPosterAsync(request.Poster);

                FilmModel? oldModel = await _filmService.GetByAsync(id);
                if (oldModel != null)
                {
                    _posterFileService.Delete(oldModel.PosterFileName);
                }
                model.PosterFileName = filename;
            }
            await _filmService.EditAsync(model);

            return Ok();
        }

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

        private Task<string> UploadPosterAsync(IFormFile poster)
        {
            using Stream stream = poster.OpenReadStream();
            string extension = Path.GetExtension(poster.FileName);

            return _posterFileService.CreateAsync(stream, extension);
        }
    }
}
