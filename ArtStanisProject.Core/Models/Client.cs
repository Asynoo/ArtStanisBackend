namespace ArtStanisProject.Core.Models;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Address Address { get; set; }
    public DateTime ApplyDate { get; set; }
    public string Notes { get; set; }
    public int Priority { get; set; }
}