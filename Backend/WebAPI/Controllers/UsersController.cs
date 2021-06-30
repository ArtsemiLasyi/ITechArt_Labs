using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using WebAPI.Requests;
using WebAPI.Responses;
using BusinessLogic.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Constants;
using System.Security.Claims;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PasswordService _passwordService;
        private readonly IdentityService _identityService;

        public UsersController(
            UserService userService,
            PasswordService passwordService,
            IdentityService identityService)
        {
            _userService = userService;
            _passwordService = passwordService;
            _identityService = identityService;
        }

        [Authorize(Policy = PolicyNames.Authorized)]
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

        [Authorize(Policy = PolicyNames.Authorized)]
        [HttpGet("current")]
        public async Task<IActionResult> Get()
        {
            int? id = _identityService.GetUserId(HttpContext.User.Identity as ClaimsIdentity);
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

        [Authorize(Policy = PolicyNames.Authorized)]
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

        [Authorize(Policy = PolicyNames.Authorized)]
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
