using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models;
using BusinessLogic.Services;
using Mapster;
using WebAPI.Requests;
using WebAPI.Responses;
using BusinessLogic.Validators;
using FluentValidation.Results;

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

            await _userService.EditAsync(model);
            await _passwordService.UpdateAsync(model.Id, request.Password);

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
