using ArtStanisProject.Core.Models;

namespace ArtStanisProject.Domain.IRepositories
{
    public interface IAuthUserRepository
    {
        //AuthUser FindByUsernameAndPassword(string username, string password);
        AuthUser FindUser(string username);
    }
}