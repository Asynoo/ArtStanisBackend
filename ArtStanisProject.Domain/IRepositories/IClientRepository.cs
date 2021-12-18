using System.Collections.Generic;
using ArtStanisProject.Core.Filtering;
using ArtStanisProject.Core.Models;

namespace ArtStanisProject.Domain.IRepositories
{
    public interface IClientRepository
    {
        List<Client> FindAll(Filter filter);
        Client Find(int clientId);
        Client Create(Client client);
        Client Delete(int clientId);
        Client Update(Client client);
        int Count();
    }
}