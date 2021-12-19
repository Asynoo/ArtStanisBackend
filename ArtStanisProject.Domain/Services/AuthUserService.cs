using ArtStanisProject.Core.IServices;
using ArtStanisProject.Core.Models;
using ArtStanisProject.Domain.IRepositories;

namespace ArtStanisProject.Domain.Services
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