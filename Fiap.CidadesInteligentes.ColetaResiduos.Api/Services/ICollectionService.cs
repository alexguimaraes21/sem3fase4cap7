using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public interface ICollectionService : IGenericService<CollectionModel>
    {
        IEnumerable<CollectionModel> FindAllScheduledCollections(int page = 1, int pageSize = 10);
        void FinalizeCollection(long id);
    }
}
