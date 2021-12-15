
using System;
using ArtStanisProject.DataAccess.Entities;

namespace ArtStanisProject.DataAccess
{
    public class MainDbSeeder : IMainDbSeeder
    {
        private readonly MainDbContext _ctx;

        public MainDbSeeder(MainDbContext ctx)
        {
            _ctx = ctx;
        }

        public void SeedDevelopment()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client1", Address = "Street 6",
                    Country = "Denmark", ApplyDate = DateTime.Today, Priority = 1, Notes = "none"
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client2", Address = "Street 9",
                    Country = "Denmark", ApplyDate = DateTime.Today, Priority = 3, Notes = "none"
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client3", Address = "Street 3",
                    Country = "Denmark", ApplyDate = DateTime.Today, Priority = 1, Notes = "none"
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client4", Address = "Street 1",
                    Country = "Denmark", ApplyDate = DateTime.Today, Priority = 2, Notes = "none"
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client5", Address = "Street 2",
                    Country = "Denmark", ApplyDate = DateTime.Today, Priority = 2, Notes = "none"
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client6", Address = "Street 5",
                    Country = "Denmark", ApplyDate = DateTime.Today, Priority = 3, Notes = "none"
                });
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}