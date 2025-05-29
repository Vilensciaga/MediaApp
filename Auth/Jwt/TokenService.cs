using MediaApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

//using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Jwt
{
    public class TokenService : ITokenService
    {

        private readonly SymmetricSecurityKey key;

        public TokenService(IConfiguration config)
        {
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }


        public async Task<string> Authenticate(AppUser existingUser, LoginDto user)
        {
            using var hmac = new HMACSHA512(existingUser.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != existingUser.PasswordHash[i])
                {
                    Console.WriteLine("We failed here");
                    return null;
                }

            }

            return await CreateToken(existingUser);

        }
        public async Task<string> CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)

            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = creds,
            };

            

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
