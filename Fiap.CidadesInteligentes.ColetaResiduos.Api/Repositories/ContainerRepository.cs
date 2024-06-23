using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public class ContainerRepository : GenericRepository<ContainerModel>, IContainerRepository
    {
        public ContainerRepository(DatabaseContext dbContext) : base(dbContext) { }
    }
}
