using System.Text;
using ArtStanisProject.Security.IRepositories;
using ArtStanisProject.Security.Models;

namespace ArtStanisProject.Security.Repositories;

public class AuthUserRepository : IAuthUserRepository
{
    private readonly AuthDbContext _ctx;

    public AuthUserRepository(AuthDbContext ctx)
    {
        _ctx = ctx;
    }

    public AuthUser FindUser(string username)
    {
        var entity = _ctx.LoginUsers
            .FirstOrDefault(user => username.Equals(user.Username));
        if (entity == null) return null;
        return new AuthUser
        {
            Id = entity.Id,
            Username = entity.Username,
            HashedPassword = entity.HashedPassword,
            Salt = Encoding.ASCII.GetBytes(entity.Salt)
        };
    }
}