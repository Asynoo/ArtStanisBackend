using ArtStanisProject.Core.Models;
using ArtStanisProject.Domain.IRepositories;

namespace ArtStanisProject.DataAccess.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly MainDbContext _ctx;

    public CountryRepository(MainDbContext ctx)
    {
        _ctx = ctx;
    }

    public List<Country> FindAll()
    {
        return _ctx.Countries.Select(entity => new Country
        {
            Id = entity.Id,
            CountryName = entity.CountryName,
            CountryCode = entity.CountryCode
        }).ToList();
    }
}