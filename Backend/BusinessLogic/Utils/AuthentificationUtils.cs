using BusinessLogic.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utils
{
    public static class AuthentificationUtils
    {
        public static readonly int SALT_LENGTH = 20;

        public static byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider rncCsp = new RNGCryptoServiceProvider();
            byte[] salt = new byte[20];
            rncCsp.GetBytes(salt);
            rncCsp.Dispose();
            return salt;
        }

        public static byte[] ComputeHash(string password, byte[] salt)
        {
            SHA256 mySHA256 = SHA256.Create();
            byte[] passwordByte = Encoding.ASCII.GetBytes(password);
            byte[] passwordWithSalt = passwordByte.Concat(salt).ToArray();
            byte[] hash = mySHA256.ComputeHash(passwordWithSalt);
            mySHA256.Dispose();
            return hash;
        }

        public static JwtSecurityToken GenerateJwtToken(UserModel userInfo, IConfiguration configuration)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.Unicode.GetBytes(configuration["JwToken:Key"])
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
                configuration["JwToken:Issuer"],
                configuration["JwToken:Audience"],
                claims,
                expires: DateTime.UtcNow.Add(
                    TimeSpan.Parse(
                        configuration.GetSection("JwToken:LifetimeMinutes").Value
                    )
                ),
                signingCredentials: credentials
            );
            return token;
        }
    }
}
