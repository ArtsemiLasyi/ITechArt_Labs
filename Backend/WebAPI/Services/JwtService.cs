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
        private readonly JwtOptions _jwtOptions;

        public JwtService(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string GetJwToken(UserModel userInfo)
        {
            JwtSecurityToken jwtSecurityToken = GenerateJwtToken(userInfo);
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private JwtSecurityToken GenerateJwtToken(UserModel userInfo)
        {
   
            SymmetricSecurityKey securityKey = new (
                Encoding.Unicode.GetBytes(_jwtOptions.Key)
            );
            SigningCredentials credentials = new (
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            Claim[] claims = new []
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

            JwtSecurityToken token = new (
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                expires: DateTime.UtcNow.Add(
                    TimeSpan.Parse(_jwtOptions.Lifetime)
                ),
                signingCredentials: credentials
            );
            return token;
        }
    }
}
