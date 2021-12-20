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
                    PostalCode = 1613,
                    City = "Grootebroek",
                    Country = countries[1]
                },
                new AddressEntity
                {
                    Id = 6,
                    Street = "Rudbølvej",
                    HouseNumber = 28,
                    PostalCode = 8600,
                    City = "Silkeborg",
                    Country = countries[179]
                },
                new AddressEntity
                {
                    Id = 7,
                    Street = "Töpferstraße",
                    HouseNumber = 3,
                    PostalCode = 3375,
                    City = "Krummnußbaum",
                    Country = countries[165]
                },
                new AddressEntity
                {
                    Id = 8,
                    Street = "Trolieberg",
                    HouseNumber = 31,
                    PostalCode = 3010,
                    City = "Leuven",
                    Country = countries[172]
                },
                new AddressEntity
                {
                    Id = 9,
                    Street = "Zijpe",
                    HouseNumber = 45,
                    PostalCode = 5032,
                    City = "Tilburg",
                    Country = countries[14]
                },
                new AddressEntity
                {
                    Id = 10,
                    Street = "Leitevegen",
                    HouseNumber = 4,
                    PostalCode = 5521,
                    City = "Haugesund",
                    Country = countries[11]
                },
                new AddressEntity
                {
                    Id = 11,
                    Street = "Møllegata",
                    HouseNumber = 5,
                    PostalCode = 4319,
                    City = "Sandnes",
                    Country = countries[11]
                },
                new AddressEntity
                {
                    Id = 12,
                    Street = "Kirsebærhaven",
                    HouseNumber = 17,
                    PostalCode = 6430,
                    City = "Nordborg",
                    Country = countries[179]
                },
                new AddressEntity
                {
                    Id = 13,
                    Street = "Skolebakken",
                    HouseNumber = 29,
                    PostalCode = 4900,
                    City = "Nakskov",
                    Country = countries[179]
                },
                new AddressEntity
                {
                    Id = 14,
                    Street = "Um Railand",
                    HouseNumber = 40,
                    PostalCode = 6114,
                    City = "Junglinster",
                    Country = countries[35]
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
                    Notes =
                        "It real sent your at. Amounted all shy set why followed declared. Repeated of endeavor mr position kindness offering ignorant so up. Simplicity are melancholy preference considered saw companions. Disposal on outweigh do speedily in on. Him ham although thoughts entirely drawings. Acceptance unreserved old admiration projection nay yet him. Lasted am so before on esteem vanity oh.",
                    Address = addresses[1]
                },
                new ClientEntity
                {
                    Name = "Susanne Rosendahl",
                    Email = "sus.ros.19@live.dk",
                    ApplyDate = DateTime.Today,
                    Priority = 3,
                    Notes =
                        "Inquietude simplicity terminated she compliment remarkably few her nay. The weeks are ham asked jokes. Neglected perceived shy nay concluded. Not mile draw plan snug next all. Houses latter an valley be indeed wished merely in my. Money doubt oh drawn every or an china. Visited out friends for expense message set eat.",
                    Address = addresses[2]
                },
                new ClientEntity
                {
                    Name = "Pernille Fryd",
                    Email = "Sweetwolf1989@yahoo.dk",
                    ApplyDate = DateTime.Today,
                    Priority = 1,
                    Notes =
                        "Talent she for lively eat led sister. Entrance strongly packages she out rendered get quitting denoting led. Dwelling confined improved it he no doubtful raptures. Several carried through an of up attempt gravity. Situation to be at offending elsewhere distrusts if. Particular use for considered projection cultivated. Worth of do doubt shall it their. Extensive existence up me contained he pronounce do. Excellence inquietude assistance precaution any impression man sufficient.",
                    Address = addresses[3]
                },
                new ClientEntity
                {
                    Name = "Claus Perrson",
                    Email = "Perrson.Claus@outlook.com",
                    ApplyDate = DateTime.Today,
                    Priority = 2,
                    Notes =
                        "Questions explained agreeable preferred strangers too him her son. Set put shyness offices his females him distant. Improve has message besides shy himself cheered however how son. Quick judge other leave ask first chief her. Indeed or remark always silent seemed narrow be. Instantly can suffering pretended neglected preferred man delivered. Perhaps fertile brandon do imagine to cordial cottage.",
                    Address = addresses[4]
                },
                new ClientEntity
                {
                    Name = "Heinrik Müller",
                    Email = "Heinrik.W.M@hotmail.de",
                    ApplyDate = DateTime.Today,
                    Priority = 2,
                    Notes =
                        "Ecstatic advanced and procured civility not absolute put continue. Overcame breeding or my concerns removing desirous so absolute. My melancholy unpleasing imprudence considered in advantages so impression. Almost unable put piqued talked likely houses her met. Met any nor may through resolve entered. An mr cause tried oh do shade happy.",
                    Address = addresses[5]
                },
                new ClientEntity
                {
                    Name = "Karoline Swartz",
                    Email = "Karo1991@gmail.com",
                    ApplyDate = DateTime.Today,
                    Priority = 2,
                    Notes =
                        "Old unsatiable our now but considered travelling impression. In excuse hardly summer in basket misery. By rent an part need. At wrong of of water those linen. Needed oppose seemed how all. Very mrs shed shew gave you. Oh shutters do removing reserved wandered an. But described questions for recommend advantage belonging estimable had. Pianoforte reasonable as so am inhabiting. Chatty design remark and his abroad figure but its.",
                    Address = addresses[0]
                },
                new ClientEntity
                {
                    Name = "Candide Cadi",
                    Email = "kmcindrew0@devhub.com",
                    ApplyDate = DateTime.Today,
                    Priority = 2,
                    Notes =
                        "Repulsive questions contented him few extensive supported. Of remarkably thoroughly he appearance in. Supposing tolerably applauded or of be. Suffering unfeeling so objection agreeable allowance me of. Ask within entire season sex common far who family. As be valley warmth assure on. Park girl they rich hour new well way you. Face ye be me been room we sons fond.",
                    Address = addresses[7]
                },
                new ClientEntity
                {
                    Name = "Petros Douglas",
                    Email = "epraill1@archive.org",
                    ApplyDate = DateTime.Today,
                    Priority = 3,
                    Notes =
                        "Sportsman do offending supported extremity breakfast by listening. Decisively advantages nor expression unpleasing she led met. Estate was tended ten boy nearer seemed. As so seeing latter he should thirty whence. Steepest speaking up attended it as. Made neat an on be gave show snug tore.",
                    Address = addresses[9]
                },
                new ClientEntity
                {
                    Name = "Stigr Helga",
                    Email = "gspacy2@deviantart.com",
                    ApplyDate = DateTime.Today,
                    Priority = 3,
                    Notes =
                        "Projecting surrounded literature yet delightful alteration but bed men. Open are from long why cold. If must snug by upon sang loud left. As me do preference entreaties compliment motionless ye literature. Day behaviour explained law remainder. Produce can cousins account you pasture. Peculiar delicate an pleasant provided do perceive.",
                    Address = addresses[9]
                },
                new ClientEntity
                {
                    Name = "Bent Leelo",
                    Email = "hdanilenko6@cmu.edu",
                    ApplyDate = DateTime.Today,
                    Priority = 3,
                    Notes =
                        "Ye to misery wisdom plenty polite to as. Prepared interest proposal it he exercise. My wishing an in attempt ferrars. Visited eat you why service looking engaged. At place no walls hopes rooms fully in. Roof hope shy tore leaf joy paid boy. Noisier out brought entered detract because sitting sir. Fat put occasion rendered off humanity has.",
                    Address = addresses[10]
                },
                new ClientEntity
                {
                    Name = "Meike Mathijs",
                    Email = "bblunkett7@google.es",
                    ApplyDate = DateTime.Today,
                    Priority = 3,
                    Notes =
                        "Examine she brother prudent add day ham. Far stairs now coming bed oppose hunted become his. You zealously departure had procuring suspicion. Books whose front would purse if be do decay. Quitting you way formerly disposed perceive ladyship are. Common turned boy direct and yet.",
                    Address = addresses[11]
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