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
using BusinessLogic.Services;
using Mapster;

using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UsersController> _logger;
        
        public UsersController(UserService service)
        {
            _userService = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            UserModel user = _userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new UserViewModel { Id = user.Id, Email = user.Email });
        }

        [HttpPut("{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] AuthentificationViewModel model)
        {
            AuthentificationModel authModel = model.Adapt<AuthentificationModel>();
            _userService.EditUser(authModel);
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
