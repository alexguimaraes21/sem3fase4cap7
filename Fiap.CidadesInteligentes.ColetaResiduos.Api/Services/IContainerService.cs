﻿using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public interface IContainerService : IGenericService<ContainerModel>
    {
        void UpdateCurrentLevel(long id, int currentLevel);
    }
}
