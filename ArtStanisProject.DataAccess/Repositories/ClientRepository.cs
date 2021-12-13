using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArtStanisProject.Core.Models;
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
    }
}