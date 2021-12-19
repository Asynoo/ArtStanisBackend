using System.Security.Cryptography;

namespace ArtStanisProject.Core.Models
{
    public static class Salt
    {
        public static byte[] GenerateSalt()
        {
            var rncCsp = new RNGCryptoServiceProvider();
            var salt = new byte[32];
            rncCsp.GetBytes(salt);

            return salt;
        }
    }
}