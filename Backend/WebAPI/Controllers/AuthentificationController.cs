using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BusinessLogic.Models;
using DataAccess.Contexts;
using WebAPI.Models;
using Mapster;
using BusinessLogic.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/account")]
    public class AuthentificationController : ControllerBase
    {
        private readonly AuthentificationService authentificationService;
        private readonly ILogger<UsersController> _logger;

        public AuthentificationController(AuthentificationService service)
        {
            authentificationService = service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromForm] AuthentificationViewModel user)
        { 
            AuthentificationModel model = user.Adapt<AuthentificationModel>();
            if (!authentificationService.SignUp(model).Result)
            {
                return BadRequest(new { errortext = "User is already exists!" });
            }
            return Ok();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromForm] AuthentificationViewModel user)
        {
            AuthentificationModel model = user.Adapt<AuthentificationModel>();
            if (!authentificationService.SignIn(model))
            {
                return BadRequest(new { errortext = "Invalid email or password!" });
            }
            return Ok();
        }
    }
}
