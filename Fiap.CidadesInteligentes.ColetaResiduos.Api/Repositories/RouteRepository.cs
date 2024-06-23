using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public class RouteRepository : GenericRepository<RouteModel>, IRouteRepository
    {
        public RouteRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public void FinalizeRoute(long id)
        {
            var route = this.FindOneBy(r => r.Id == id);
            if (route != null)
            {
                route.EndTime = DateTime.Now;
                this.Update(route);
            }
        }
    }
}
