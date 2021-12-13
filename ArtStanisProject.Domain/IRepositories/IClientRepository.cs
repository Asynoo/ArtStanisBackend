﻿using System.Collections.Generic;
using ArtStanisProject.Core.Models;

namespace ArtStanisProject.Domain.IRepositories
{
    public interface IClientRepository
    {
        List<Client> FindAll();
        Client Find(int clientId);
    }
}