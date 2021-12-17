using ArtStanisProject.Core.IServices;
using ArtStanisProject.Core.Models;
using ArtStanisProject.Domain.IRepositories;

namespace ArtStanisProject.Domain.Services;

public class CountryService : ICountryService
{
    private ICountryRepository _repo;

    public CountryService(ICountryRepository repo)
    {
        _repo = repo;
    }

    public List<Country> GetAllCountries()
    {
        return _repo.FindAll();
    }
}