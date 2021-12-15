using System.IO;
using System.Linq;
using System.Reflection;
using ArtStanisProject.Core.IServices;
using ArtStanisProject.Core.Models;
using ArtStanisProject_Backend.Controllers;
using ArtStanisProject_Backend.Dtos.Clients;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ArtStanisProject.WebApi.Test
{
    public class ClientControllerTest
    {
        #region ControllerInit
        [Fact]
        public void ClientController_IsOfTypeControllerBase()
        {
            var service = new Mock<IClientService>();
            var controller = new ClientController(service.Object);
            Assert.IsAssignableFrom<ControllerBase>(controller);
        }
        [Fact]
        public void ClientRepository_WithNullDbContextThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new ClientController(null));
        }
        
        [Fact]
        public void ClientRepository_WithNullDbContextThrowsInvalidDataExceptionWithMessage()
        {
            var ex = Assert.Throws<InvalidDataException>(() => new ClientController(null));
            Assert.Equal("ClientService cannot be null",ex.Message);
        }

        [Fact]
        public void ClientController_UsesApiControllerAttribute()
        {
            TypeInfo typeInfo = typeof(ClientController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attr);
        }
        
        [Fact]
        public void ClientController_UsesRouteAttribute()
        {
            TypeInfo typeInfo = typeof(ClientController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            Assert.NotNull(attr);
        }
        
        [Fact]
        public void ClientController_UsesRouteAttribute_WithParamApiControllerNameRoute()
        {
            TypeInfo typeInfo = typeof(ClientController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            var routeAttr = attr as RouteAttribute;
            Assert.Equal("api/[controller]",routeAttr.Template);
        }
        #endregion

        #region GetAllMethod
        [Fact]
        public void ClientController_HasGetAllMethod()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.NotNull(method);
        }

        [Fact]
        public void GetAll_WithNoParams_HasGetHttpAttribute()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            var attr = method.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("HttpGetAttribute"));
            Assert.NotNull(attr);
        }

        [Fact]
        public void GetAll_CallsServicesGetAllClients_Once()
        {
            var service = new Mock<IClientService>();
            var controller = new ClientController(service.Object);
            controller.GetAll();
            service.Verify(clientService => clientService.GetAllClients(),Times.Once);
        }

        #endregion

        #region GetMethod
        
        [Fact]
        public void ClientController_HasGetMethod()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "Get".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void Get_HasGetHttpAttribute()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "Get".Equals(m.Name));
            var attr = method.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("HttpGetAttribute"));
            Assert.NotNull(attr);
        }
        
        [Fact]
        public void Get_HasGetHttpAttribute_WithParamIdInt()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "Get".Equals(m.Name));
            var attr = method.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("HttpGetAttribute"));
            var getAttr = attr as HttpGetAttribute;
            Assert.Equal("{id:int}",getAttr.Template);
        }
        
        [Fact]
        public void Get_CallsServicesGetClient_Once()
        {
            var service = new Mock<IClientService>();
            var controller = new ClientController(service.Object);
            controller.Get(1);
            service.Verify(clientService => clientService.GetClient(1),Times.Once);
        }
        #endregion

        #region CreateMethod

        [Fact]
        public void ClientController_HasCreateMethod()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "Create".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void Create_HasPostHttpAttribute()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "Create".Equals(m.Name));
            var attr = method.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("HttpPostAttribute"));
            Assert.NotNull(attr);
        }
        
        #endregion

        #region DeleteMethod

        [Fact]
        public void ClientController_HasDeleteMethod()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "Delete".Equals(m.Name));
            Assert.NotNull(method);
        }

        [Fact]
        public void Delete_HasDeleteHttpAttribute_WithParamIdInt()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "Delete".Equals(m.Name));
            var attr = method.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("HttpDeleteAttribute"));
            var delAttr = attr as HttpDeleteAttribute;
            Assert.Equal("{id:int}",delAttr.Template);
        }
        
        [Fact]
        public void Delete_CallsServicesDeleteClient_Once()
        {
            var service = new Mock<IClientService>();
            var controller = new ClientController(service.Object);
            controller.Delete(1);
            service.Verify(clientService => clientService.DeleteClient(1),Times.Once);
        }
        #endregion

        #region UpdateMethod

        [Fact]
        public void ClientController_HasUpdateMethod()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "Update".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void Update_HasPutHttpAttribute_WithParamIdInt()
        {
            var method = typeof(ClientController).
                GetMethods().FirstOrDefault(m => "Update".Equals(m.Name));
            var attr = method.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("HttpPutAttribute"));
            var putAttr = attr as HttpPutAttribute;
            Assert.Equal("{id:int}",putAttr.Template);
        }

        #endregion
    }
}