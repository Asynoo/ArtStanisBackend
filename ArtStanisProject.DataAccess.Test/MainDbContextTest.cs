using ArtStanisProject.Core.Models;
using ArtStanisProject.DataAccess.Entities;
using EntityFrameworkCore.Testing.Moq;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ArtStanisProject.DataAccess.Test
{
    public class MainDbContextTest
    {
        [Fact]
        public void DbContext_WithContextOptions_IsAvailable()
        {
            var mockedDbContext = Create.MockedDbContextFor<MainDbContext>();
            Assert.NotNull(mockedDbContext);
        }

        [Fact]
        public void DbContext_DbSets_MustHaveDbSetWithTypeClientEntity()
        {
            var mockedDbContext = Create.MockedDbContextFor<MainDbContext>();
            Assert.True(mockedDbContext.Clients is DbSet<ClientEntity>);
        }
    }
}