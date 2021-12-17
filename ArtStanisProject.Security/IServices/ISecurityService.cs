using ArtStanisProject.Security.Models;

namespace ArtStanisProject.Security.IServices;

public interface ISecurityService
{
    JwtToken GenerateJwtToken(string username, string password);
    string HashedPassword(string plainTextPassword, byte[] userSalt);
}