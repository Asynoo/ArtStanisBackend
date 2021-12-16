
using System;
using System.Data.Entity;
using ArtStanisProject.Core.Models;
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
            List<CountryEntity> countries = new()
            {
                new CountryEntity {Id = 1, CountryName = "Denmark"},
                new CountryEntity {Id = 2, CountryName = "Slovakia"},
                new CountryEntity{Id = 3,CountryName = "Poland"},
                new CountryEntity{Id = 4,CountryName = "Sweden"},
                new CountryEntity{Id = 5,CountryName = "Norway"},
                new CountryEntity{Id = 6,CountryName = "Germany"},
                new CountryEntity{Id = 7,CountryName = "France"},
                new CountryEntity{Id = 8,CountryName = "Scotland"},
                new CountryEntity{Id = 9,CountryName = "Ireland"},
                new CountryEntity{Id = 10,CountryName = "Netherlands"}
            };
            List<AddressEntity> addresses = new()
            {
                new AddressEntity
                {
                    Id = 1,
                    Street = "Rolfsgade",
                    HouseNumber = 11,
                    PostalCode = 6700,
                    City = "Esbjerg",
                    Country = countries[1]
                },
                new AddressEntity
                {
                    Id = 2,
                    Street = "street2",
                    HouseNumber = 11,
                    PostalCode = 6700,
                    City = "Esbjerg",
                    Country = countries[2]
                },
                new AddressEntity
                {
                    Id = 3,
                    Street = "Street3",
                    HouseNumber = 11,
                    PostalCode = 6700,
                    City = "Esbjerg",
                    Country = countries[3]
                },
                new AddressEntity
                {
                    Id = 4,
                    Street = "Street4",
                    HouseNumber = 11,
                    PostalCode = 6700,
                    City = "Esbjerg",
                    Country = countries[4]
                },
                new AddressEntity
                {
                    Id = 5,
                    Street = "street5",
                    HouseNumber = 11,
                    PostalCode = 6700,
                    City = "Esbjerg",
                    Country = countries[5]
                },
                new AddressEntity
                {
                    Id = 6,
                    Street = "street6",
                    HouseNumber = 11,
                    PostalCode = 6700,
                    City = "Esbjerg",
                    Country = countries[0]
                }
            };
            _ctx.Countries.AddRange(countries);
            _ctx.Addresses.AddRange(addresses);
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client1", ApplyDate = DateTime.Today, Priority = 1, Notes = "none",
                    Address = addresses[1]
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client2", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                    Address = addresses[2]
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client3", ApplyDate = DateTime.Today, Priority = 1, Notes = "none",
                    Address = addresses[3]
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client4", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                    Address = addresses[4]
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client5", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                    Address = addresses[5]
                });
            _ctx.Clients.Add(
                new ClientEntity
                {
                    Name = "Client6", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                    Address = addresses[0]
                });
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}