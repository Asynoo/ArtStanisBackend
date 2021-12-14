using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArtStanisProject.Core.Models;
using ArtStanisProject.DataAccess.Entities;
using ArtStanisProject.DataAccess.Repositories;
using ArtStanisProject.Domain.IRepositories;
using EntityFrameworkCore.Testing.Moq;
using Moq;
using Xunit;

namespace ArtStanisProject.DataAccess.Test.Repositories
{
    public class ClientRepositoryTest
    {
        private readonly ClientRepository _repo;
        private readonly List<Client> _expected;       
        
        public ClientRepositoryTest()
        {
            MainDbContext mockedDbContext = Create.MockedDbContextFor<MainDbContext>();
            _repo = new ClientRepository(mockedDbContext);
            List<ClientEntity> entities = new()
            {
                new ClientEntity
                {
                    Id = 1, Name = "Client1", Address = "test1",
                    Country = "Denmark", ApplyDate = DateTime.Today, Priority = 1, Notes = "none"
                },
                new ClientEntity
                {
                    Id = 2, Name = "Client2", Address = "test2",
                    Country = "Denmark", ApplyDate = DateTime.Today, Priority = 2, Notes = "none"
                },
                new ClientEntity
                {
                    Id = 3, Name = "Client3", Address = "test3",
                    Country = "Denmark", ApplyDate = DateTime.Today, Priority = 3, Notes = "none"
                }
            };
            mockedDbContext.Set<ClientEntity>().AddRange(entities);
            mockedDbContext.SaveChanges();
            _expected = entities.Select(ce => new Client
            {
                Id = ce.Id,
                Name = ce.Name,
                Address = ce.Address,
                Country = ce.Country,
                ApplyDate = ce.ApplyDate,
                Priority = ce.Priority,
                Notes = ce.Notes
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
            Assert.Equal("ClientRepository's DbContext cannot be null",ex.Message);
        }

        #endregion

        #region GetMethod

        [Fact]
        public void ClientRepository_GetAllClientEntities_AsAList()
        {
            Assert.Equal(_expected,_repo.FindAll(), new Comparer());
        }

        [Fact]
        public void ClientRepository_GetClientEntity_ReturnsCorrectEntity()
        {
            var actualClient = _repo.Find(2);
            var expectedClient = _expected.Single(client => client.Id == 2);
            Assert.Equal(expectedClient,actualClient, new Comparer());
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
            Assert.Equal("Client ID not found",ex.Message);
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
            Assert.Equal("Client cannot be null",ex.Message);
        }

        [Fact]
        public void ClientRepository_CreateClientEntity_ReturnsClientWIthCorrectId()
        {
            var client = new Client
            {
                Name = "Client4", Address = "test4",
                Country = "Denmark", ApplyDate = DateTime.Today, Priority = 3, Notes = "none"
            };
            Assert.Equal(4,_repo.Create(client).Id);
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
            Assert.Equal("Client ID not found",ex.Message);
        }

        [Fact]
        public void ClientRepository_DeleteClientEntity_ReturnsClientWIthCorrectId()
        {
            Assert.Equal(2,_repo.Delete(2).Id);
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
            return x.Id == y.Id && x.Name == y.Name && x.Address == y.Address && x.ApplyDate.Equals(y.ApplyDate) 
                   && x.Country == y.Country && x.Notes == y.Notes && x.Priority == y.Priority;
        }

        public int GetHashCode(Client obj)
        {
            return HashCode.Combine(obj.Id, obj.Name, obj.Address, obj.ApplyDate, obj.Country, obj.Notes, obj.Priority);
        }
    }
}