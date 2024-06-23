using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IRouteRepository _routeRepository;

        public CollectionService(ICollectionRepository collectionRepository, 
            IRouteRepository routeRepository)
        {
            _collectionRepository = collectionRepository;
            _routeRepository = routeRepository;
        }

        public IEnumerable<CollectionModel> FindAllScheduledCollections(int page = 1, int pageSize = 10)
        {
            return _collectionRepository.FindAllScheduledCollections(page, pageSize);
        }

        public void Add(CollectionModel model)
        {
            _collectionRepository.Add(model);
        }

        public void Delete(long id)
        {
            _collectionRepository.Delete(c => c.Id == id);
        }

        public IEnumerable<CollectionModel> FindAll(int page = 1, int pageSize = 10)
        {
            return _collectionRepository.FindAll(page, pageSize);
        }

        public CollectionModel? FindById(long id)
        {
            return _collectionRepository.FindOneBy(c => c.Id == id);
        }

        public void Update(CollectionModel model)
        {
            _collectionRepository.Update(model);
        }

        public void FinalizeCollection(long id)
        {
            var collection = _collectionRepository.FindOneBy(c => c.Id == id);
            if (collection != null)
            {
                _routeRepository.FinalizeRoute(collection.RouteId);
            }
        }
    }
}
