namespace ArtStanisProject.Core.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }
    }
}