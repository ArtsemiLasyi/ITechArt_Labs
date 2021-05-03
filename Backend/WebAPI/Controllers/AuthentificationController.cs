using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BusinessLogic.Models;
using DataAccess.Contexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/authentification")]
    public class AuthentificationController : Controller
    {
        private readonly IAuthentificationService authentificationService;
        private readonly ILogger<UsersController> _logger;

        public AuthentificationController(IAuthentificationService service)
        {
            authentificationService = service;
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> SignUp(AuthentificationViewModel user)
        {
            AuthentificationModel model = new AuthentificationModel
            {
                Email = user.Email,
                Password = user.Password
            };
            authentificationService.SignUp(model);
            return Ok();
        }

        [HttpPost("/signin")]
        public async Task<IActionResult> SignIn(AuthentificationViewModel user)
        {
            AuthentificationModel model = new AuthentificationModel
            {
                Email = user.Email,
                Password = user.Password
            };
            if (!authentificationService.SignIn(model))
            {
                return BadRequest(new { errortext = "Invalid email or password!" });
            }
            return Ok();
        }
    }
}
