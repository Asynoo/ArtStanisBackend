namespace ArtStanisProject.Security.Entities
{
    public class LoginUserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public byte[] Salt { get; set; }
    }
}