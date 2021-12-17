using ArtStanisProject.Security.Models;

namespace ArtStanisProject.Security.IRepositories
{
    public interface IAuthUserRepository
    {
        //AuthUser FindByUsernameAndPassword(string username, string password);
        AuthUser FindUser(string username);
    }
}