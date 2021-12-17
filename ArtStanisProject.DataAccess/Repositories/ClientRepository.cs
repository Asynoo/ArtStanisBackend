using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ArtStanisProject.Core.Filtering;
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

        public List<Client> FindAll(Filter filter)
        {
            var query = _ctx.Clients.Select(ce => new Client
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
                    Country = new Country
                    {
                        Id = ce.Address.Country.Id,
                        CountryName = ce.Address.Country.CountryName,
                        CountryCode = ce.Address.Country.CountryCode
                    }
                }
            });
            var paging = 
                query.Skip(filter.Count * (filter.Page - 1)) // skip through pages
                    .Take(filter.Count); //take the exact amount
            
            if (string.IsNullOrEmpty(filter.SortOrder) || filter.SortOrder.Equals("asc"))
            {
                paging = filter.SortBy switch
                {
                    "id" => paging.OrderBy(p => p.Id),
                    "name" => paging.OrderBy(p => p.Name),
                    "priority" => paging.OrderBy(p => p.Priority),
                    "applyDate" => paging.OrderBy(p => p.ApplyDate),
                    "country" => paging.OrderBy(p => p.Address.Country.CountryName),
                    _ => paging
                };
            }
            else
            {
                paging = filter.SortBy switch
                {
                    "id" => paging.OrderByDescending(p => p.Id),
                    "name" => paging.OrderByDescending(p => p.Name),
                    _ => paging
                };
            }
            return paging.ToList();
        }

        public Client Find(int clientId)
        {
            var query = _ctx.Clients.Select(ce => new Client
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
                    Country = new Country
                    {
                        Id = ce.Address.Country.Id,
                        CountryName = ce.Address.Country.CountryName,
                        CountryCode = ce.Address.Country.CountryCode
                    }
                }
            });
            var foundClient = query.SingleOrDefault(client => client.Id == clientId);
            if (foundClient == null)
                throw new ArgumentException("Client ID not found");
            return foundClient;
        }

        public Client Create(Client client)
        {
            if (client == null) throw new ArgumentException("Client cannot be null");
            var country = _ctx.Countries.FirstOrDefault(c => c.Id == client.Address.Country.Id);
            if (country == null)
                throw new ArgumentException("Country not found");

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
                entity.Country.Id == client.Address.Country.Id
            );
            if (address != null)
                clientEntity.Address = address;
            else
                clientEntity.Address = new AddressEntity
                {
                    Id = client.Address.Id,
                    Street = client.Address.Street,
                    HouseNumber = client.Address.HouseNumber,
                    PostalCode = client.Address.PostalCode,
                    City = client.Address.City,
                    Country = country
                };

            var createdEntity = _ctx.Clients.Add(clientEntity).Entity;
            _ctx.SaveChanges();
            return new Client
            {
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
                    Country = new Country
                    {
                        Id = clientEntity.Address.Country.Id,
                        CountryName = clientEntity.Address.Country.CountryName,
                        CountryCode = clientEntity.Address.Country.CountryCode
                    }
                }
            };
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
            var country = _ctx.Countries.FirstOrDefault(c => c.Id == client.Address.Country.Id);
            if (country == null)
                throw new ArgumentException("Country not found");

            editedClient.Name = client.Name;
            editedClient.Notes = client.Notes;
            editedClient.Priority = client.Priority;
            editedClient.ApplyDate = client.ApplyDate;

            var address = _ctx.Addresses.FirstOrDefault(entity =>
                entity.Street == client.Address.Street &&
                entity.HouseNumber == client.Address.HouseNumber &&
                entity.PostalCode == client.Address.PostalCode &&
                entity.City == client.Address.City &&
                entity.Country.Id == client.Address.Country.Id
            );
            if (address != null)
                editedClient.Address = address;
            else
                editedClient.Address = new AddressEntity
                {
                    Street = client.Address.Street,
                    HouseNumber = client.Address.HouseNumber,
                    PostalCode = client.Address.PostalCode,
                    City = client.Address.City,
                    Country = country
                };

            var updatedEntity = _ctx.Clients.Update(editedClient).Entity;
            _ctx.SaveChanges();
            return new Client
            {
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
                    Country = new Country
                    {
                        Id = updatedEntity.Address.Country.Id,
                        CountryName = updatedEntity.Address.Country.CountryName,
                        CountryCode = updatedEntity.Address.Country.CountryCode
                    }
                }
            };
        }

        public int Count()
        {
            return _ctx.Clients.Count();
        }
    }
}