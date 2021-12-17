using System.Collections.Generic;
using ArtStanisProject.Core.Models;

namespace ArtStanisProject.Domain.IRepositories
{
    public interface ICountryRepository
    {
        List<Country> FindAll();
    }
}