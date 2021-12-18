using System.Collections.Generic;
using System.IO;
using ArtStanisProject.Core.IServices;
using ArtStanisProject.Core.Models;
using ArtStanisProject.Domain.IRepositories;
using ArtStanisProject.Domain.Services;
using Moq;
using Xunit;

namespace ArtStanisProject.Domain.Test.Services
{
    public class CountryServiceTest
    {
        private readonly Mock<ICountryRepository> _mock;
        private readonly CountryService _service;
        private List<Country> _expected;

        public CountryServiceTest()
        {
            _mock = new Mock<ICountryRepository>();
            _service = new CountryService(_mock.Object);
        }

        [Fact]
        public void CountryService_IsICountryService()
        {
            Assert.True(_service is ICountryService);
        }

        [Fact]
        public void CountryService_WithNullRepositoryThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new ClientService(null));
        }

        [Fact]
        public void GetCountries_CallsFindAllExactlyOnce()
        {
            _service.GetAllCountries();
            _mock.Verify(repository => repository.FindAll(), Times.Once);
        }

        [Fact]
        public void GetClients_ReturnsListOfAllClients()
        {
            _mock.Setup(r => r.FindAll()).Returns(_expected);
            var actual = _service.GetAllCountries();
            Assert.Equal(_expected, actual);
        }
    }
}