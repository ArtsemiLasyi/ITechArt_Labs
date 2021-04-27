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
using WebAPI.Contexts;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("/users")]
    public class UsersController : Controller
    {
        private readonly UsersContext _context;

        private readonly ILogger<UsersController> _logger;

        public UsersController(UsersContext context)
        {
            _context = context;
        }

        [HttpGet("{*id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Entities.UserEntity? user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost("/register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                Entities.UserEntity? userEntity = await GetUserByEmail(user.Email);
                if (userEntity != null)
                {
                    return BadRequest(new { errortext = "User with this email is already exists!" });
                }

                SHA256 mySHA256 = SHA256.Create();
                string salt = Encoding.Unicode.GetString(GenerateSalt());
                byte[] bytePassword = Encoding.ASCII.GetBytes(user.Password + salt);
                byte[] hashValue = mySHA256.ComputeHash(bytePassword);

                userEntity = new Entities.UserEntity
                {
                    Email = user.Email,
                    PasswordHash = hashValue,
                    Salt = salt,
                    RoleId = (int) Models.UserRole.CommonUser
                };
                _context.Add(userEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpPost("/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                Entities.UserEntity? userEntity = await GetUserByEmail(user.Email);
                if (userEntity == null)
                {
                    return NotFound(new { errortext = "No user with this email!"});
                }
                SHA256 mySHA256 = SHA256.Create();
                string salt = userEntity.Salt;
                byte[] bytePassword = Encoding.ASCII.GetBytes(user.Password + salt);
                byte[] hashValue = mySHA256.ComputeHash(bytePassword);

                if (!CompareHashes(hashValue, userEntity.PasswordHash))
                {
                    return BadRequest(new { errortext = "Invalid email or password!"});
                }

                // Create jwt-token

                var response = new
                {
                    // To do    
                };

                return Json(response);

            }
            return View(user);
        }

        [HttpPost("{*id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Email,Password")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpPost("{*id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompareHashes(byte[] first, byte[] second)
        {
            bool bEqual = false;
            if (first.Length == second.Length)
            {
                int i = 0;
                bool compareCondition = (i < first.Length) && (first[i] == second[i]);
                while (compareCondition)
                {
                    i += 1;
                }
                if (i == second.Length)
                {
                    bEqual = true;
                }
            }
            return bEqual;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private async Task<Entities.UserEntity> GetUserByEmail(string email)
        {
            return await _context.Users
                    .FirstOrDefaultAsync(m => m.Email == email);
        }

        private static byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider rncCsp = new RNGCryptoServiceProvider();
            byte[] salt = new byte[20];
            rncCsp.GetBytes(salt);

            return salt;
        }
    }
}
