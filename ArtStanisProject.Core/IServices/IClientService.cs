﻿using System.Collections.Generic;
using ArtStanisProject.Core.Models;

namespace ArtStanisProject.Core.IServices
{
    public interface IClientService
    {
        List<Client> GetAllClients();
        Client GetClient(int clientId);
        
    }
}