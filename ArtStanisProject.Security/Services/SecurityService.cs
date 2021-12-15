using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using ArtStanisProject.Security.IServices;
using ArtStanisProject.Security.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ArtStanisProject.Security.Services
{

    public class SecurityService : ISecurityService
    {
        public IConfiguration Configuration { get; }

        public IAuthUserService _service;

        public SecurityService(IConfiguration configuration, IAuthUserService service)
        {
            Configuration = configuration;
            _service = service;
        }

        public JwtToken GenerateJwtToken(string username, string password)
        {
            var user = _service.FindUser(username);
            if (Authenticate(user, password))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                    Configuration["Jwt:Audience"],
                    null,
                    expires: DateTime.Now.AddMinutes(90),
                    signingCredentials: credentials);
                return new JwtToken
                {
                    Jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "Very nice!"
                };
            }

            throw new AuthenticationException("Incorrect Username or Password");
        }

        private bool Authenticate(AuthUser user, string plainTextPassword)
        {
            if (user == null || user.HashedPassword.Length <= 0 || user.Salt.Length <= 0)
                return false;

            var hashedPasswordFromPlain = HashedPassword(plainTextPassword, user.Salt);
            return user.HashedPassword.Equals(hashedPasswordFromPlain);
        }

        public string HashedPassword(string plainTextPassword, byte[] userSalt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainTextPassword,
                salt: userSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}