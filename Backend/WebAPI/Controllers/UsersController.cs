using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using WebAPI.Requests;
using WebAPI.Responses;
using BusinessLogic.Validators;
using FluentValidation.Results;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.JsonWebTokens;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly PasswordService _passwordService;
        
        public UsersController(UserService userService, PasswordService passwordService)
        {
            _userService = userService;
            _passwordService = passwordService;
        }

        [HttpGet("current")]
        public async Task<IActionResult> Get()
        {
            int id;
            ClaimsIdentity? identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return Unauthorized();
            }
            bool converted = int.TryParse(
                identity
                    .Claims
                    .Where(c => c.Type == JwtRegisteredClaimNames.Sub)
                    .Select(c => c.Value)
                    .SingleOrDefault(),
                out id
            );
            if (!converted)
            {
                return Unauthorized();
            }

            UserModel? user = await _userService.GetByAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Adapt<UserResponse>());
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
