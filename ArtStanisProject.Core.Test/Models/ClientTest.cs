
using ArtStanisProject.Core.Models;
using Xunit;

namespace ArtStanisProject.Core.Test.Models
{
    public class ClientTest
    {
        private readonly Client _client;

        public ClientTest()
        {
            _client = new Client();
        }

        [Fact]
        public void Client_CanBeInitialized()
        {
            Assert.NotNull(_client);
        }
        
        [Fact]
        public void Client_Id_MustBeInt()
        {
            Assert.True(_client.Id is int);
        }

        [Fact]
        public void Client_SetId_StoredID()
        {
            _client.Id = 1;
            Assert.Equal(1, _client.Id);
        }
        
        [Fact]
        public void Client_UpdateId_StoredID()
        {
            _client.Id = 1;
            _client.Id = 2;
            Assert.Equal(2, _client.Id);
        }

        [Fact]
        public void Client_SetName_StoreNameAsString()
        {
            _client.Name = "Item";
            Assert.Equal("Item", _client.Name);
        }
    }
}