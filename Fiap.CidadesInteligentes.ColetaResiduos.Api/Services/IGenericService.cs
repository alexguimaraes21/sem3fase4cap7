using Fiap.CidadesInteligentes.ColetaResiduos.Api.Models;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Services
{
    public interface IGenericService<T> where T : class
    {
        IEnumerable<T> FindAll(int page = 1, int pageSize = 10);
        T? FindById(long id);
        void Update(T model);
        void Add(T model);
        void Delete(long id);
    }
}
