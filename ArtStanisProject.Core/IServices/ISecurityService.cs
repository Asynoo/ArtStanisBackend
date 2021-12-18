using ArtStanisProject.Core.Models;

namespace ArtStanisProject.Core.IServices
{
    public interface ISecurityService
    {
        JwtToken GenerateJwtToken(string username, string password);
        string HashedPassword(string plainTextPassword, byte[] userSalt);
    }
}