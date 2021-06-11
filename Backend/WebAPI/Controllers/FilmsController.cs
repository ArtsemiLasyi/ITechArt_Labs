using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Responses;
using WebAPI.Requests;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

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
        public async Task<IActionResult> Create([FromBody] FilmRequest request)
        {
            // [FromForm] IFormFile file
            IFormFile file;

            Stream stream = file.OpenReadStream();
            string extension = Path.GetExtension(file.FileName);

            Options.FileOptions options = new Options.FileOptions();
            _configuration.GetSection(Options.FileOptions.Files).Bind(options);

            string path = Path.Combine(options.Path, options.Films);
            string filename = await _fileService.CreateAsync(stream, path, extension);
            
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

            List<FilmResponse> films = _filmService
                                           .Get(request.PageNumber, request.PageSize)
                                           .ToList()
                                           .Adapt<List<FilmResponse>>();
       
            films.ForEach(film => film.Poster = _fileService.Get(path, film.PosterFileName));
            return Ok(films);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] FilmRequest request)
        {
            FilmModel model = request.Adapt<FilmModel>();

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
    }
}
