namespace ArtStanisProject.DataAccess.Entities{

public class AddressEntity
{
    public int Id { get; set; }
    public string Street { get; set; }
    public int HouseNumber { get; set; }
    public int PostalCode { get; set; }
    public string City { get; set; }
    public CountryEntity Country { get; set; }
}
}