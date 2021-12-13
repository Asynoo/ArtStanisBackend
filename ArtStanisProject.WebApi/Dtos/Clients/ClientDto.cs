using System;

namespace ArtStanisProject_Backend.Dtos.Clients
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime ApplyDate { get; set; }
        public string Country { get; set; }
        public string Notes  { get; set; }
        public int Priority { get; set; }
    }
}