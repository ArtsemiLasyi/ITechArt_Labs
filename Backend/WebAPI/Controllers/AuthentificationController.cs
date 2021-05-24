using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BusinessLogic.Models;
using WebAPI.Requests;
using Mapster;
using BusinessLogic.Services;
using WebAPI.Responses;
using BusinessLogic.Results;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/account")]
    public class AuthentificationController : ControllerBase
    {
        private readonly SignInService _signInService;
        private readonly SignUpService _signUpService;
        private readonly JwtService _jwtService;
        private readonly ILogger<UsersController> _logger;

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
        public IActionResult SignUp([FromForm] SignUpRequest request)
        {
            SignUpModel model = request.Adapt<SignUpModel>();
            if (!_signUpService.SignUpAsync(model).Result)
            {
                return BadRequest(new { errortext = "User is already exists!" });
            }
            return Ok();
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromForm] SignInRequest request)
        {
            SignInModel model = request.Adapt<SignInModel>();
            BusinessLogic.Results.SignInResult result = _signInService.SignIn(model);
            if (!result.IsSuccessful)
            {
                return Unauthorized(new { errortext = "Invalid email or password!" });
            }
            if (result.User == null)
            {
                return NotFound();
            }
            string? token = _jwtService.GetJwToken(result.User);
            return Ok(new SignInResponse { Token = token });
        }
    }
}


