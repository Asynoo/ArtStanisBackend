using ArtStanisProject.Security.Models;

namespace ArtStanisProject.Security.IServices
{

    public interface IAuthUserService
    {
        AuthUser FindUser(string username);
    }
}