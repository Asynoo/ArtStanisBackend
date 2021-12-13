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
            var list = _service.GetAllClients()
                .Select(c => new ClientDto() {
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
        
        [HttpGet("{id:int}")]
        public ActionResult<List<Client>> Get(int id)
        {
            try
            {
                var client = _service.GetClient(id);
                return Ok(new ClientDto()
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
    }
}