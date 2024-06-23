using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public class ContainerService : IContainerService
    {
        private readonly IContainerRepository _containerRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly ICollectionRepository _collectionRepository;
        private readonly ITruckRepository _truckRepository;
        private readonly INotificationRepository _notificationRepository;
        public ContainerService(IContainerRepository repository, 
            IRouteRepository routeRepository,
            ICollectionRepository collectionRepository,
            ITruckRepository truckRepository,
            INotificationRepository notificationRepository)
        {
            _containerRepository = repository;
            _routeRepository = routeRepository;
            _collectionRepository = collectionRepository;
            _truckRepository = truckRepository;
            _notificationRepository = notificationRepository;
        }
        public void Add(ContainerModel model)
        {
            _containerRepository.Add(model);
        }

        public void Delete(long id)
        {
            _containerRepository.Delete(c => c.Id == id);
        }

        public IEnumerable<ContainerModel> FindAll(int page = 1, int pageSize = 10)
        {
            return _containerRepository.FindAll(page, pageSize);
        }

        public ContainerModel? FindById(long id)
        {
            return _containerRepository.FindOneBy(c => c.Id == id);
        }

        public void Update(ContainerModel model)
        {
            _containerRepository.Update(model);
        }

        public void UpdateCurrentLevel(long id, int currentLevel)
        {
            var container = _containerRepository.FindOneBy(cnt => cnt.Id == id);
            if(container != null)
            {
                container.CurrentLevel = currentLevel;
                _containerRepository.Update(container);

                if(currentLevel >= 80)
                {
                    var amanha = DateTime.Now.AddDays(1);
                    var proximaColeta = new DateTime(amanha.Year, amanha.Month, amanha.Day, 20, 0, 0);

                    var collections = _collectionRepository.FindAllScheduledCollections();
                    long[] collectionIds = null;
                    if(collections != null)
                    {
                        foreach (var item in collections)
                        {
                            collectionIds = new long[] { item.Route.TruckId };
                        }
                    }

                    var trucks = _truckRepository.GetTruckNotHasCollectionScheduled(collectionIds);
                    TruckModel truck = null;
                    if(trucks != null)
                    {
                        truck = trucks.FirstOrDefault();
                    }
                    

                    RouteModel routeModel = new RouteModel();
                    routeModel.Description = container.Location;
                    routeModel.StartTime = proximaColeta;
                    routeModel.TruckId = truck.Id;
                    routeModel.Truck = truck;
                    _routeRepository.Add(routeModel);
                    
                    CollectionModel collection = new CollectionModel();
                    collection.Container = container;
                    collection.ContainerId = container.Id;
                    collection.DateTime = proximaColeta;
                    collection.RouteId = routeModel.Id;
                    collection.Route = routeModel;
                    _collectionRepository.Add(collection);

                    NotificationModel notification = new NotificationModel();
                    notification.NotificationType = "COLLECTION_SCHEDULED_NOTIFICATION";
                    notification.ValidUntil = proximaColeta;
                    notification.Message = "Nova coleta de resíduos agendada para " + proximaColeta.ToString("dd/MM/yyyy HH:mm") + ". " +
                        "A coleta ocorrerá no Container localizado no endereço " + container.Location;
                    notification.IsActive = true;
                    _notificationRepository.Add(notification);

                    NotificationModel notificationTruckDriver = new NotificationModel();
                    notificationTruckDriver.NotificationType = "TRUCK_DRIVER_NOTIFICATION";
                    notificationTruckDriver.ValidUntil = proximaColeta;
                    notificationTruckDriver.Message = "Nova coleta de resíduos foi agendada para " + proximaColeta.ToString("dd/MM/yyyy HH:mm") + ". " +
                        "A coleta ocorrerá no Container localizado no endereço " + container.Location + " " +
                        "e será relizada pelo veículo coletor com placa " + truck.LicensePlate;
                    notificationTruckDriver.IsActive = true;
                    _notificationRepository.Add(notificationTruckDriver);
                }
            }
        }
    }
}
