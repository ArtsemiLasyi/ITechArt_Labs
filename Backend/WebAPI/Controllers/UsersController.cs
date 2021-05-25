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
        private readonly PasswordService _passwordService;
        private readonly ILogger<UsersController> _logger;
        
        public UsersController(UserService userService, PasswordService passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
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
            return Ok(user.Adapt<UserResponse>());
        }

        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] UserRequest request)
        {
            UserModel? user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            if (request.Password != null)
            {
                await _passwordService.UpdatePassword(id, request.Password);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            UserModel? user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userService.DeleteUserAsync(id);
            return Ok();
        }    
    }
}
