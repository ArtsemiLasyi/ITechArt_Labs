using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public IActionResult Get(int id)
        {
            UserModel? user = _userService.GetBy(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.Adapt<UserResponse>());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] UserEditRequest request)
        {
            UserModel model = request.Adapt<UserModel>();
            bool isPasswordWasChanged = false;

            if (request.Password != null)
            {
                PasswordValidator passwordValidator = new();
                ValidationResult passwordResults = passwordValidator.Validate(request.Password);
                if (!passwordResults.IsValid)
                {
                    return BadRequest();
                }
                isPasswordWasChanged = true;
            }

            UserValidator userValidator = new();
            ValidationResult results = userValidator.Validate(model);
            if (!results.IsValid)
            {
                return BadRequest();
            }
            await _userService.EditAsync(model);
            if (isPasswordWasChanged)
            {
                await _passwordService.UpdatePasswordAsync(model.Id, request.Password);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isSuccessful = await _userService.DeleteByAsync(id);
            if (!isSuccessful)
            {
                return NotFound();
            }
            return Ok();
        }    
    }
}
