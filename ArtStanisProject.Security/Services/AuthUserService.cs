using ArtStanisProject.Security.IRepositories;
using ArtStanisProject.Security.IServices;
using ArtStanisProject.Security.Models;
using ArtStanisProject.Security.Repositories;

namespace ArtStanisProject.Security.Services
{

    public class AuthUserService : IAuthUserService
    {
        private readonly IAuthUserRepository _repo;

        public AuthUserService(IAuthUserRepository repo)
        {
            _repo = repo;
        }

        public AuthUser FindUser(string username)
        {
            return _repo.FindUser(username);
        }
    }
}