using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusinessLogic.Models;
using WebAPI.Requests;
using Mapster;
using BusinessLogic.Services;
using WebAPI.Services;
using System.Threading.Tasks;
using BusinessLogic.Validators;
using FluentValidation.Results;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/account")]
    public class AuthentificationController : ControllerBase
    {
        private readonly SignInService _signInService;
        private readonly SignUpService _signUpService;
        private readonly JwtService _jwtService;

        public AuthentificationController(
            SignInService signin,
            SignUpService signup,
            JwtService jwtService)
        {
            _signInService = signin;
            _signUpService = signup;
            _jwtService = jwtService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            SignUpModel model = request.Adapt<SignUpModel>();

            bool isSuccessful = await _signUpService.SignUpAsync(model);
            if (!isSuccessful)
            {
                return BadRequest(new { errortext = "User is already exists!" });
            }

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            SignInModel model = request.Adapt<SignInModel>();
            UserModel? user = await _signInService.SignIn(model);
            if (user == null)
            {
                return Unauthorized(new { errortext = "Invalid email or password!" });
            }

            string? token = _jwtService.GetJwToken(user);
            return Ok(token);
        }
    }
}