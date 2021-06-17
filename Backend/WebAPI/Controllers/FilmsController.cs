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
            FilmModel? entity = await _filmService.GetByAsync(id);
            if (entity?.PosterFileName != null)
            {
                Stream? stream = _posterFileService.Get(entity.PosterFileName!);
                if (stream != null)
                {
                    string contentType;
                    FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(entity.PosterFileName, out contentType))
                    {
                        contentType = MediaTypeNames.Application.Octet;
                    }
                    return File(stream, contentType);
                }
            }      
            return NotFound();
        }

        [HttpPost("{id}/poster")]
        public async Task<IActionResult> UploadPoster(IFormFile formFile)
        {
            using Stream stream = formFile.OpenReadStream();
            string extension = MimeTypeMap.GetExtension(formFile.ContentType);

            string fileName = await _posterFileService.CreateAsync(stream, extension);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageRequest request)
        {
            IReadOnlyCollection<FilmModel> films = await _filmService.GetAsync(request.PageNumber, request.PageSize);
            IReadOnlyCollection<FilmResponse> result = new List<FilmModel>(films).Adapt<List<FilmResponse>>();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] FilmRequest request)
        {
            FilmModel model = request.Adapt<FilmModel>();
            model.Id = id;

            FilmModel? oldModel = await _filmService.GetByAsync(id);
            if (oldModel?.PosterFileName != model.PosterFileName)
            {
                if (oldModel != null)
                {
                    _posterFileService.Delete(oldModel.PosterFileName!);
                }
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
    }
}
