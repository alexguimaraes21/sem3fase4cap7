using AutoMapper;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Controllers;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ResponseModels;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Services;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Test
{
    public class UnitTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private readonly Mock<DatabaseContext> _mockContext;

        #region Controllers
        private readonly CollectionController _collectionController;
        private readonly ContainerController _containerController;
        private readonly NotificationController _notificationController;
        private readonly TruckController _truckController;
        private readonly UserController _userController;
        #endregion

        #region Repositories
        private readonly CollectionRepository _collectionRepository;
        private readonly ContainerRepository _containerRepository;
        private readonly NotificationRepository _notificationRepository;
        private readonly RouteRepository _routeRepository;
        private readonly TruckRepository _truckRepository;
        private readonly UserRepository _userRepository;
        #endregion

        #region Services
        private readonly CollectionService _collectionService;
        private readonly ContainerService _containerService;
        private readonly NotificationService _notificationService;
        private readonly RouteService _routeService;
        private readonly TruckService _truckService;
        private readonly UserService _userService;
        #endregion

        #region DbSets
        private readonly DbSet<CollectionModel> _collections;
        private readonly DbSet<ContainerModel> _containers;
        private readonly DbSet<NotificationModel> _notifications;
        private readonly DbSet<RouteModel> _routes;
        private readonly DbSet<TruckModel> _trucks;
        private readonly DbSet<UserModel> _users;
        #endregion

        public UnitTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            // Mapper Configuration
            var mapperConfig = new AutoMapper.MapperConfiguration(mc =>
            {
                mc.AllowNullCollections = true;
                mc.AllowNullDestinationValues = true;

                /* UserModel, UserViewModel, RegisterViewModel */
                // UserModel -> UserResponseModel || UserResponseModel -> UserModel
                mc.CreateMap<UserModel, UserResponseModel>();
                mc.CreateMap<UserResponseModel, UserModel>();
                // UserModel -> UserViewModel || UserViewModel -> UserModel
                mc.CreateMap<UserModel, UserViewModel>();
                mc.CreateMap<UserViewModel, UserModel>();
                // UserModel -> RegisterViewModel || RegisterViewModel -> UserModel
                mc.CreateMap<UserModel, RegisterViewModel>();
                mc.CreateMap<RegisterViewModel, UserModel>();

                /* TruckModel, TruckViewModel, TruckResponseModel */
                // TruckModel -> TruckViewModel || TruckViewModel -> TruckModel
                mc.CreateMap<TruckModel, TruckViewModel>();
                mc.CreateMap<TruckViewModel, TruckModel>();
                // TruckModel -> TruckResponseModel || TruckResponseModel -> TruckModel
                mc.CreateMap<TruckModel, TruckResponseModel>();
                mc.CreateMap<TruckResponseModel, TruckModel>();

                /* ContainerModel, ContainerViewModel, ContainerResponseModel */
                // ContainerModel -> ContainerViewModel || ContainerViewMode -> ContainerModel
                mc.CreateMap<ContainerModel, ContainerViewModel>();
                mc.CreateMap<ContainerViewModel, ContainerModel>();
                // ContainerModel -> ContainerResponseModel || ContainerResponseModel -> ContainerModel
                mc.CreateMap<ContainerModel, ContainerResponseModel>();
                mc.CreateMap<ContainerResponseModel, ContainerModel>();

                /* NotificationModel, NotificationResponseModel */
                // NotificationModel -> NotificationResponseModel || NotificationResponseModel -> NotificationModel
                mc.CreateMap<NotificationModel, NotificationResponseModel>();
                mc.CreateMap<NotificationResponseModel, NotificationModel>();
            });
            IMapper mapper = mapperConfig.CreateMapper();

            // Collection DateTime Params
            var tomorrow = DateTime.Now.AddDays(1);
            var nextCollection = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 20, 0, 0);

            // Mocks
            _mockContext = new Mock<DatabaseContext>();
            _collections = MockCollectionDbSet(nextCollection);
            _containers = MockContainerDbSet();
            _notifications = MockNotificationDbSet(nextCollection);
            _routes = MockRouteDbSet(nextCollection);
            _trucks = MockTruckDbSet();
            _users = MockUserDbSet();

            // DbSets
            _mockContext.Setup(m => m.Collections).Returns(_collections);
            _mockContext.Setup(m => m.Containers).Returns(_containers);
            _mockContext.Setup(m => m.Notifications).Returns(_notifications);
            _mockContext.Setup(m => m.Routes).Returns(_routes);
            _mockContext.Setup(m => m.Trucks).Returns(_trucks);
            _mockContext.Setup(m => m.Users).Returns(_users);

            // Repositories
            _collectionRepository = new CollectionRepository(_mockContext.Object);
            _containerRepository = new ContainerRepository(_mockContext.Object);
            _notificationRepository = new NotificationRepository(_mockContext.Object);
            _routeRepository = new RouteRepository(_mockContext.Object);
            _truckRepository = new TruckRepository(_mockContext.Object);
            _userRepository = new UserRepository(_mockContext.Object);

            // Services
            _collectionService = new CollectionService(_collectionRepository, _routeRepository);
            _containerService = new ContainerService(_containerRepository, _routeRepository, _collectionRepository, _truckRepository, _notificationRepository);
            _notificationService = new NotificationService(_notificationRepository);
            _routeService = new RouteService(_routeRepository);
            _truckService = new TruckService(_truckRepository);
            _userService = new UserService(_userRepository);

            // Controllers
            _collectionController = new CollectionController(_collectionService);
            _containerController = new ContainerController(_containerService, mapper);
            _notificationController = new NotificationController(_notificationService, mapper);
            _truckController = new TruckController(_truckService, mapper);
            _userController = new UserController(_userService, mapper);
        }

        // DbSet de ContainerModel
        private DbSet<ContainerModel> MockContainerDbSet()
        {
            var data = new List<ContainerModel>
            {
                new ContainerModel { Id = 1, Location = "Rua Flaminio Levy, 400", Capacity = 3610.12, CurrentLevel = 91 },
                new ContainerModel { Id = 2, Location = "Rua Tenente Américo Moreti, 170", Capacity = 4221.13, CurrentLevel = 98 },
                new ContainerModel { Id = 3, Location = "Rua Sizino Patusca, 254", Capacity = 7235.22, CurrentLevel = 90 },
                new ContainerModel { Id = 4, Location = "Rua Adriano de Campos Tourinho, 25", Capacity = 6000.00, CurrentLevel = 30 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ContainerModel>>();

            mockSet.As<IQueryable<ContainerModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ContainerModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ContainerModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ContainerModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }

        private DbSet<CollectionModel> MockCollectionDbSet(DateTime nextCollectionDateTime)
        {

            var data = new List<CollectionModel>
            {
                new CollectionModel { Id = 1, DateTime = nextCollectionDateTime, 
                    ContainerId = 1, Container = new ContainerModel { Id = 1, Location = "Rua Flaminio Levy, 400", Capacity = 3610.12, CurrentLevel = 91 }, 
                    RouteId = 1, Route = new RouteModel { Id = 1, Description = "Rua Flaminio Levy, 400", StartTime = nextCollectionDateTime, EndTime = nextCollectionDateTime.AddHours(1), 
                        TruckId = 1, Truck = new TruckModel { Id = 1, LicensePlate = "ADG8737", Capacity = 15000, Available = true } } },
                new CollectionModel { Id = 2, DateTime = nextCollectionDateTime, 
                    ContainerId = 2, Container = new ContainerModel { Id = 1, Location = "Rua Tenente Américo Moreti, 170", Capacity = 4221.13, CurrentLevel = 98 },
                    RouteId = 2, Route = new RouteModel { Id = 2, Description = "Rua Tenente Américo Moreti, 170", StartTime = nextCollectionDateTime, EndTime = nextCollectionDateTime.AddHours(1),
                        TruckId = 2, Truck = new TruckModel { Id = 2, LicensePlate = "FTB7157", Capacity = 15000, Available = true } } },
                new CollectionModel { Id = 3, DateTime = nextCollectionDateTime, 
                    ContainerId = 4, Container = new ContainerModel { Id = 1, Location = "Rua Sizino Patusca, 254", Capacity = 7235.22, CurrentLevel = 90 },
                    RouteId = 3, Route = new RouteModel { Id = 3, Description = "Rua Sizino Patusca, 254", StartTime = nextCollectionDateTime, EndTime = null,
                        TruckId = 3, Truck = new TruckModel { Id = 3, LicensePlate = "RGA8O82", Capacity = 77523.21, Available = true } } },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<CollectionModel>>();

            mockSet.As<IQueryable<CollectionModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<CollectionModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<CollectionModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<CollectionModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }

        private DbSet<NotificationModel> MockNotificationDbSet(DateTime nextCollectionDateTime)
        {
            var data = new List<NotificationModel>
            {
                new NotificationModel { Id = 1, IsActive = true, Message = "Nova coleta de resíduos agendada para 24/06/2024 20:00. A coleta ocorrerá no Container localizado no endereço Rua Flaminio Levy, 400", NotificationType = "COLLECTION_SCHEDULED_NOTIFICATION", ValidUntil = nextCollectionDateTime },
                new NotificationModel { Id = 2, IsActive = true, Message = "Nova coleta de resíduos foi agendada para 24/06/2024 20:00. A coleta ocorrerá no Container localizado no endereço Rua Flaminio Levy, 400 e será relizada pelo veículo coletor com placa ADG8737", NotificationType = "TRUCK_DRIVER_NOTIFICATION", ValidUntil = nextCollectionDateTime },
                new NotificationModel { Id = 3, IsActive = true, Message = "Nova coleta de resíduos agendada para 24/06/2024 20:00. A coleta ocorrerá no Container localizado no endereço Rua Tenente Américo Moreti, 170", NotificationType = "COLLECTION_SCHEDULED_NOTIFICATION", ValidUntil = nextCollectionDateTime },
                new NotificationModel { Id = 4, IsActive = true, Message = "Nova coleta de resíduos foi agendada para 24/06/2024 20:00. A coleta ocorrerá no Container localizado no endereço Rua Tenente Américo Moreti, 170 e será relizada pelo veículo coletor com placa FTB7157", NotificationType = "TRUCK_DRIVER_NOTIFICATION", ValidUntil = nextCollectionDateTime },
                new NotificationModel { Id = 5, IsActive = true, Message = "Nova coleta de resíduos agendada para 24/06/2024 20:00. A coleta ocorrerá no Container localizado no endereço Rua Sizino Patusca, 254", NotificationType = "COLLECTION_SCHEDULED_NOTIFICATION", ValidUntil = nextCollectionDateTime },
                new NotificationModel { Id = 6, IsActive = true, Message = "Nova coleta de resíduos foi agendada para 24/06/2024 20:00. A coleta ocorrerá no Container localizado no endereço Rua Sizino Patusca, 254 e será relizada pelo veículo coletor com placa RGA8O82", NotificationType = "TRUCK_DRIVER_NOTIFICATION", ValidUntil = nextCollectionDateTime }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<NotificationModel>>();

            mockSet.As<IQueryable<NotificationModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<NotificationModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<NotificationModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<NotificationModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }

        private DbSet<RouteModel> MockRouteDbSet(DateTime nextCollectionDateTime)
        {
            var data = new List<RouteModel>
            {
                new RouteModel { Id = 1, Description = "Rua Flaminio Levy, 400", StartTime = nextCollectionDateTime, EndTime = nextCollectionDateTime.AddHours(1),
                    TruckId = 1, Truck = new TruckModel { Id = 1, LicensePlate = "ADG8737", Capacity = 15000, Available = true } },
                new RouteModel { Id = 2, Description = "Rua Tenente Américo Moreti, 170", StartTime = nextCollectionDateTime, EndTime = nextCollectionDateTime.AddHours(1),
                    TruckId = 2, Truck = new TruckModel { Id = 2, LicensePlate = "FTB7157", Capacity = 15000, Available = true } },
                new RouteModel { Id = 3, Description = "Rua Sizino Patusca, 254", StartTime = nextCollectionDateTime, EndTime = null,
                    TruckId = 3, Truck = new TruckModel { Id = 3, LicensePlate = "RGA8O82", Capacity = 77523.21, Available = true } }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<RouteModel>>();

            mockSet.As<IQueryable<RouteModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<RouteModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<RouteModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<RouteModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }

        private DbSet<TruckModel> MockTruckDbSet()
        {
            var data = new List<TruckModel>
            {
                new TruckModel { Id = 1, LicensePlate = "ADG8737", Capacity = 15000, Available = true },
                new TruckModel { Id = 2, LicensePlate = "FTB7157", Capacity = 15000, Available = true },
                new TruckModel { Id = 3, LicensePlate = "RGA8O82", Capacity = 77523.21, Available = true }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<TruckModel>>();

            mockSet.As<IQueryable<TruckModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<TruckModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TruckModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TruckModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }

        private DbSet<UserModel> MockUserDbSet()
        {
            var data = new List<UserModel>
            {
                new UserModel { Id = 1, Email = "admin@email.com.br", Password = "pass123", IsActive = true, Role = "Admin", CreatedAt = DateTime.Now },
                new UserModel { Id = 2, Email = "manager@email.com.br", Password = "pass123", IsActive = true, Role = "Manager", CreatedAt = DateTime.Now },
                new UserModel { Id = 3, Email = "user@email.com.br", Password = "pass123", IsActive = true, Role = "User", CreatedAt = DateTime.Now }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<UserModel>>();

            mockSet.As<IQueryable<UserModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UserModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }

        private async Task<HttpResponseMessage> httpClient(string urlRequest, string httpMethod = "GET", StringContent postParams = null)
        {
            var client = _factory.CreateClient();
            var urlAuthentication = "/api/v1/Auth/Login";
            var authParams = new StringContent("{ \"email\": \"admin@email.com.br\", \"password\": \"pass123\" }", Encoding.UTF8, "application/json");
            var responseAuthenticated = await client.PostAsync(urlAuthentication, authParams);
            var responseAuthenticatedObject = await responseAuthenticated.Content.ReadFromJsonAsync<AuthModel>();
            var authenticatedClient = _factory.CreateClient();
            authenticatedClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", responseAuthenticatedObject.Token);
            switch(httpMethod)
            {
                default:
                case "GET":
                    return await authenticatedClient.GetAsync(urlRequest);
                case "DELETE":
                    return await authenticatedClient.DeleteAsync(urlRequest);
                case "POST":
                    return await authenticatedClient.PostAsync(urlRequest, postParams);
                case "PUT":
                    return await authenticatedClient.PutAsync(urlRequest, postParams);
                case "PATCH":
                    return await authenticatedClient.PatchAsync(urlRequest, postParams);
            }
        }

        [Fact]
        public async Task AuthControllerTestStatusCode200()
        {
            // Arrange
            var client = _factory.CreateClient();
            var urlAuthentication = "/api/v1/Auth/Login";
            var authParams = new StringContent("{ \"email\": \"user@email.com.br\", \"password\": \"pass123\" }", Encoding.UTF8, "application/json");

            // Act
            var responseAuthenticated = await client.PostAsync(urlAuthentication, authParams);

            // Assert
            responseAuthenticated.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TruckControllerAddTest()
        {
            // Arrange
            var request = "/api/v1/Truck";

            // Act
            var postParams = new StringContent("{ \"licensePlate\": \"MER5Y85\", \"capacity\": 103785.12, \"available\": true}", Encoding.UTF8, "application/json");
            var response = await httpClient(request, "POST", postParams);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task TruckControllerTestStatusCode200()
        {
            // Arrange
            var request = "/api/v1/Truck";

            // Act
            var response = await httpClient(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ContainerControllerAddTest()
        {
            // Arrange
            var request = "/api/v1/Container";

            // Act
            var postParams = new StringContent("{ \"location\": \"Rua Olivo Gomes, 1125\", \"capacity\": 8000.02, \"currentLevel\": 12 }", Encoding.UTF8, "application/json");
            var response = await httpClient(request, "POST", postParams);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ContainerControllerPatchTest()
        {
            // Arrange
            var request = "/api/v1/Container/1?containerLevel=90";

            // Act
            var response = await httpClient(request, "PATCH");

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task ContainerControllerTestStatusCode200()
        {
            // Arrange
            var request = "/api/v1/Container";

            // Act
            var response = await httpClient(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CollectionControllerTestStatusCode200()
        {
            // Arrange
            var request = "/api/v1/Collection";

            // Act
            var response = await httpClient(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task NotificationControllerTestStatusCode200()
        {
            // Arrange
            var request = "/api/v1/Notification";

            // Act
            var response = await httpClient(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UserControllerTestStatusCode200()
        {
            // Arrange
            var request = "/api/v1/User";

            // Act
            var response = await httpClient(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}