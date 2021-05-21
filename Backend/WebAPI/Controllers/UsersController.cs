using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using WebAPI.Requests;
using WebAPI.Responses;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UsersController> _logger;
        
        public UsersController(UserService service)
        {
            _userService = service;
        }

        [HttpGet("{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult Get(int id)
        {
            UserModel? user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new UserResponse { Id = user.Id, Email = user.Email });
        }

        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id,[FromForm] UserRequest request)
        {
            UserModel? user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.EditUser(user, request.Password);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            UserModel? user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteUser(id);
            return Ok();
        }    
    }
}
