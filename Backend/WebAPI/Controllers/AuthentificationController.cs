using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models;
using WebAPI.Requests;
using Mapster;
using BusinessLogic.Services;
using WebAPI.Services;
using System.Threading.Tasks;
using WebAPI.Responses;

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

            bool successful = await _signUpService.SignUpAsync(model);
            if (!successful)
            {
                return BadRequest(new { errorText = "User already exists!" });
            }

            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            SignInModel model = request.Adapt<SignInModel>();
            UserModel? user = await _signInService.SignInAsync(model);
            if (user == null)
            {
                return Unauthorized(new { errorText = "Invalid email or password!" });
            }

            string token = _jwtService.GetJwToken(user);
            return Ok(new TokenResponse(token));
        }
    }
}
