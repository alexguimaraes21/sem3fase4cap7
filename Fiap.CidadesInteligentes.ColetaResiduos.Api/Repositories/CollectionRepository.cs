using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public class CollectionRepository : GenericRepository<CollectionModel>, IGenericRepository<CollectionModel>
    {
        public CollectionRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
