using ArtStanisProject.Core.Models;

namespace ArtStanisProject.Core.IServices;

public interface IAuthUserService
{
    AuthUser FindUser(string username);
}