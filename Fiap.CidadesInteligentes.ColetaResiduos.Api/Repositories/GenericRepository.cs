using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Fiap.CidadesInteligentes.ColetaResiduos.Api.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(Func<T, bool> condition)
        {
            var entity = FindOneBy(condition);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<T> FindAll(int page = 1, int pageSize = 10)
        {
            return _dbContext.Set<T>()
                .Skip((page - 1) * page)
                .Take(pageSize)
                .AsNoTracking()
                .ToList();
        }

        public T? FindOneBy(Func<T, bool> condition)
        {
            return _dbContext.Set<T>().FirstOrDefault(condition);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
