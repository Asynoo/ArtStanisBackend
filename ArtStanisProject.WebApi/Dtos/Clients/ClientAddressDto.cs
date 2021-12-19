using ArtStanisProject_Backend.Dtos.Countries;

namespace ArtStanisProject_Backend.Dtos.Clients
{
    public class ClientAddressDto
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public CountryDto Country { get; set; }
    }
}