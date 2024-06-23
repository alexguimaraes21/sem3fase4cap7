using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public interface IRouteRepository : IGenericRepository<RouteModel>
    {
        void FinalizeRoute(long id);
    }
}
