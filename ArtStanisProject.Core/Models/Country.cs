using System;
using System.Collections.Generic;

namespace ArtStanisProject.Core.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
    }

    public class CountryComparer : IEqualityComparer<Country>
    {
        public bool Equals(Country x, Country y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.CountryName == y.CountryName && x.CountryCode == y.CountryCode;
        }

        public int GetHashCode(Country obj)
        {
            return HashCode.Combine(obj.Id, obj.CountryName, obj.CountryCode);
        }
    }
}