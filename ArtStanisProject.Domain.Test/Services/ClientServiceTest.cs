using System;
using System.Collections.Generic;
using System.IO;
using ArtStanisProject.Core.Filtering;
using ArtStanisProject.Core.IServices;
using ArtStanisProject.Core.Models;
using ArtStanisProject.Domain.IRepositories;
using ArtStanisProject.Domain.Services;
using Moq;
using Xunit;

namespace ArtStanisProject.Domain.Test.Services
{
    public class ClientServiceTest
    {
        private readonly Mock<IClientRepository> _mock;
        private readonly ClientService _service;
        private List<Client> _expected;

        public ClientServiceTest()
        {
            _mock = new Mock<IClientRepository>();
            _service = new ClientService(_mock.Object);
        }

        [Fact]
        public void ClientService_isIClientService()
        {
            Assert.True(_service is IClientService);
        }

        [Fact]
        public void ClientService_WithNullRepositoryThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new ClientService(null));
        }

        [Fact]
        public void GetClients_CallsFindAllExactlyOnce()
        {
            var filter = new Filter
            {
                Count = 10,
                Page = 1,
                SortOrder = null,
                SortBy = null
            };
            _service.GetAllClients(filter);
            _mock.Verify(repository => repository.FindAll(filter), Times.Once);
        }

        [Fact]
        public void GetClients_ReturnsListOfAllClients()
        {
            var filter = new Filter
            {
                Count = 10,
                Page = 1,
                SortOrder = null,
                SortBy = null
            };
            _mock.Setup(r => r.FindAll(filter)).Returns(_expected);
            var actual = _service.GetAllClients(filter);
            Assert.Equal(_expected, actual);
        }

        [Fact]
        public void GetClients_WithFilterCountOverHundred_ThrowsArgumentExceptionWithMessage()
        {
            var filter = new Filter
            {
                Count = 101,
                Page = 1,
                SortOrder = null,
                SortBy = null
            };
            var ex = Assert.Throws<ArgumentException>(() => _service.GetAllClients(filter));
            Assert.Equal("Filter count must be between 1 and 100", ex.Message);
        }

        [Fact]
        public void GetClients_WithFilterPageUnderZero_ThrowsArgumentException()
        {
            var filter = new Filter
            {
                Count = 2,
                Page = 0,
                SortOrder = null,
                SortBy = null
            };
            Assert.Throws<ArgumentException>(() => _service.GetAllClients(filter));
        }

        [Fact]
        public void GetClient_CallsFindExactlyOnce()
        {
            _service.GetClient(1);
            _mock.Verify(repository => repository.Find(1), Times.Once);
        }

        [Fact]
        public void CreateClient_CallsCreateExactlyOnce()
        {
            var client = new Client();
            _service.CreateClient(client);
            _mock.Verify(repository => repository.Create(client), Times.Once);
        }

        [Fact]
        public void DeleteClient_CallsDeleteExactlyOnce()
        {
            _service.DeleteClient(1);
            _mock.Verify(repository => repository.Delete(1), Times.Once);
        }

        [Fact]
        public void UpdateClient_CallsUpdateExactlyOnce()
        {
            var client = new Client();
            _service.UpdateClient(client);
            _mock.Verify(repository => repository.Update(client), Times.Once);
        }

        [Fact]
        public void GetClientCount_CallsCountExactlyOnce()
        {
            _service.GetClientCount();
            _mock.Verify(repository => repository.Count(), Times.Once);
        }
    }
}