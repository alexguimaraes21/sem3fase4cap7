using Fiap.CidadesInteligentes.ColetaResiduos.Api.Context;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public class TruckRepository : GenericRepository<TruckModel>, IGenericRepository<TruckModel>
    {
        public TruckRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
