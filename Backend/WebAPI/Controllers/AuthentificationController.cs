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
using WebAPI.BBL.DTOs;
using WebAPI.BBL.Interfaces;
using WebAPI.DAL.Contexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/users")]
    public class AuthentificationController : Controller
    {
        private IUserService userService;
        private readonly ILogger<UsersController> _logger;

        public AuthentificationController(IUserService service)
        {
            userService = service;
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> SignUp(UserViewModel user)
        {
            UserDTO userDto = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password
            };
            userService.SignUpUser(userDto);
            return Ok();
        }

        [HttpPost("/signin")]
        public async Task<IActionResult> SignIn(UserViewModel user)
        {
            UserDTO userDto = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password
            };
            if (!userService.SignInUser(userDto))
            {
                return BadRequest(new { errortext = "Invalid email or password!" });
            }
            return Ok();
        }
    }
}
