using System.Collections.Generic;
using System.IO;
using ArtStanisProject.Core.IServices;
using ArtStanisProject.Core.Models;
using ArtStanisProject.Domain.IRepositories;

namespace ArtStanisProject.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository ?? throw new InvalidDataException("ClientRepository Cannot Be Null");
        }
        
        public List<Client> GetAllClients()
        {
            return _clientRepository.FindAll();
        }

        public Client GetClient(int clientId)
        {
            return _clientRepository.Find(clientId);
        }
    }
}