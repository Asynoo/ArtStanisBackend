using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArtStanisProject.Core.Filtering;
using ArtStanisProject.Core.Models;
using ArtStanisProject.DataAccess.Entities;
using ArtStanisProject.DataAccess.Repositories;
using ArtStanisProject.Domain.IRepositories;
using EntityFrameworkCore.Testing.Moq;
using Xunit;

namespace ArtStanisProject.DataAccess.Test.Repositories
{
    public class ClientRepositoryTest
    {
        private readonly List<Client> _expected;
        private readonly ClientRepository _repo;

        public ClientRepositoryTest()
        {
            var mockedDbContext = Create.MockedDbContextFor<MainDbContext>();
            _repo = new ClientRepository(mockedDbContext);
            List<ClientEntity> entities = new()
            {
                new ClientEntity
                {
                    Id = 1, Name = "Client1", ApplyDate = DateTime.Today, Priority = 1, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 1,
                        Street = "Rolfsgade",
                        HouseNumber = 11,
                        PostalCode = 6700,
                        City = "Esbjerg",
                        Country = new CountryEntity
                        {
                            Id = 2,
                            CountryName = "Denmark"
                        }
                    }
                },
                new ClientEntity
                {
                    Id = 2, Name = "Client2", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 2,
                        Street = "Exnersgade",
                        HouseNumber = 61,
                        PostalCode = 6700,
                        City = "Esbjerg",
                        Country = new CountryEntity
                        {
                            Id = 3,
                            CountryName = "USA"
                        }
                    }
                },
                new ClientEntity
                {
                    Id = 3, Name = "Client3", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 3,
                        Street = "Jyllandsgade",
                        HouseNumber = 3,
                        PostalCode = 6700,
                        City = "Esbjerg",
                        Country = new CountryEntity
                        {
                            Id = 4,
                            CountryName = "Iceland"
                        }
                    }
                }
            };
            mockedDbContext.Set<ClientEntity>().AddRange(entities);
            mockedDbContext.SaveChanges();
            _expected = entities.Select(ce => new Client
            {
                Id = ce.Id,
                Name = ce.Name,
                ApplyDate = ce.ApplyDate,
                Priority = ce.Priority,
                Notes = ce.Notes,
                Address = new Address
                {
                    Id = ce.Address.Id,
                    Street = ce.Address.Street,
                    HouseNumber = ce.Address.HouseNumber,
                    PostalCode = ce.Address.PostalCode,
                    City = ce.Address.City,
                    Country = new Country
                    {
                        Id = ce.Address.Country.Id,
                        CountryName = ce.Address.Country.CountryName
                    }
                }
            }).ToList();
        }

        #region Setup

        [Fact]
        public void ClientRepository_IsIClientRepository()
        {
            Assert.IsAssignableFrom<IClientRepository>(_repo);
        }

        [Fact]
        public void ClientRepository_WithNullDbContextThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new ClientRepository(null));
        }

        [Fact]
        public void ClientRepository_WithNullDbContextThrowsInvalidDataExceptionWithMessage()
        {
            var ex = Assert.Throws<InvalidDataException>(() => new ClientRepository(null));
            Assert.Equal("ClientRepository's DbContext cannot be null", ex.Message);
        }

        #endregion

        #region GetMethod

        [Fact]
        public void ClientRepository_GetAllClientEntities_AsAList()
        {
            Assert.Equal(_expected, _repo.FindAll(new Filter
            {
                Count = 10,
                Page = 1,
                SortOrder = null,
                SortBy = null
            }), new Comparer());
        }

        [Fact]
        public void ClientRepository_GetClientEntity_ReturnsCorrectEntity()
        {
            var actualClient = _repo.Find(2);
            var expectedClient = _expected.Single(client => client.Id == 2);
            Assert.Equal(expectedClient, actualClient, new Comparer());
        }

        [Fact]
        public void ClientRepository_GetClientEntityWithIncorrectId_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _repo.Find(0));
        }

        [Fact]
        public void ClientRepository_GetClientEntityWithIncorrectId_ThrowsArgumentExceptionWithMessage()
        {
            var ex = Assert.Throws<ArgumentException>(() => _repo.Find(0));
            Assert.Equal("Client ID not found", ex.Message);
        }

        #endregion

        #region CreateMethod

        [Fact]
        public void ClientRepository_CreateClientEntityNoParam_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _repo.Create(null));
        }

        [Fact]
        public void ClientRepository_CreateClientEntityNoParam_ThrowsArgumentExceptionWithMessage()
        {
            var ex = Assert.Throws<ArgumentException>(() => _repo.Create(null));
            Assert.Equal("Client cannot be null", ex.Message);
        }

        [Fact]
        public void ClientRepository_CreateClientEntity_ReturnsClientWIthCorrectId()
        {
            var client = new Client
            {
                Name = "Client4", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                Address = new Address
                {
                    Id = 4,
                    Street = "Teststreet",
                    HouseNumber = 69,
                    PostalCode = 6700,
                    City = "Springfield",
                    Country = new Country
                    {
                        Id = 2,
                        CountryName = "Denmark"
                    }
                }
            };
            Assert.Equal(4, _repo.Create(client).Id);
        }

        #endregion

        #region DeleteMethod

        [Fact]
        public void ClientRepository_DeleteClientEntityWithIncorrectId_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _repo.Delete(0));
        }

        [Fact]
        public void ClientRepository_DeleteClientEntityWithIncorrectId_ThrowsArgumentExceptionWithMessage()
        {
            var ex = Assert.Throws<ArgumentException>(() => _repo.Delete(0));
            Assert.Equal("Client ID not found", ex.Message);
        }

        [Fact]
        public void ClientRepository_DeleteClientEntity_ReturnsCorrectId()
        {
            Assert.Equal(2, _repo.Delete(2));
        }

        #endregion

        #region UpdateMethod

        [Fact]
        public void ClientRepository_UpdateClientEntityNoParam_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _repo.Update(null));
        }

        [Fact]
        public void ClientRepository_UpdateClientEntityNoParam_ThrowsArgumentExceptionWithMessage()
        {
            var ex = Assert.Throws<ArgumentException>(() => _repo.Update(null));
            Assert.Equal("Client cannot be null", ex.Message);
        }

        #endregion
    }

    public class Comparer : IEqualityComparer<Client>
    {
        public bool Equals(Client x, Client y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Name == y.Name && x.ApplyDate.Equals(y.ApplyDate)
                   && x.Notes == y.Notes && x.Priority == y.Priority;
        }

        public int GetHashCode(Client obj)
        {
            return HashCode.Combine(obj.Id, obj.Name, obj.ApplyDate, obj.Notes, obj.Priority);
        }
    }
}