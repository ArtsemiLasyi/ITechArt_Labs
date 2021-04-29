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
    public class UsersController : Controller
    {
        private IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        
        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            UserDTO user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Json(new UserViewModel { Id = user.Id, Email = user.Email });
        }

        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserViewModel user)
        {
            UserDTO userDto = new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password
            };
            _userService.EditUser(userDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            UserDTO user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            _userService.DeleteUser(id);
            return Ok();
        }

       
    }
}
