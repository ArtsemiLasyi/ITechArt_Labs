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
using BusinessLogic.Interfaces;
using BusinessLogic.Services;

using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        
        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            UserModel user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Json(new UserViewModel { Id = user.Id, Email = user.Email });
        }

        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel user, string password)
        {
            UserModel userModel = new UserModel
            {
                Id = user.Id,
                Email = user.Email
            };
            _userService.EditUser(userModel, password);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            UserModel user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            _userService.DeleteUser(id);
            return Ok();
        }    
    }
}
