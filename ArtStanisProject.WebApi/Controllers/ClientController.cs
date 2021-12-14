using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArtStanisProject.Core.IServices;
using ArtStanisProject.Core.Models;
using ArtStanisProject_Backend.Dtos.Clients;
using Microsoft.AspNetCore.Mvc;

namespace ArtStanisProject_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService _service;

        public ClientController(IClientService service)
        {
            _service = service ?? throw new InvalidDataException("ClientService cannot be null");
        }

        [HttpGet]
        public ActionResult<ClientsAllDto> GetAll()
        {
            try
            {
                var list = _service.GetAllClients()
                    .Select(c => new ClientDto {
                        Id = c.Id,
                        Name = c.Name,
                        Address = c.Address,
                        Country = c.Country,
                        ApplyDate = c.ApplyDate,
                        Priority = c.Priority,
                        Notes = c.Notes
                    })
                    .ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest("Clients not found");
            }
            
        }
        
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
                    Address = client.Address,
                    Country = client.Country,
                    ApplyDate = client.ApplyDate,
                    Priority = client.Priority,
                    Notes = client.Notes
                });
            }
            catch (Exception e)
            {
                return BadRequest("Client id not found");
            }
        }

        [HttpPost]
        public ActionResult<ClientDto> Create(ClientDto clientDto)
        {
            var client = _service.CreateClient(new Client
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Address = clientDto.Address,
                Country = clientDto.Country,
                ApplyDate = clientDto.ApplyDate,
                Priority = clientDto.Priority,
                Notes = clientDto.Notes
            });
            return StatusCode(201,new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Address = client.Address,
                Country = client.Country,
                ApplyDate = client.ApplyDate,
                Priority = client.Priority,
                Notes = client.Notes
            });
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult<ClientDto> Delete(int id)
        {
            try
            {
                var client = _service.DeleteClient(id);
                return StatusCode(200,new ClientDto
                {
                    Id = client.Id,
                    Name = client.Name,
                    Address = client.Address,
                    Country = client.Country,
                    ApplyDate = client.ApplyDate,
                    Priority = client.Priority,
                    Notes = client.Notes
                });
            }
            catch (Exception e)
            {
                return BadRequest("Client id not found");
            }
        }
        
        [HttpPut("{id:int}")]
        public ActionResult<ClientDto> Update(int id,ClientDto clientDto)
        {
            try
            {
                if (id != clientDto.Id)
                    return BadRequest("IDs don't match");
                var client = _service.UpdateClient(new Client
                {
                    Id = clientDto.Id,
                    Name = clientDto.Name,
                    Address = clientDto.Address,
                    Country = clientDto.Country,
                    ApplyDate = clientDto.ApplyDate,
                    Priority = clientDto.Priority,
                    Notes = clientDto.Notes
                });
                return StatusCode(200,new ClientDto
                {
                    Id = client.Id,
                    Name = client.Name,
                    Address = client.Address,
                    Country = client.Country,
                    ApplyDate = client.ApplyDate,
                    Priority = client.Priority,
                    Notes = client.Notes
                });
            }
            catch (Exception e)
            {
                return BadRequest("Client with specified ID does not exist");
            }
        }
    }
}