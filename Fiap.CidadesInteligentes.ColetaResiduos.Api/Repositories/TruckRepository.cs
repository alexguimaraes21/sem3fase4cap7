using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public class TruckRepository : GenericRepository<TruckModel>, ITruckRepository
    {
        public TruckRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<TruckModel> GetTruckNotHasCollectionScheduled(long[] idsTrucksRouteScheduled)
        {
            return _dbContext.Set<TruckModel>()
                .Where(t => !idsTrucksRouteScheduled.Contains(t.Id));
        }
    }
}
