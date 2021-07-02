using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using WebAPI.Requests;
using WebAPI.Responses;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Constants;
using System.Security.Claims;
using WebAPI.Extensions;

namespace WebAPI.Controllers
{
    [Authorize(Policy = PolicyNames.Authorized)]
    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PasswordService _passwordService;

        public UsersController(
            UserService userService,
            PasswordService passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            UserModel? user = await _userService.GetByAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Adapt<UserResponse>());
        }

        [HttpGet("current")]
        public async Task<IActionResult> Get()
        {
            int? id = HttpContext.User.Identity.GetUserId();
            if (id == null)
            {
                return Unauthorized();
            }

            UserModel? user = await _userService.GetByAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Adapt<UserResponse>());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UserEditRequest request)
        {
            UserModel model = request.Adapt<UserModel>();
            model.Id = id;

            await _userService.EditAsync(model);
            if (request.Password != null)
            {
                await _passwordService.UpdateAsync(model.Id, request.Password);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool successful = await _userService.DeleteByAsync(id);
            if (!successful)
            {
                return NotFound();
            }
            return Ok();
        }    
    }
}
