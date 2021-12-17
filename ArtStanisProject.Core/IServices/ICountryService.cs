using ArtStanisProject.Core.Models;

namespace ArtStanisProject.Core.IServices;

public interface ICountryService
{
    List<Country> GetAllCountries();
}