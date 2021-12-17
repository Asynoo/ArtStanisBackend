using System;
using System.Collections.Generic;
using System.IO;
using ArtStanisProject.DataAccess.Entities;
using Newtonsoft.Json;

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
            List<CountryEntity> countries;
            using (var reader = new StreamReader(Directory.GetCurrentDirectory() + "/countries.json"))
            {
                var json = reader.ReadToEnd();
                countries = JsonConvert.DeserializeObject<List<CountryEntity>>(json);
            }

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
            List<ClientEntity> clients = new()
            {
                new ClientEntity
                {
                    Name = "Client1", ApplyDate = DateTime.Today, Priority = 1, Notes = "none",
                    Address = addresses[1]
                },
                new ClientEntity
                {
                    Name = "Client2", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                    Address = addresses[2]
                },
                new ClientEntity
                {
                    Name = "Client3", ApplyDate = DateTime.Today, Priority = 1, Notes = "none",
                    Address = addresses[3]
                },
                new ClientEntity
                {
                    Name = "Client4", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                    Address = addresses[4]
                },
                new ClientEntity
                {
                    Name = "Client5", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                    Address = addresses[5]
                },
                new ClientEntity
                {
                    Name = "Client6", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                    Address = addresses[0]
                }
            };
            _ctx.Countries.AddRange(countries);
            _ctx.Addresses.AddRange(addresses);
            _ctx.Clients.AddRange(clients);
            _ctx.SaveChanges();
        }

        public void SeedProduction()
        {
            _ctx.Database.EnsureCreated();
        }
    }
}