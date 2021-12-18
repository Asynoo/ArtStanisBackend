using System;

namespace ArtStanisProject.DataAccess.Entities;

public class ClientEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime ApplyDate { get; set; }
    public string Notes { get; set; }
    public int Priority { get; set; }
    public AddressEntity Address { get; set; }
}