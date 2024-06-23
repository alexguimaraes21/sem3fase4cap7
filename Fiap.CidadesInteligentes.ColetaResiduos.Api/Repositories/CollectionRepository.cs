using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public class CollectionRepository : GenericRepository<CollectionModel>, ICollectionRepository
    {
        public CollectionRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<CollectionModel> FindAllScheduledCollections(int page = 1, int pageSize = 10)
        {
            return _dbContext.Set<CollectionModel>()
                .Where(c => c.DateTime >= DateTime.Now)
                .Include(c => c.Container)
                .Include(c => c.Route)
                    .ThenInclude(r => r.Truck)
                .Where(c => c.Route.EndTime == null)
                .Skip((page - 1) * page)
                .Take(pageSize)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<CollectionModel> FindAll(int page = 1, int pageSize = 10)
        {
            return _dbContext.Set<CollectionModel>()
                .Include(c => c.Container)
                .Include(c => c.Route)
                    .ThenInclude(r => r.Truck)
                .Skip((page - 1) * page)
                .Take(pageSize)
                .AsNoTracking()
                .ToList();
        }
    }
}
