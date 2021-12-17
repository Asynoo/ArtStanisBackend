using System;
using System.IO;
using System.Linq;
using ArtStanisProject.Core.IServices;
using ArtStanisProject_Backend.Dtos.Countries;
using Microsoft.AspNetCore.Mvc;

namespace ArtStanisProject_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private ICountryService _service;

        public CountryController(ICountryService service)
        {
            _service = service ?? throw new InvalidDataException("CountryService cannot be null");
        }
        
        [HttpGet]
        public ActionResult<CountriesDto> GetAll()
        {
            try
            {
                var list = _service.GetAllCountries()
                    .Select(c => new CountryDto {
                        Id = c.Id,
                        CountryName = c.CountryName
                    })
                    .ToList();
                return Ok(list);
            }
            catch (Exception)
            {
                return StatusCode(500,"Please contact admin!");
            }
            
        }
    }
}