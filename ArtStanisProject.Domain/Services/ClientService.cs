using ArtStanisProject.Core.Filtering;
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

        public List<Client> GetAllClients(Filter filter)
        {
            if (filter.Count is <= 0 or > 100)
            {
                throw new ArgumentException("Filter count must be between 1 and 100");
            }

            var totalCount =  _clientRepository.Count();
            if (filter.Page < 1 || filter.Count * (filter.Page - 1) > totalCount)
            {
                throw new ArgumentException($"You need to put in a filter page between 1 and max page size, max page size allowed now: {(totalCount / filter.Count) + 1}");
            }
            return _clientRepository.FindAll(filter);
        }

        public Client GetClient(int clientId)
        {
            return _clientRepository.Find(clientId);
        }

        public Client CreateClient(Client client)
        {
            return _clientRepository.Create(client);
        }

        public int DeleteClient(int clientId)
        {
            return _clientRepository.Delete(clientId);
        }

        public Client UpdateClient(Client client)
        {
            return _clientRepository.Update(client);
        }

        public int GetClientCount()
        {
            return _clientRepository.Count();
        }
    }
}