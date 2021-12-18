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
                    City = "Grootebroek",
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
                    Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent at cursus purus. Mauris in arcu sapien. Donec sodales, nisi vel tempus sagittis, metus elit lacinia lorem, nec sodales quam lacus fermentum odio. Donec et porta ex, id rutrum libero. Sed pretium ullamcorper augue sed vehicula. Suspendisse vel egestas turpis. Aenean bibendum fermentum dapibus. Suspendisse sed euismod lacus. Pellentesque libero arcu, dapibus ut elementum sed, gravida ac justo. Vivamus non nisl vel leo viverra vestibulum. Integer est dolor, gravida ut lectus nec, ornare venenatis quam. Maecenas mi eros, efficitur nec pharetra in, dapibus at arcu. Nunc turpis mauris, hendrerit non erat vitae, dignissim congue dolor. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Pellentesque vulputate bibendum velit vel vestibulum.",
                    Address = addresses[1]
                },
                new ClientEntity
                {
                    Name = "Susanne Rosendahl",
                    Email = "sus.ros.19@live.dk",
                    ApplyDate = DateTime.Today,
                    Priority = 3,
                    Notes = "Cras id lacinia nisl. Aenean porta velit eget nisi tempus dignissim. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Mauris bibendum facilisis ante, et porta mauris venenatis eget. Etiam sem odio, condimentum a dignissim nec, sagittis non metus. Phasellus metus nunc, placerat aliquam placerat sit amet, pretium quis sem. Ut odio lacus, tincidunt vitae consequat quis, dapibus vitae velit.",
                    Address = addresses[2]
                },
                new ClientEntity
                {
                    Name = "Pernille Fryd",
                    Email = "Sweetwolf1989@yahoo.dk",
                    ApplyDate = DateTime.Today,
                    Priority = 1,
                    Notes = "In massa eros, congue et convallis eget, imperdiet sed mauris. Duis sed ullamcorper ipsum. Quisque a lectus eu eros volutpat pretium. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Curabitur purus quam, pretium sed pulvinar sit amet, tristique eget felis. Aliquam laoreet scelerisque nulla sit amet ornare. Integer tincidunt tristique dignissim. Cras eget viverra dolor. Vivamus porttitor quis tellus sed malesuada. Ut efficitur consectetur nisl ac efficitur.",
                    Address = addresses[3]
                },
                new ClientEntity
                {
                    Name = "Claus Perrson",
                    Email = "Perrson.Claus@outlook.com",
                    ApplyDate = DateTime.Today,
                    Priority = 2,
                    Notes = "Nulla convallis sit amet neque vel volutpat. Nulla nec porta ex. Aliquam ut rutrum tellus. Vestibulum vulputate et ex nec pulvinar. Suspendisse eleifend viverra est aliquam mollis. Nulla at congue risus. Vivamus ipsum massa, malesuada non lacus a, commodo volutpat justo. Nam fringilla neque felis, venenatis lacinia sem aliquet auctor.",
                    Address = addresses[4]
                },
                new ClientEntity
                {
                    Name = "Heinrik Müller",
                    Email = "Heinrik.W.M@hotmail.de",
                    ApplyDate = DateTime.Today,
                    Priority = 2,
                    Notes = "Etiam rhoncus nisi non rhoncus viverra. Nullam rhoncus eros non viverra accumsan. Integer suscipit, ex eget fermentum porttitor, felis eros dapibus purus, at luctus mauris libero in sem. Proin laoreet, ipsum vel condimentum fermentum, arcu dui bibendum enim, eget volutpat felis mi in ipsum. Mauris facilisis orci vel leo vehicula, eu aliquet massa posuere.",
                    Address = addresses[5]
                },
                new ClientEntity
                {
                    Name = "Karoline Swartz",
                    Email = "Karo1991@gmail.com",
                    ApplyDate = DateTime.Today,
                    Priority = 3,
                    Notes = "Vivamus rhoncus augue ut elit mattis, a dignissim mauris elementum. Nulla faucibus molestie vehicula. Quisque pretium augue vitae nisi porttitor pellentesque in nec ante. Aenean ultricies aliquam leo ac commodo. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce a leo dolor. Ut scelerisque, orci ut elementum dapibus, ex odio luctus ante, ultrices finibus justo lacus at ipsum. In sit amet auctor orci, sed viverra odio. Nam mollis lacinia velit, eu pretium ex fermentum eget.",
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