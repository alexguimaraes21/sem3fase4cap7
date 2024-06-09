using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;
using Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public class TruckService : ITruckService
    {
        private readonly TruckRepository _repository;
        public TruckService(TruckRepository repository)
        {
            _repository = repository;
        }
        public void Add(TruckModel model)
        {
            _repository.Add(model);
        }

        public void Delete(long id)
        {
            _repository.Delete(t => t.Id == id);
        }

        public IEnumerable<TruckModel> FindAll(int page = 1, int pageSize = 10)
        {
            return _repository.FindAll(page, pageSize);
        }

        public TruckModel FindById(long id)
        {
            return _repository.FindOneBy(t => t.Id == id);
        }

        public void Update(TruckModel model)
        {
            _repository.Update(model);
        }

        public TruckModel FindByLicensePlate(string licensePlate)
        {
            return _repository.FindOneBy(t => t.LicensePlate == licensePlate);
        }
    }
}
