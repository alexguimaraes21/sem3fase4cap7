using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public class RouteRepository : GenericRepository<RouteModel>, IGenericRepository<RouteModel>
    {
        public RouteRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
