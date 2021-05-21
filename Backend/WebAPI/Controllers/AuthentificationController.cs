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

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/account")]
    public class AuthentificationController : ControllerBase
    {
        private readonly SignInService _signInService;
        private readonly SignUpService _signUpService;
        private readonly ILogger<UsersController> _logger;

        public AuthentificationController(
            SignInService signin,
            SignUpService signup)
        {
            _signInService = signin;
            _signUpService = signup;
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromForm] SignUpRequest request)
        { 
            SignUpModel model = request.Adapt<SignUpModel>();
            if (!_signUpService.SignUp(model).Result)
            {
                return Unauthorized(new { errortext = "User is already exists!" });
            }
            return Ok();
        }

        [HttpPost("signin")]
        public IActionResult SignIn([FromForm] SignInRequest request)
        {
            SignInModel model = request.Adapt<SignInModel>();
            (bool, string) tuple = _signInService.SignIn(model);
            if (!tuple.Item1)
            {
                return Unauthorized(new { errortext = "Invalid email or password!" });
            }
            Response.Cookies.Append(
                "token",
                tuple.Item2,
                new Microsoft.AspNetCore.Http.CookieOptions 
                { 
                    HttpOnly = true 
                }
            );
            return Ok();
        }
    }
}


