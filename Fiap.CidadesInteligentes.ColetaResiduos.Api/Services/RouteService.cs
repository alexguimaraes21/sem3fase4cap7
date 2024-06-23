using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }
        public void Add(RouteModel model)
        {
            _routeRepository.Add(model);
        }

        public void Delete(long id)
        {
            _routeRepository.Delete(r => r.Id == id);
        }

        public void FinalizeRoute(long id)
        {
            _routeRepository.FinalizeRoute(id);
        }

        public IEnumerable<RouteModel> FindAll(int page = 1, int pageSize = 10)
        {
            return _routeRepository.FindAll(page, pageSize);
        }

        public RouteModel? FindById(long id)
        {
            return _routeRepository.FindOneBy(r => r.Id == id);
        }

        public void Update(RouteModel model)
        {
            _routeRepository.Update(model);
        }
    }
}
