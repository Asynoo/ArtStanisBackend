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
    public class CountryRepositoryTest
    {
        private readonly List<Country> _expected;
        private readonly CountryRepository _repo;

        public CountryRepositoryTest()
        {
            var mockedDbContext = Create.MockedDbContextFor<MainDbContext>();
            _repo = new CountryRepository(mockedDbContext);
            List<CountryEntity> entities = new()
            {
                new CountryEntity
                {
                    Id = 1,
                    CountryName = "SomeCountry1",
                    CountryCode = "SC1"
                },
                new CountryEntity
                {
                    Id = 2,
                    CountryName = "SomeCountry2",
                    CountryCode = "SC2"
                },
                new CountryEntity
                {
                    Id = 3,
                    CountryName = "SomeCOuntry3",
                    CountryCode = "SC3"
                }
            };
            mockedDbContext.Set<CountryEntity>().AddRange(entities);
            mockedDbContext.SaveChanges();
            _expected = entities.Select(ce => new Country
            {
                Id = ce.Id,
                CountryCode = ce.CountryCode,
                CountryName = ce.CountryName
            }).ToList();
        }

        #region Setup
        
        [Fact]
        public void CountryRepository_IsICountryRepository()
        {
            Assert.IsAssignableFrom<ICountryRepository>(_repo);
        }

        [Fact]
        public void CountryRepository_WithNullDbContextThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new CountryRepository(null));
        }

        [Fact]
        public void CountryRepository_WithNullDbContextThrowsInvalidDataExceptionWithMessage()
        {
            var ex = Assert.Throws<InvalidDataException>(() => new CountryRepository(null));
            Assert.Equal("CountryRepository's DbContext cannot be null", ex.Message);
        }
        

        #endregion

        #region GetMethod

        [Fact]
        public void CountryRepository_GetAllCountryEntities_AsAList()
        {
            Assert.Equal(_expected, _repo.FindAll(), new CountryComparer());
        }

        #endregion
    }
}