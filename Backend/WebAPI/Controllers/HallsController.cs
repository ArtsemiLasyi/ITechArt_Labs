using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MimeTypes;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using WebAPI.Requests;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/halls")]
    public class HallsController : ControllerBase
    {
        private readonly HallService _hallService;
        private readonly HallPhotoService _hallPhotoService;

        public HallsController(
            HallService hallService,
            HallPhotoService hallPhotoService)
        {
            _hallService = hallService;
            _hallPhotoService = hallPhotoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HallRequest request)
        {
            HallModel model = request.Adapt<HallModel>();
            await _hallService.CreateAsync(model);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            HallModel? hall = await _hallService.GetByAsync(id);
            if (hall == null)
            {
                return NotFound();
            }

            return Ok(hall.Adapt<HallResponse>());
        }

        [HttpGet("{id}/photo")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            PosterModel? model = await _hallPhotoService.GetAsync(id);
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

        [HttpPost("{id}/photo")]
        public async Task<IActionResult> UploadPhoto(int id, IFormFile formFile)
        {
            HallModel? model = await _hallService.GetByAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            using Stream stream = formFile.OpenReadStream();
            string extension = MimeTypeMap.GetExtension(formFile.ContentType);

            await _hallPhotoService.UploadAsync(id, stream, extension);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBy([FromQuery] int cinemaId)
        {
            IReadOnlyCollection<HallModel> halls = await _hallService.GetAllByAsync(cinemaId);
            IReadOnlyCollection<HallResponse> response = halls.Adapt<IReadOnlyCollection<HallResponse>>();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] HallRequest request)
        {
            HallModel model = request.Adapt<HallModel>();
            model.Id = id;
            await _hallService.EditAsync(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool successful = await _hallService.DeleteByAsync(id);
            if (!successful)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
