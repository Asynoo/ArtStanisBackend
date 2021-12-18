using System.Text;
using ArtStanisProject.Core.IServices;
using ArtStanisProject.Security.Entities;

namespace ArtStanisProject.Security
{
    public class AuthDbSeeder : IAuthDbSeeder
    {
        private readonly AuthDbContext _ctx;
        private readonly ISecurityService _service;

        public AuthDbSeeder(AuthDbContext ctx, ISecurityService service)
        {
            _ctx = ctx;
            _service = service;
        }

        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();

            var salt = "NAcl";
            _ctx.LoginUsers.Add(
                new LoginUserEntity
                {
                    Salt = salt,
                    Username = "User",
                    HashedPassword = _service.HashedPassword("12345", Encoding.ASCII.GetBytes(salt))
                });
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}