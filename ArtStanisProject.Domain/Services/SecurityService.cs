using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Text;
using ArtStanisProject.Core.IServices;
using ArtStanisProject.Core.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ArtStanisProject.Domain.Services;

public class SecurityService : ISecurityService
{
    public IAuthUserService _service;

    public SecurityService(IConfiguration configuration, IAuthUserService service)
    {
        Configuration = configuration;
        _service = service;
    }

    public IConfiguration Configuration { get; }

    public JwtToken GenerateJwtToken(string username, string password)
    {
        var user = _service.FindUser(username);
        if (Authenticate(user, password))
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((string) Configuration["Jwt:Secret"]));
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

    public string HashedPassword(string plainTextPassword, byte[] userSalt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            plainTextPassword,
            userSalt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8));
    }

    private bool Authenticate(AuthUser user, string plainTextPassword)
    {
        if (user == null || user.HashedPassword.Length <= 0 || user.Salt.Length <= 0)
            return false;

        var hashedPasswordFromPlain = HashedPassword(plainTextPassword, user.Salt);
        return user.HashedPassword.Equals(hashedPasswordFromPlain);
    }
}