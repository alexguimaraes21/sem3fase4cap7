using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public interface ITruckRepository : IGenericRepository<TruckModel>
    {
        IEnumerable<TruckModel> GetTruckNotHasCollectionScheduled(long[] idsTrucksRouteScheduled);
    }
}
