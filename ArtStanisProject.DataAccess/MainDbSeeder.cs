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
                    Street = "Stuhrsvej",
                    HouseNumber = 26,
                    PostalCode = 9990,
                    City = "Skagen",
                    Country = countries[179]
                },
                new AddressEntity
                {
                    Id = 2,
                    Street = "Germaniastrasse",
                    HouseNumber = 2,
                    PostalCode = 8006,
                    City = "Zürich",
                    Country = countries[116]
                },
                new AddressEntity
                {
                    Id = 3,
                    Street = "Duntzfelts Alle",
                    HouseNumber = 26,
                    PostalCode = 2900,
                    City = "Hellerup",
                    Country = countries[179]
                },
                new AddressEntity
                {
                    Id = 4,
                    Street = "Steinsvikskrenten",
                    HouseNumber = 97,
                    PostalCode = 5239,
                    City = "Rådal",
                    Country = countries[11]
                },
                new AddressEntity
                {
                    Id = 5,
                    Street = "Geerling",
                    HouseNumber = 24,
                    PostalCode = 1613 ,
                    City = "PG Grootebroek",
                    Country = countries[1]
                },
                new AddressEntity
                {
                    Id = 6,
                    Street = "Klosterstraße",
                    HouseNumber = 13,
                    PostalCode = 24534,
                    City = "Neumünster",
                    Country = countries[231]
                }
            };
            List<ClientEntity> clients = new()
            {
                new ClientEntity
                {
                    Name = "Lars Pedersen",
                    Email = "l.pedersen1@gmail.com",
                    ApplyDate = DateTime.Today,
                    Priority = 1,
                    Notes = "none",
                    Address = addresses[1]
                },
                new ClientEntity
                {
                    Name = "Susanne Rosendahl",
                    Email = "sus.ros.19@live.dk",
                    ApplyDate = DateTime.Today,
                    Priority = 3,
                    Notes = "none",
                    Address = addresses[2]
                },
                new ClientEntity
                {
                    Name = "Pernille Fryd",
                    Email = "Sweetwolf1989@yahoo.dk",
                    ApplyDate = DateTime.Today,
                    Priority = 1,
                    Notes = "none",
                    Address = addresses[3]
                },
                new ClientEntity
                {
                    Name = "Claus Perrson",
                    Email = "Perrson.Claus@outlook.com",
                    ApplyDate = DateTime.Today,
                    Priority = 2,
                    Notes = "none",
                    Address = addresses[4]
                },
                new ClientEntity
                {
                    Name = "Heinrik Müller",
                    Email = "Heinrik.W.M@hotmail.de",
                    ApplyDate = DateTime.Today,
                    Priority = 2,
                    Notes = "none",
                    Address = addresses[5]
                },
                new ClientEntity
                {
                    Name = "Karoline Swartz",
                    Email = "Karo1991@gmail.com",
                    ApplyDate = DateTime.Today,
                    Priority = 3,
                    Notes = "none",
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