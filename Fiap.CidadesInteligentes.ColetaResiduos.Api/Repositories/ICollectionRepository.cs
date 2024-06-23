using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public interface ICollectionRepository : IGenericRepository<CollectionModel>
    {
        IEnumerable<CollectionModel> FindAllScheduledCollections(int page = 1, int pageSize = 10);
        IEnumerable<CollectionModel> FindAll(int page = 1, int pageSize = 10);
    }
}
