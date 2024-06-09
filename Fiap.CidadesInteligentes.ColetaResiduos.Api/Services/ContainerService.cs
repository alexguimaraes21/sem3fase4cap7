using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public class ContainerService : IContainerService
    {
        private readonly ContainerRepository _repository;
        public ContainerService(ContainerRepository repository)
        {
            _repository = repository;
        }
        public void Add(ContainerModel model)
        {
            _repository.Add(model);
        }

        public void Delete(long id)
        {
            _repository.Delete(c => c.Id == id);
        }

        public IEnumerable<ContainerModel> FindAll(int page = 1, int pageSize = 10)
        {
            return _repository.FindAll(page, pageSize);
        }

        public ContainerModel? FindById(long id)
        {
            return _repository.FindOneBy(c => c.Id == id);
        }

        public void Update(ContainerModel model)
        {
            _repository.Update(model);
        }
    }
}
