using System;

namespace ArtStanisProject.DataAccess.Entities
{
    public class ClientEntity
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