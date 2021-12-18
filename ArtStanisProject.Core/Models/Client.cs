using System;
using System.Collections.Generic;

namespace ArtStanisProject.Core.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public DateTime ApplyDate { get; set; }
        public string Notes { get; set; }
        public int Priority { get; set; }
    }
    public class ClientComparer : IEqualityComparer<Client>
    {
        public bool Equals(Client x, Client y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Name == y.Name && x.ApplyDate.Equals(y.ApplyDate)
                   && x.Notes == y.Notes && x.Priority == y.Priority;
        }

        public int GetHashCode(Client obj)
        {
            return HashCode.Combine(obj.Id, obj.Name, obj.ApplyDate, obj.Notes, obj.Priority);
        }
    }
}