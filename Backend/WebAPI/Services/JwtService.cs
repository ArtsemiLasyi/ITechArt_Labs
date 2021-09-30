using BusinessLogic.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Options;

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
            JwtOptions jwtOptions = new JwtOptions();
            _configuration.GetSection(JwtOptions.JwToken).Bind(jwtOptions);

            SymmetricSecurityKey securityKey = new (
                Encoding.ASCII.GetBytes(jwtOptions.Key)
            );
            SigningCredentials credentials = new (
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            Claim[] claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    userInfo.Id.ToString()
                ),
                new Claim(
                    ClaimTypes.Email,
                    userInfo.Email
                ),
                new Claim(
                    ClaimTypes.Role,
                    ((int)userInfo.Role).ToString()
                )
            };

            JwtSecurityToken token = new (
                jwtOptions.Issuer,
                jwtOptions.Audience,
                claims,
                expires: DateTime.UtcNow.Add(
                    jwtOptions.Lifetime
                ),
                signingCredentials: credentials
            );
            return token;
        }
    }
}