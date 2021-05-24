using BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetJwToken(UserModel userInfo)
        {
            JwtSecurityToken jwtSecurityToken = GenerateJwtToken(userInfo);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }



        private JwtSecurityToken GenerateJwtToken(UserModel userInfo)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.Unicode.GetBytes(_configuration["JwToken:Key"])
            );
            SigningCredentials credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            Claim[] claims = new Claim[]
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    userInfo.Id.ToString()
                ),
                new Claim(
                    "email",
                    userInfo.Email
                )
            };

            JwtSecurityToken? token = new JwtSecurityToken(
                _configuration["JwToken:Issuer"],
                _configuration["JwToken:Audience"],
                claims,
                expires: DateTime.UtcNow.Add(
                    TimeSpan.Parse(
                        _configuration.GetSection("JwToken:LifetimeMinutes").Value          // Will be optimized
                    )
                ),
                signingCredentials: credentials
            );
            return token;
        }
    }
}
