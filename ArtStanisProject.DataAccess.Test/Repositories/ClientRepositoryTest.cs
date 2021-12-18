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
                    Id = 1, Name = "Client1",Email = "SomeEmail1", ApplyDate = DateTime.Today, Priority = 1, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 1,
                        Street = "SomeStreet1",
                        HouseNumber = 1,
                        PostalCode = 1001,
                        City = "SomeCity",
                        Country = new CountryEntity
                        {
                            Id = 1,
                            CountryName = "SomeCountry1"
                        }
                    }
                },
                new ClientEntity
                {
                    Id = 2, Name = "Client2",Email = "SomeEmail2", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 2,
                        Street = "SomeStreet2",
                        HouseNumber = 2,
                        PostalCode = 1002,
                        City = "SomeCity2",
                        Country = new CountryEntity
                        {
                            Id = 2,
                            CountryName = "SomeCountry2"
                        }
                    }
                },
                new ClientEntity
                {
                    Id = 3, Name = "Client3",Email = "SomeEmail3", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                    Address = new AddressEntity
                    {
                        Id = 3,
                        Street = "SomeStreet3",
                        HouseNumber = 3,
                        PostalCode = 1003,
                        City = "SomeCity3",
                        Country = new CountryEntity
                        {
                            Id = 3,
                            CountryName = "SomeCountry3"
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
            }), new ClientComparer());
        }
        
        [Fact]
        public void ClientRepository_GetAllClientEntitiesWithNoFilter_ThrowsArgumentExceptionWithMessage()
        {
            var ex = Assert.Throws<ArgumentException>(() => _repo.FindAll(null));
            Assert.Equal("This method requires a filter!",ex.Message);
        }

        [Fact]
        public void ClientRepository_GetClientEntity_ReturnsCorrectEntity()
        {
            var actualClient = _repo.Find(2);
            var expectedClient = _expected.Single(client => client.Id == 2);
            Assert.Equal(expectedClient, actualClient, new ClientComparer());
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
        public void ClientRepository_CreateClientEntityWithNonexistentCountry_ThrowsArgumentExceptionWithMessage()
        {
            var clientToCreate = new Client
            {
                Id = 4,Name = "Client4",Email = "SomeEmail4", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                Address = new Address
                {
                    Id = 4,
                    Street = "SomeStreet4",
                    HouseNumber = 4,
                    PostalCode = 1004,
                    City = "SomeCity",
                    Country = new Country
                    {
                        Id = 5,
                        CountryName = "SomeCountry5"
                    }
                }
            };
            var ex = Assert.Throws<ArgumentException>(() => _repo.Create(clientToCreate));
            Assert.Equal("Country not found", ex.Message);
        }

        [Fact]
        public void ClientRepository_CreateClientEntity_ReturnsCorrectClient()
        {
            var clientToCreate = new Client
            {
                Id = 4,Name = "Client4",Email = "SomeEmail4", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                Address = new Address
                {
                    Id = 4,
                    Street = "SomeStreet4",
                    HouseNumber = 4,
                    PostalCode = 1004,
                    City = "SomeCity",
                    Country = new Country
                    {
                        Id = 3,
                        CountryName = "SomeCountry"
                    }
                }
            };
            Assert.Equal(clientToCreate, _repo.Create(clientToCreate), new ClientComparer());
        }
        
        [Fact]
        public void ClientRepository_CreateClientEntity_ClientCountIsIncreased()
        {
            var clientToCreate = new Client
            {
                Id = 4,Name = "Client4",Email = "SomeEmail4", ApplyDate = DateTime.Today, Priority = 3, Notes = "none",
                Address = new Address
                {
                    Id = 3,
                    Street = "SomeStreet3",
                    HouseNumber = 3,
                    PostalCode = 1003,
                    City = "SomeCity3",
                    Country = new Country
                    {
                        Id = 3,
                        CountryName = "SomeCountry"
                    }
                }
            };
            _repo.Create(clientToCreate);
            Assert.Equal(4,_repo.Count());
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
        public void ClientRepository_DeleteClientEntity_ReturnsCorrectEntity()
        {
            var entityToDelete = new Client
            {
                Id = 2, Name = "Client2",Email = "SomeEmail2", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                Address = new Address
                {
                    Id = 2,
                    Street = "SomeStreet2",
                    HouseNumber = 2,
                    PostalCode = 1002,
                    City = "SomeCity2",
                    Country = new Country
                    {
                        Id = 2,
                        CountryName = "SomeCountry2"
                    }
                }
            };
            Assert.Equal(entityToDelete,_repo.Delete(entityToDelete.Id),new ClientComparer());
        }

        [Fact]
        public void ClientRepository_DeleteClientEntity_ClientCountIsReduced()
        {
            _repo.Delete(3);
            Assert.Equal(2,_repo.Count());
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
        
        [Fact]
        public void ClientRepository_UpdateNonexistentEntity_ThrowsArgumentExceptionWithMessage()
        {
            var entityToUpdate = new Client
            {
                Id = 4, Name = "Client4",Email = "SomeEmail4", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                Address = new Address
                {
                    Id = 2,
                    Street = "SomeStreet2",
                    HouseNumber = 2,
                    PostalCode = 1002,
                    City = "SomeCity3",
                    Country = new Country
                    {
                        Id = 2,
                        CountryName = "SomeCountry2"
                    }
                }
            };
            var ex = Assert.Throws<ArgumentException>(() => _repo.Update(entityToUpdate));
            Assert.Equal("Client with the specified ID does not exist", ex.Message);
        }
        
        [Fact]
        public void ClientRepository_UpdateEntityWithNonexistentCountry_ThrowsArgumentExceptionWithMessage()
        {
            var entityToUpdate = new Client
            {
                Id = 2, Name = "Client2",Email = "SomeEmail2", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                Address = new Address
                {
                    Id = 2,
                    Street = "SomeStreet2",
                    HouseNumber = 2,
                    PostalCode = 1002,
                    City = "SomeCity3",
                    Country = new Country
                    {
                        Id = 5,
                        CountryName = "SomeCountry5"
                    }
                }
            };
            var ex = Assert.Throws<ArgumentException>(() => _repo.Update(entityToUpdate));
            Assert.Equal("Country not found", ex.Message);
        }
        
        [Fact]
        public void ClientRepository_UpdateClientEntity_ReturnsCorrectEntity()
        {
            var entityToUpdate = new Client
            {
                Id = 2, Name = "Client3",Email = "SomeEmail3", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                Address = new Address
                {
                    Id = 2,
                    Street = "SomeStreet2",
                    HouseNumber = 2,
                    PostalCode = 1002,
                    City = "SomeCity2",
                    Country = new Country
                    {
                        Id = 2,
                        CountryName = "SomeCountry2"
                    }
                }
            };
            Assert.Equal(entityToUpdate,_repo.Update(entityToUpdate),new ClientComparer());
        }

        [Fact]
        public void ClientRepository_UpdateClientEntity_ClientCountIsUnaffected()
        {
            var entityToUpdate = new Client
            {
                Id = 2, Name = "Client2",Email = "SomeEmail2", ApplyDate = DateTime.Today, Priority = 2, Notes = "none",
                Address = new Address
                {
                    Id = 2,
                    Street = "SomeStreet2",
                    HouseNumber = 2,
                    PostalCode = 1002,
                    City = "SomeCity3",
                    Country = new Country
                    {
                        Id = 2,
                        CountryName = "SomeCountry2"
                    }
                }
            };
            _repo.Update(entityToUpdate);
            Assert.Equal(3,_repo.Count());
        }
        #endregion
    }
}