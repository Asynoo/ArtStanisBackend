
using System;
using System.Data.Entity;
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
                    Name = "Client1", ApplyDate = DateTime.Today, Priority = 1, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 1,
                        Street = "Rolfsgade",
                        HouseNumber = 11,
                        PostalCode = 6700,
                        City = "Esbjerg",
                        Country = 1
                    }
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client2", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 2,
                        Street = "street2",
                        HouseNumber = 11,
                        PostalCode = 6700,
                        City = "Esbjerg",
                        Country = 1
                    }
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client3", ApplyDate = DateTime.Today, Priority = 1, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 3,
                        Street = "Street3",
                        HouseNumber = 11,
                        PostalCode = 6700,
                        City = "Esbjerg",
                        Country = 1
                    }
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client4", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 4,
                        Street = "Street4",
                        HouseNumber = 11,
                        PostalCode = 6700,
                        City = "Esbjerg",
                        Country = 1
                    }
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client5", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 5,
                        Street = "street5",
                        HouseNumber = 11,
                        PostalCode = 6700,
                        City = "Esbjerg",
                        Country = 1
                    }
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client6", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 6,
                        Street = "street6",
                        HouseNumber = 11,
                        PostalCode = 6700,
                        City = "Esbjerg",
                        Country = 1
                    }
                });
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}