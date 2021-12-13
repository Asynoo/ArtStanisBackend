using System.Collections.Generic;
using System.IO;
using ArtStanisProject.Core.IServices;
using ArtStanisProject.Core.Models;
using ArtStanisProject.Domain.IRepositories;
using ArtStanisProject.Domain.Services;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace ArtStanisProject.Domain.Test.Services
{
    public class ClientServiceTest
    {
        private Mock<IClientRepository> _mock;
        private ClientService _service;
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
            _service.GetAllClients();
            _mock.Verify(repository => repository.FindAll(), Times.Once);
        }
        
        [Fact]
        public void GetClients_ReturnsListOfAllClients()
        {
            _mock.Setup(r => r.FindAll()).Returns(_expected);
            var actual = _service.GetAllClients();
            Assert.Equal(_expected, actual);
        }
        
        [Fact]
        public void GetClient_CallsFindExactlyOnce()
        {
            _service.GetClient(1);
            _mock.Verify(repository => repository.Find(1), Times.Once);
        }
        
        [Fact]
        public void GetClient_CallsCreateExactlyOnce()
        {
            var client = new Client();
            _service.CreateClient(client);
            _mock.Verify(repository => repository.Create(client), Times.Once);
        }
    }
}