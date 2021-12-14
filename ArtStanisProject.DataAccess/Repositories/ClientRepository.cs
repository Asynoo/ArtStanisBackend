using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArtStanisProject.Core.Models;
using ArtStanisProject.DataAccess.Entities;
using ArtStanisProject.Domain.IRepositories;

namespace ArtStanisProject.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private MainDbContext _ctx;

        public ClientRepository(MainDbContext ctx)
        {
            _ctx = ctx ?? throw new InvalidDataException("ClientRepository's DbContext cannot be null");
        }

        public List<Client> FindAll()
        {
            return _ctx.Clients.Select(ce => new Client
            {
                Id = ce.Id,
                Name = ce.Name,
                Address = ce.Address,
                ApplyDate = ce.ApplyDate,
                Country = ce.Country,
                Notes = ce.Notes,
                Priority = ce.Priority
            }).ToList();
        }

        public Client Find(int clientId)
        {
            try
            {
                var client = _ctx.Clients.Single(ce => ce.Id == clientId);
                return client != null ? new Client {
                    Id = client.Id,
                    Name = client.Name,
                    Address = client.Address,
                    ApplyDate = client.ApplyDate,
                    Country = client.Country,
                    Notes = client.Notes,
                    Priority = client.Priority
                } : null;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Client ID not found");
            }
        }

        public Client Create(Client client)
        {
            if (client == null) throw new ArgumentException("Client cannot be null");
            var clientEntity = new ClientEntity
            {
                Name = client.Name,
                Address = client.Address,
                Country = client.Country,
                ApplyDate = client.ApplyDate,
                Priority = client.Priority,
                Notes = client.Notes
            };
            var createdEntity = _ctx.Clients.Add(clientEntity).Entity;
            _ctx.SaveChanges();
            return new Client {
                Id = createdEntity.Id,
                Name = createdEntity.Name,
                Address = createdEntity.Address,
                Country = createdEntity.Country,
                ApplyDate = createdEntity.ApplyDate,
                Priority = createdEntity.Priority,
                Notes = createdEntity.Notes };
        }

        public Client Delete(int clientId)
        {
            try
            {
                var clientEntity = _ctx.Clients.Single(ce => ce.Id == clientId);
                var deletedEntity = _ctx.Clients.Remove(clientEntity).Entity;
                _ctx.SaveChanges();
                return new Client {
                    Id = deletedEntity.Id,
                    Name = deletedEntity.Name,
                    Address = deletedEntity.Address,
                    Country = deletedEntity.Country,
                    ApplyDate = deletedEntity.ApplyDate,
                    Priority = deletedEntity.Priority,
                    Notes = deletedEntity.Notes 
                };
            }
            catch (Exception e)
            {
                throw new ArgumentException("Client ID not found");
            }
        }
    }
}