using ArtStanisProject.Core.Models;
using ArtStanisProject.DataAccess.Entities;
using ArtStanisProject.Domain.IRepositories;

namespace ArtStanisProject.DataAccess.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly MainDbContext _ctx;

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
                ApplyDate = ce.ApplyDate,
                Notes = ce.Notes,
                Priority = ce.Priority,
                Address = new Address
                {
                    Id = ce.Address.Id,
                    Street = ce.Address.Street,
                    HouseNumber = ce.Address.HouseNumber,
                    PostalCode = ce.Address.PostalCode,
                    City = ce.Address.City,
                    Country = ce.Address.Country
                }
            }).ToList();
        }

        public Client Find(int clientId)
        {
            try
            {
                var client = _ctx.Clients.Single(ce => ce.Id == clientId);
                return new Client {
                    Id = client.Id,
                    Name = client.Name,
                    ApplyDate = client.ApplyDate,
                    Notes = client.Notes,
                    Priority = client.Priority,
                    Address = new Address
                    {
                        Id = client.Address.Id,
                        Street = client.Address.Street,
                        HouseNumber = client.Address.HouseNumber,
                        PostalCode = client.Address.PostalCode,
                        City = client.Address.City,
                        Country = client.Address.Country
                    }
                };
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
                ApplyDate = client.ApplyDate,
                Priority = client.Priority,
                Notes = client.Notes
            };
            
            var address = _ctx.Addresses.FirstOrDefault(entity =>
                entity.Street == client.Address.Street &&
                entity.HouseNumber == client.Address.HouseNumber &&
                entity.PostalCode == client.Address.PostalCode &&
                entity.City == client.Address.City &&
                entity.Country == client.Address.Country
            );
            if (address != null)
            {
                clientEntity.Address = address;
            }
            else
            {
                clientEntity.Address = new AddressEntity
                {
                    Id = client.Address.Id,
                    Street = client.Address.Street,
                    HouseNumber = client.Address.HouseNumber,
                    PostalCode = client.Address.PostalCode,
                    City = client.Address.City,
                    Country = client.Address.Country
                };
            }
            
            var createdEntity = _ctx.Clients.Add(clientEntity).Entity;
            _ctx.SaveChanges();
            return new Client {
                Id = createdEntity.Id,
                Name = createdEntity.Name,
                ApplyDate = createdEntity.ApplyDate,
                Priority = createdEntity.Priority,
                Notes = createdEntity.Notes,
                Address = new Address
                {
                    Id = createdEntity.Address.Id,
                    Street = createdEntity.Address.Street,
                    HouseNumber = createdEntity.Address.HouseNumber,
                    PostalCode = createdEntity.Address.PostalCode,
                    City = createdEntity.Address.City,
                    Country = createdEntity.Address.Country
                } };
        }

        public int Delete(int clientId)
        {
            try
            {
                var clientEntity = _ctx.Clients.Single(ce => ce.Id == clientId);
                var deletedEntity = _ctx.Clients.Remove(clientEntity).Entity;
                _ctx.SaveChanges();
                return deletedEntity.Id;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Client ID not found");
            }
        }

        public Client Update(Client client)
        {
            if (client == null) throw new ArgumentException("Client cannot be null");
            var editedClient = _ctx.Clients.FirstOrDefault(ce => ce.Id == client.Id);
            if (editedClient == null)
                throw new ArgumentException("Client with the specified ID does not exist");

            editedClient.Name = client.Name;
            editedClient.Notes = client.Notes;
            editedClient.Priority = client.Priority;
            editedClient.ApplyDate = client.ApplyDate;

            var address = _ctx.Addresses.FirstOrDefault(entity =>
                entity.Street == client.Address.Street &&
                entity.HouseNumber == client.Address.HouseNumber &&
                entity.PostalCode == client.Address.PostalCode &&
                entity.City == client.Address.City &&
                entity.Country == client.Address.Country
            );
            if (address != null)
            {
                editedClient.Address = address;
            }
            else
            {
                editedClient.Address = new AddressEntity
                {
                    Street = client.Address.Street,
                    HouseNumber = client.Address.HouseNumber,
                    PostalCode = client.Address.PostalCode,
                    City = client.Address.City,
                    Country = client.Address.Country
                };
            }

            var updatedEntity = _ctx.Clients.Update(editedClient).Entity;
            _ctx.SaveChanges();
            return new Client {
                Id = updatedEntity.Id,
                Name = updatedEntity.Name,
                ApplyDate = updatedEntity.ApplyDate,
                Priority = updatedEntity.Priority,
                Notes = updatedEntity.Notes,
                Address = new Address
                {
                    Id = updatedEntity.Address.Id,
                    Street = updatedEntity.Address.Street,
                    HouseNumber = updatedEntity.Address.HouseNumber,
                    PostalCode = updatedEntity.Address.PostalCode,
                    City = updatedEntity.Address.City,
                    Country = updatedEntity.Address.Country
                }
            };
        }
    }
}