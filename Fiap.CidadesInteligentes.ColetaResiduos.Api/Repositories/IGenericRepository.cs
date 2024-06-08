namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> FindAll(int page = 1, int pageSize = 10);
        T? FindOneBy(Func<T, bool> condition);
        void Delete(Func<T, bool> condition);
        void Add(T entity);
        void Update(T entity);
    }
}
