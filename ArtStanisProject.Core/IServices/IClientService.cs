using ArtStanisProject.Core.Filtering;
using ArtStanisProject.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace ArtStanisProject.Core.IServices
{
    public interface IClientService
    {
        List<Client> GetAllClients(Filter filter);
        Client GetClient(int clientId);
        Client CreateClient(Client client);
        int DeleteClient(int clientId);
        Client UpdateClient(Client client);
        int GetClientCount();
    }
}