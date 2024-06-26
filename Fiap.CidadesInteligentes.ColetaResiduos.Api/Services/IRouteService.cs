﻿using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public interface IRouteService : IGenericService<RouteModel>
    {
        void FinalizeRoute(long id);
    }
}
