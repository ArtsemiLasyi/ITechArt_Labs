using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Responses;
using WebAPI.Requests;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/films")]
    public class FilmsController : ControllerBase
    {
        private readonly FilmService _filmService;
        private readonly FileService _fileService;
        private readonly IConfiguration _configuration;

        public FilmsController(
            FilmService filmService,
            FileService fileService,
            IConfiguration configuration
            )
        {
            _filmService = filmService;
            _fileService = fileService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FilmRequest request)
        {
            if (request.Poster == null)
            {
                return BadRequest(new { errortext = "Poster must be not null!" });
            }
            string filename = await UploadPoster(request.Poster);
            
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

        [HttpGet]
        public IActionResult Get([FromBody] PageRequest request)
        {
            Options.FileOptions options = new Options.FileOptions();
            _configuration.GetSection(Options.FileOptions.Files).Bind(options);
            string path = Path.Combine(options.Path, options.Films);

            FilmResponse[] films = _filmService
                                           .Get(request.PageNumber, request.PageSize)
                                           .ToArray()
                                           .Adapt<FilmResponse[]>();
            return Ok(films);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] FilmRequest request)
        {
            FilmModel model = request.Adapt<FilmModel>();

            if (request.Poster != null)
            {
                string filename = await UploadPoster(request.Poster);
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

        private Task<string> UploadPoster(IFormFile poster)
        {
            Stream stream = poster.OpenReadStream();
            string extension = Path.GetExtension(poster.FileName);

            Options.FileOptions options = new Options.FileOptions();
            _configuration
                .GetSection(Options.FileOptions.Files)
                .Bind(options);

            string path = Path.Combine(options.Path, options.Films);
            return _fileService.CreateAsync(stream, path, extension);
        }
    }
}
