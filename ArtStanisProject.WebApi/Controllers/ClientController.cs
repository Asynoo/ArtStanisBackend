using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArtStanisProject.Core.Filtering;
using ArtStanisProject.Core.IServices;
using ArtStanisProject.Core.Models;
using ArtStanisProject_Backend.Dtos.Clients;
using ArtStanisProject_Backend.Dtos.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtStanisProject_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service ?? throw new InvalidDataException("ClientService cannot be null");
        }

        [Authorize]
        [HttpGet]
        public ActionResult<ClientsDto> GetAll([FromQuery] Filter filter)
        {
            try
            {
                var list = _service.GetAllClients(filter)
                    .Select(c => new ClientDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email,
                        ApplyDate = c.ApplyDate,
                        Priority = c.Priority,
                        Notes = c.Notes,
                        Address = new ClientAddressDto
                        {
                            Id = c.Address.Id,
                            Street = c.Address.Street,
                            HouseNumber = c.Address.HouseNumber,
                            PostalCode = c.Address.PostalCode,
                            City = c.Address.City,
                            Country = new CountryDto
                            {
                                Id = c.Address.Country.Id,
                                CountryName = c.Address.Country.CountryName,
                                CountryCode = c.Address.Country.CountryCode
                            }
                        }
                    })
                    .ToList().OrderBy(x => x.Priority);
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest("Clients not found");
            }
        }
        
        [Authorize]
        [HttpGet("{id:int}")]
        public ActionResult<List<Client>> Get(int id)
        {
            try
            {
                var client = _service.GetClient(id);
                return Ok(new ClientDto
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    ApplyDate = client.ApplyDate,
                    Priority = client.Priority,
                    Notes = client.Notes,
                    Address = new ClientAddressDto
                    {
                        Id = client.Address.Id,
                        Street = client.Address.Street,
                        HouseNumber = client.Address.HouseNumber,
                        PostalCode = client.Address.PostalCode,
                        City = client.Address.City,
                        Country = new CountryDto
                        {
                            Id = client.Address.Country.Id,
                            CountryName = client.Address.Country.CountryName,
                            CountryCode = client.Address.Country.CountryCode
                        }
                    }
                });
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Please contact admin");
            }
        }

        [Authorize]
        [HttpGet(nameof(Count))]
        public ActionResult<int> Count()
        {
            return Ok(_service.GetClientCount());
        }

        [Authorize]
        [HttpPost]
        public ActionResult<ClientDto> Create(ClientDto clientDto)
        {
            try
            {
                var client = _service.CreateClient(new Client
                {
                    Id = clientDto.Id,
                    Name = clientDto.Name,
                    Email = clientDto.Email,
                    ApplyDate = clientDto.ApplyDate,
                    Priority = clientDto.Priority,
                    Notes = clientDto.Notes,
                    Address = new Address
                    {
                        Id = clientDto.Address.Id,
                        Street = clientDto.Address.Street,
                        HouseNumber = clientDto.Address.HouseNumber,
                        PostalCode = clientDto.Address.PostalCode,
                        City = clientDto.Address.City,
                        Country = new Country
                        {
                            Id = clientDto.Address.Country.Id,
                            CountryName = clientDto.Address.Country.CountryName,
                            CountryCode = clientDto.Address.Country.CountryCode
                        }
                    }
                });
                return StatusCode(201, new ClientDto
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    ApplyDate = client.ApplyDate,
                    Priority = client.Priority,
                    Notes = client.Notes,
                    Address = new ClientAddressDto
                    {
                        Id = client.Address.Id,
                        Street = client.Address.Street,
                        HouseNumber = client.Address.HouseNumber,
                        PostalCode = client.Address.PostalCode,
                        City = client.Address.City,
                        Country = new CountryDto
                        {
                            Id = client.Address.Country.Id,
                            CountryName = client.Address.Country.CountryName,
                            CountryCode = client.Address.Country.CountryCode
                        }
                    }
                });
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public ActionResult<int> Delete(int id)
        {
            try
            {
                var clientId = _service.DeleteClient(id);
                return StatusCode(200, clientId);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Please contact admin!");
            }
        }

        [Authorize]
        [HttpPut]
        public ActionResult<ClientDto> Update(ClientDto clientDto)
        {
            try
            {
                var client = _service.UpdateClient(new Client
                {
                    Id = clientDto.Id,
                    Name = clientDto.Name,
                    Email = clientDto.Email,
                    ApplyDate = clientDto.ApplyDate,
                    Priority = clientDto.Priority,
                    Notes = clientDto.Notes,
                    Address = new Address
                    {
                        Id = clientDto.Address.Id,
                        Street = clientDto.Address.Street,
                        HouseNumber = clientDto.Address.HouseNumber,
                        PostalCode = clientDto.Address.PostalCode,
                        City = clientDto.Address.City,
                        Country = new Country
                        {
                            Id = clientDto.Address.Country.Id,
                            CountryName = clientDto.Address.Country.CountryName,
                            CountryCode = clientDto.Address.Country.CountryCode
                        }
                    }
                });
                return StatusCode(200, new ClientDto
                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    ApplyDate = client.ApplyDate,
                    Priority = client.Priority,
                    Notes = client.Notes,
                    Address = new ClientAddressDto
                    {
                        Id = client.Address.Id,
                        Street = client.Address.Street,
                        HouseNumber = client.Address.HouseNumber,
                        PostalCode = client.Address.PostalCode,
                        City = client.Address.City,
                        Country = new CountryDto
                        {
                            Id = client.Address.Country.Id,
                            CountryName = client.Address.Country.CountryName,
                            CountryCode = client.Address.Country.CountryCode
                        }
                    }
                });
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}