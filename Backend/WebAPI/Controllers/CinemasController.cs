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
using WebAPI.Constants;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Authorize(Policy = PolicyNames.Administrator)]
    [ApiController]
    [Route("/cinemas")]
    public class CinemasController : ControllerBase
    {
        private readonly CinemaService _cinemaService;
        private readonly CinemaPhotoService _cinemaPhotoService;

        public CinemasController(
            CinemaService cinemaService,
            CinemaPhotoService cinemaPhotoService)
        {
            _cinemaService = cinemaService;
            _cinemaPhotoService = cinemaPhotoService;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            CinemaModel? cinema = await _cinemaService.GetByAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }

            return Ok(cinema.Adapt<CinemaResponse>());
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllBy([FromQuery] int cityId)
        {
            IReadOnlyCollection<CinemaModel> cinemas = await _cinemaService.GetAllByAsync(cityId);
            IReadOnlyCollection<CinemaResponse> response = cinemas.Adapt<IReadOnlyCollection<CinemaResponse>>();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}/photo")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            PosterModel? model = await _cinemaPhotoService.GetAsync(id);
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CinemaRequest request)
        {
            CinemaModel model = request.Adapt<CinemaModel>();
            await _cinemaService.CreateAsync(model);

            return Ok();
        }

        [HttpPost("{id}/photo")]
        public async Task<IActionResult> UploadPhoto(int id, IFormFile formFile)
        {
            CinemaModel? model = await _cinemaService.GetByAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            using Stream stream = formFile.OpenReadStream();
            string extension = MimeTypeMap.GetExtension(formFile.ContentType);

            await _cinemaPhotoService.UploadAsync(id, stream, extension);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CinemaRequest request)
        {
            CinemaModel model = request.Adapt<CinemaModel>();
            model.Id = id;
            await _cinemaService.EditAsync(model);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool successful = await _cinemaService.DeleteByAsync(id);
            if (!successful)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
